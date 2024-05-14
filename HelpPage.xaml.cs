using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using ExamSystem.Logic;
using Model;
using static System.Windows.Forms.AxHost;
using Microsoft.VisualBasic.ApplicationServices;



namespace ExamSystem
{
    /// <summary>
    /// Interaction logic for HelpPage.xaml
    /// </summary>
    public partial class HelpPage : Page
    {
        public LoggedInUser currentUser { get; }
        Model.Model _context;
        private int selectedExamId; // Az aktuális vizsga azonosítója
        int instructorID = -1;
        public HelpPage(LoggedInUser user)
        {
            InitializeComponent();
            currentUser = user;
            LoadStudents();
            LoadExams(currentUser.userName);
            LoadSolutionOptions();
        }
        private string connectionString = "Server=localhost;Database=examSystem;Trusted_Connection=True;";

        private void AddExam_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = @"INSERT INTO EXAMs (title, level, kredit_value, start_time, end_time, time_limit, imgSource) 
                   VALUES (@Title, @Level, @KreditValue, @StartTime, @EndTime, @TimeLimit, @imgSource);
                   SELECT SCOPE_IDENTITY();";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Title", txtTitle.Text);
                        command.Parameters.AddWithValue("@Level", txtLevel.Text);
                        command.Parameters.AddWithValue("@KreditValue", int.Parse(txtKreditValue.Text));
                        command.Parameters.AddWithValue("@StartTime", dpStartTime.SelectedDate);
                        command.Parameters.AddWithValue("@EndTime", dpEndTime.SelectedDate);
                        command.Parameters.AddWithValue("@TimeLimit", int.Parse(txtTimeLimit.Text));
                        command.Parameters.AddWithValue("@imgSource", "/img/noimage.png");

                        // Az új vizsga azonosítójának lekérdezése
                        int newExamId = Convert.ToInt32(command.ExecuteScalar());

                        // Az új vizsga azonosítójának beállítása
                        int examCourseId = newExamId;

                        MessageBox.Show("Vizsga sikeresen hozzáadva az adatbázishoz.");

                        // Oktatót is hozzáadjuk.
                        string insertInstructorSql = "INSERT INTO EXAMs_INSTRUCTORS (course_id, profid) VALUES (@CourseId, @ProfId)";
                        using (SqlCommand insertInstructorCommand = new SqlCommand(insertInstructorSql, connection))
                        {
                            insertInstructorCommand.Parameters.AddWithValue("@CourseId", examCourseId);
                            insertInstructorCommand.Parameters.AddWithValue("@ProfId", GetInstructorID(currentUser.userName));

                            insertInstructorCommand.ExecuteNonQuery();
                        }
                        LoadExams(currentUser.userName);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba történt a vizsga hozzáadása közben: " + ex.Message);
            }
        }
        private bool CheckInput()
        {
            bool isValid = true;
            string errorMessage = "";

            // Ellenőrizzük a kredit értéket
            if (!int.TryParse(txtKreditValue.Text, out _))
            {
                errorMessage += "Kredit érték csak szám lehet.\n";
                isValid = false;
            }


            // Ellenőrizzük a időkorlátot
            if (!int.TryParse(txtTimeLimit.Text, out _))
            {
                errorMessage += "Időkorlát csak szám lehet.\n";
                isValid = false;
            }

            // Kiírjuk az error message-et a labelbe
            lblError.Content = errorMessage;

            return isValid;
        }
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Foreground == Brushes.Gray)
            {
                textBox.Text = "";
                textBox.Foreground = Brushes.Black;
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                if (textBox == txtTitle)
                    textBox.Text = "Enter Title (eg.: Calculus)";
                if (textBox == txtLevel)
                    textBox.Text = "Enter Level (eg.: BsC)";
                if (textBox == txtKreditValue)
                    textBox.Text = "Enter credit Value (eg.: 4)";
                if (textBox == txtTimeLimit)
                    textBox.Text = "Enter Time Limit in seconds(eg.: 150)";
                textBox.Foreground = Brushes.Gray;
            }
        }
        private void LoadStudents()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT neptun_id FROM STUDENTs";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            List<string> students = new List<string>();
                            while (reader.Read())
                            {
                                students.Add(reader.GetString(0));
                            }
                            studentComboBox.ItemsSource = students;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba történt a diákok betöltése közben: " + ex.Message);
            }
        }

        private void LoadExams(string instructor)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = @"SELECT EXAMs.title 
               FROM EXAMs 
               INNER JOIN EXAMs_INSTRUCTORS ON EXAMs.course_id = EXAMs_INSTRUCTORS.course_id
               WHERE EXAMs_INSTRUCTORS.profid = (SELECT profid FROM INSTRUCTORS WHERE username LIKE @Username)";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Username", instructor);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            List<string> exams = new List<string>();
                            while (reader.Read())
                            {
                                exams.Add(reader.GetString(0));
                            }
                            examComboBox.ItemsSource = exams;
                            examComboBox2.ItemsSource = exams;
                            examComboBox3.ItemsSource = exams;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba történt a vizsgák betöltése közben: " + ex.Message);
            }
        }


        private void AssignButton_Click(object sender, RoutedEventArgs e)
        {
            string selectedStudent = studentComboBox.SelectedItem as string;
            string selectedExam = examComboBox.SelectedItem as string;

            if (selectedStudent == null || selectedExam == null)
            {
                MessageBox.Show("Kérem válasszon ki egy tanulót és egy vizsgát az hozzárendeléshez.");
                return;
            }

            try
            {
                // Első lépés: lekérjük a tanuló neptun azonosítóját
                string studentNeptunId = selectedStudent;
                

                // Második lépés: lekérjük a vizsga course_id-jét
                int examCourseId = 0;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = @"SELECT course_id FROM EXAMs WHERE title = @Title";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Title", selectedExam);
                        object result = command.ExecuteScalar();
                        if (result != null)
                        {
                            examCourseId = Convert.ToInt32(result);
                        }
                        else
                        {
                            MessageBox.Show("Nem található a megadott címmel rendelkező vizsga.");
                            return;
                        }
                    }
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string checkSql = "SELECT COUNT(*) FROM STUDENTs_EXAMs WHERE neptun_id = @NeptunId AND course_id = @CourseId";

                    using (SqlCommand checkCommand = new SqlCommand(checkSql, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@NeptunId", studentNeptunId);
                        checkCommand.Parameters.AddWithValue("@CourseId", examCourseId);

                        int count = (int)checkCommand.ExecuteScalar();
                        if (count > 0)
                        {
                            MessageBox.Show("A tanuló már hozzá lett adva a vizsgához!");
                            return;
                        }
                    }
                    // Ellenőrizzük, hogy az oktató már hozzá van-e rendelve a vizsgához az EXAMs_INSTRUCTORS táblában

                    

                    string insertSql = "INSERT INTO STUDENTs_EXAMs (neptun_id, course_id) VALUES (@NeptunId, @CourseId)";
                    using (SqlCommand command = new SqlCommand(insertSql, connection))
                    {
                        command.Parameters.AddWithValue("@NeptunId", studentNeptunId);
                        command.Parameters.AddWithValue("@CourseId", examCourseId);

                        command.ExecuteNonQuery();
                        MessageBox.Show("Vizsga sikeresen hozzáadva a tanulóhoz.");
                        LoadExams(currentUser.userName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba történt a hozzárendelés közben: " + ex.Message);
            }
        }

        private int GetInstructorID(string username)
        {
            int instructorID = -1; // Alapértelmezett érték, ha nem találjuk meg az oktatót

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT profid FROM INSTRUCTORS WHERE username = @Username";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);

                        object result = command.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            instructorID = Convert.ToInt32(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba történt az oktató azonosítójának lekérdezése közben: " + ex.Message);
            }

            return instructorID;
        }

        private void DeleteExam_Click(object sender, RoutedEventArgs e)
        {
            string selectedExam = examComboBox2.SelectedItem as string;
            if (selectedExam == null)
            {
                MessageBox.Show("Válassz ki egy vizsgát a törléshez!");
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Kapcsolatok törlése a STUDENTs_EXAMs táblából
                    string deleteStudentExamsSql = @"DELETE FROM STUDENTs_EXAMs 
                                     WHERE course_id = (SELECT course_id FROM EXAMs WHERE title = @Title)";

                    using (SqlCommand deleteStudentExamsCommand = new SqlCommand(deleteStudentExamsSql, connection))
                    {
                        deleteStudentExamsCommand.Parameters.AddWithValue("@Title", selectedExam);
                        deleteStudentExamsCommand.ExecuteNonQuery();
                    }

                    // Kapcsolatok törlése az EXAMs_INSTRUCTORs táblából
                    string deleteInstructorsSql = @"DELETE FROM EXAMs_INSTRUCTORs 
                                    WHERE course_id = (SELECT course_id FROM EXAMs WHERE title = @Title)";

                    using (SqlCommand deleteInstructorsCommand = new SqlCommand(deleteInstructorsSql, connection))
                    {
                        deleteInstructorsCommand.Parameters.AddWithValue("@Title", selectedExam);
                        deleteInstructorsCommand.ExecuteNonQuery();
                    }

                    // Vizsga törlése az EXAMs táblából
                    string deleteExamSql = @"DELETE FROM EXAMs 
                              WHERE title = @Title";

                    using (SqlCommand deleteExamCommand = new SqlCommand(deleteExamSql, connection))
                    {
                        deleteExamCommand.Parameters.AddWithValue("@Title", selectedExam);
                        int rowsAffected = deleteExamCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("A vizsga sikeresen törölve lett.");
                            // Frissítsd újra a legördülő menüt, hogy az új állapotot tükrözze
                            LoadExams(currentUser.userName);
                        }
                        else
                        {
                            MessageBox.Show("Nem sikerült törölni a vizsgát.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba történt a vizsga törlése közben: " + ex.Message);
            }
        }
        private void AddQuestion_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = @"INSERT INTO QUESTIONs (question, answers, solution, point_value, course_id) 
                               VALUES (@Question, @Answers, @Solution, @PointValue, @CourseId)";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        // Összeállítjuk a válaszokat
                        string answers = $"{txtAnswer1.Text};{txtAnswer2.Text};{txtAnswer3.Text};{txtAnswer4.Text}";

                        command.Parameters.AddWithValue("@Question", txtQuestion.Text);
                        command.Parameters.AddWithValue("@Answers", answers);
                        command.Parameters.AddWithValue("@Solution", cmbSolution.SelectedItem);
                        command.Parameters.AddWithValue("@PointValue", 5); 
                        command.Parameters.AddWithValue("@CourseId", GetSelectedExamId(examComboBox3.SelectedItem.ToString()));

                        command.ExecuteNonQuery();
                        MessageBox.Show("A kérdés sikeresen hozzáadva az adatbázishoz.");
                        ClearFields();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba történt a kérdés hozzáadása során: {ex.Message}");
            }
        }

        private int GetSelectedExamId(string selectedItem)
        {
            if (selectedItem != null)
            {
                try
                {
                    string selectedExam = selectedItem;
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string sql = "SELECT course_id FROM EXAMs WHERE title = @Title";

                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            command.Parameters.AddWithValue("@Title", selectedExam);
                            object result = command.ExecuteScalar();
                            if (result != null)
                            {
                                return Convert.ToInt32(result);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hiba történt az azonosító lekérésekor: {ex.Message}");
                }
            }
            return -1;
        }

        private void ClearFields()
        {
            txtQuestion.Clear();
            txtAnswer1.Clear();
            txtAnswer2.Clear();
            txtAnswer3.Clear();
            txtAnswer4.Clear();
        }
        private void LoadSolutionOptions()
        {
            // Feltöltjük a megoldás lehetőségeket (1, 2, 3, 4) a ComboBox-ban
            for (int i = 1; i <= 4; i++)
            {
                cmbSolution.Items.Add(i);
            }
            cmbSolution.SelectedIndex = 0; // Az alapértelmezett megoldás az első válasz lesz
        }


    }

}
