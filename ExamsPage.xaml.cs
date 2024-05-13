using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
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
using ExamSystem.Logic;
using Model;

namespace ExamSystem
{
    /// <summary>
    /// Interaction logic for ExamsPage.xaml
    /// </summary>
    public partial class ExamsPage : Page
    {
        int correctCounter;
        Model.Model _context;
        public LoggedInUser currentUser { get; }

        public ExamsPage(LoggedInUser user)
        {
            InitializeComponent();
            _context = new Model.Model();
            currentUser = user;

            loadExams();
     

        }

        private void loadExams()
        {
            // Adatok betöltése
            lbExamName0.Content = _context.EXAMs.ToList()[2].title;
            lbExamName1.Content = _context.EXAMs.ToList()[4].title;
            lbExamName2.Content = _context.EXAMs.ToList()[0].title;

            //jó lenne, de nem működik, mivel az enitity developer nem generalta le
            //lbExamPoints1.Content = _context.STUDENTs_EXAMs.ToList()[1].result;

            //exam-ek betöltése és engedélyezése
            int[] validExams = getExams(currentUser.userName);
            int excount = 0;
            foreach (int id in validExams)
            {
                if (excount < validExams.Length)
                {
                    Button btExamStart = (Button)this.FindName($"btExamStart{excount}");
                    TextBox tbExam = (TextBox)this.FindName($"tbExam{excount}");
                    ProgressBar pbExam = (ProgressBar)this.FindName($"pbExam{excount}");
                    Label lbExamName = (Label)this.FindName($"lbExamName{excount}");
                    Label lbExamPoints = (Label)this.FindName($"lbExamPoints{excount}");
                    Image imExam = (Image)this.FindName($"imExam{excount}");

                    if (GetExamResult(currentUser.userName, id) == null)
                    {
                        btExamStart.IsEnabled = true;
                    }
                    else
                    {
                        btExamStart.IsEnabled = false;
                        btExamStart.Content = "Finished";
                    }

                    tbExam.Text = String.Format("Level: " + GetExamDetails(id).Level + "\nCredits: " + GetExamDetails(id).KreditValue + "\nTime limit: " + GetExamDetails(id).TimeLimit + " s");
                    if (GetExamResult(currentUser.userName, id) != null)
                    {
                        pbExam.Value = int.Parse(GetExamResult(currentUser.userName, id).ToString());
                    }
                    else
                    {
                        pbExam.Value = 0;
                    }
                    lbExamName.Content = GetExamDetails(id).Title;
                    lbExamPoints.Content = GetExamResult(currentUser.userName, id);
                    imExam.Source = new BitmapImage(new Uri(GetExamDetails(id).imgSource, UriKind.Relative));

                    excount++;
                }
                
                //meg nem vizsgazott
                if (GetExamResult(currentUser.userName, id) == null) {
                    // prog
                    
                    /* Regi, statikus metodus:
                    //villanytan van
                    if (id == 5)
                    {
                        btExamStart1.IsEnabled = true;
                    }

                    //adatb van
                    if (id == 3)
                    {
                        btExamStart0.IsEnabled = true;
                    }

                    //prog van
                    if (id == 1)
                    {
                        btExamStart2.IsEnabled = true;
                    }
                    */
                }

            }
            for (int i = excount; i < 6; i++)
            {
                 Button btExamStart = (Button)this.FindName($"btExamStart{i}");
                 TextBox tbExam = (TextBox)this.FindName($"tbExam{i}");
                 ProgressBar pbExam = (ProgressBar)this.FindName($"pbExam{i}");
                 Label lbExamName = (Label)this.FindName($"lbExamName{i}");
                 Label lbExamPoints = (Label)this.FindName($"lbExamPoints{i}");
                 Image imExam = (Image)this.FindName($"imExam{i}");
                 Label lbResults = (Label)this.FindName($"lbResults{i}");
                 Frame frExam = (Frame)this.FindName($"frExam{i}");

                btExamStart.Visibility = Visibility.Hidden;
                 tbExam.Visibility = Visibility.Hidden;
                 pbExam.Visibility = Visibility.Hidden;
                 lbExamName.Visibility = Visibility.Hidden;
                 lbExamPoints.Visibility = Visibility.Hidden;
                 imExam.Visibility = Visibility.Hidden;
                lbResults.Visibility = Visibility.Hidden;
                frExam.Background = Brushes.White;
            }

            try
            {
                
                //loading the villamossagtan exam
                //amennyiben nem null, azaz mar egyszer megcsinalta a user

                /*
                if (GetExamResult(currentUser.userName, 5) != null)
                {
                    pbExam1.Value = int.Parse(GetExamResult(currentUser.userName, 5).ToString());
                    lbExamPoints1.Content = GetExamResult(currentUser.userName, 5);
                }
                else
                {
                    pbExam1.Value = 0;
                    lbExamPoints1.Content = 0;
                }
                */
                //loading the adatbazisok exam
               


            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to fetch data from database!");
            }

        }


        //lekerdezzuk, hogy milyen vizsgak tartoznak adott neptunkodhoz
        public int[] getExams(string neptunId)
        {
            // Itt definiáljuk a kapcsolati sztringet
            string connectionString = "Server=localhost;Database=examSystem;Trusted_Connection=True; TrustServerCertificate=True";

            // SQL parancs, ami lekérdezi a vizsga kurzusait a megadott hallgató alapján
            string sql = @"
        SELECT course_id
        FROM STUDENTs_EXAMs
        WHERE neptun_id = @NeptunId";

            List<int> courseIdList = new List<int>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        // Paraméter hozzáadása a SQL parancshoz
                        command.Parameters.AddWithValue("@NeptunId", neptunId);

                        // SQL parancs végrehajtása és az eredmény olvasása
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int courseId = Convert.ToInt32(reader["course_id"]); // Helyes oszlop nevet kell használni
                                courseIdList.Add(courseId);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }

            // Listát tömbbé alakítás és visszaadás
            return courseIdList.ToArray();
        }

        public struct ExamDetails
        {
            public int CourseId { get; set; }
            public string Title { get; set; }
            public string Level { get; set; }
            public int KreditValue { get; set; }
            public DateTime StartTime { get; set; }
            public DateTime EndTime { get; set; }
            public int TimeLimit { get; set; }
            public string imgSource { get; set; }
        }

        public ExamDetails GetExamDetails(int courseId)
        {
            // Itt definiáljuk a kapcsolati sztringet
            string connectionString = "Server=localhost;Database=examSystem;Trusted_Connection=True; TrustServerCertificate=True";

            // SQL parancs, ami lekérdezi a vizsga kurzusait a megadott course_id alapján
            string sql = @"
SELECT *
FROM EXAMs
WHERE course_id = @courseId";

            ExamDetails examDetails = new ExamDetails();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        // Paraméter hozzáadása a SQL parancshoz
                        command.Parameters.AddWithValue("@courseId", courseId);

                        // SQL parancs végrehajtása és az eredmény olvasása
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                examDetails.CourseId = Convert.ToInt32(reader["course_id"]);
                                examDetails.Title = reader["title"].ToString();
                                examDetails.Level = reader["level"].ToString();
                                examDetails.KreditValue = Convert.ToInt32(reader["kredit_value"]);
                                examDetails.StartTime = reader.IsDBNull(reader.GetOrdinal("start_time")) ? DateTime.MinValue : Convert.ToDateTime(reader["start_time"]);
                                examDetails.EndTime = reader.IsDBNull(reader.GetOrdinal("end_time")) ? DateTime.MinValue : Convert.ToDateTime(reader["end_time"]);
                                examDetails.TimeLimit = reader.IsDBNull(reader.GetOrdinal("time_limit")) ? 0 : Convert.ToInt32(reader["time_limit"]);
                                examDetails.imgSource = reader["imgSource"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }

            return examDetails;
        }



        //lekerdezzuk, hogy milyen eredmeny tartozik adott neptunkodhoz adott viszga eseten

        public int? GetExamResult(string neptunId, int courseId)
        {
            // Itt definiáljuk a kapcsolati sztringet
            string connectionString = "Server=localhost;Database=examSystem;Trusted_Connection=True; TrustServerCertificate=True";

            // SQL parancs, ami lekérdezi a vizsga eredményét a megadott hallgató és kurzus azonosító alapján
            string sql = @"
        SELECT result
        FROM STUDENTs_EXAMs
        WHERE neptun_id = @NeptunId AND course_id = @CourseId";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        // Paraméterek hozzáadása a SQL parancshoz
                        command.Parameters.AddWithValue("@NeptunId", neptunId);
                        command.Parameters.AddWithValue("@CourseId", courseId);

                        // SQL parancs végrehajtása és az eredmény olvasása
                        object result = command.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            return Convert.ToInt32(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }

            // Ha nincs eredmény, vagy hiba történt, null értékkel térünk vissza
            return null;
        }

      


        private void btExamStart0_Click(object sender, RoutedEventArgs e)
        {
            //atrianyitas a relevans exam-hez, pl ez az adatb
            this.NavigationService.Navigate(new examPage(lbExamName0.Content.ToString(), currentUser));

        }

        private void btExamStart1_Click(object sender, RoutedEventArgs e)
        {
          
            this.NavigationService.Navigate(new examPage(lbExamName1.Content.ToString(), currentUser));
        }

        private void btExamStart2_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new examPage(lbExamName2.Content.ToString(), currentUser));

        }

        private void pbExam1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
          
        }
    }
}
