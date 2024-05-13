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



namespace ExamSystem
{
    /// <summary>
    /// Interaction logic for HelpPage.xaml
    /// </summary>
    public partial class HelpPage : Page
    {
        public LoggedInUser currentUser { get; }
        Model.Model _context;
        public HelpPage(LoggedInUser user)
        {
            InitializeComponent();
            currentUser = user;
            LoadStudents();
            LoadExams(currentUser.userName);
        }
        private string connectionString = "Server=localhost;Database=examSystem;Trusted_Connection=True;";

        private void AddExam_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = @"INSERT INTO EXAMs (title, level, kredit_value, start_time, end_time, time_limit) 
                                   VALUES (@Title, @Level, @KreditValue, @StartTime, @EndTime, @TimeLimit)";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Title", txtTitle.Text);
                        command.Parameters.AddWithValue("@Level", txtLevel.Text);
                        command.Parameters.AddWithValue("@KreditValue", int.Parse(txtKreditValue.Text));
                        command.Parameters.AddWithValue("@StartTime", dpStartTime.SelectedDate);
                        command.Parameters.AddWithValue("@EndTime", dpEndTime.SelectedDate);
                        command.Parameters.AddWithValue("@TimeLimit", int.Parse(txtTimeLimit.Text));

                        command.ExecuteNonQuery();
                        MessageBox.Show("Vizsga sikeresen hozzáadva az adatbázishoz.");
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

                    string insertSql = "INSERT INTO STUDENTs_EXAMs (neptun_id, course_id) VALUES (@NeptunId, @CourseId)";
                    using (SqlCommand command = new SqlCommand(insertSql, connection))
                    {
                        command.Parameters.AddWithValue("@NeptunId", studentNeptunId);
                        command.Parameters.AddWithValue("@CourseId", examCourseId);

                        command.ExecuteNonQuery();
                        MessageBox.Show("Vizsga sikeresen hozzáadva a tanulóhoz.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba történt a hozzárendelés közben: " + ex.Message);
            }
        }

    }

}
