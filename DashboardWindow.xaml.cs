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
using System.Windows.Shapes;
using System.Globalization;
using Model;
using ExamSystem.Logic;
using Microsoft.VisualBasic.ApplicationServices;
using System.Data.SqlClient;
using static ExamSystem.ExamsPage;



namespace ExamSystem
{
    /// <summary>
    /// Interaction logic for DashboardWindow.xaml
    /// </summary>
    public partial class DashboardWindow : Window
    {
        public LoggedInUser CurrentUser { get; }


        public DashboardWindow(LoggedInUser User)
        {
            CurrentUser = User;

            

            InitializeComponent();

            if (isInstructor(CurrentUser.userName))
            {
                miExams.Visibility = Visibility.Hidden;
                frDashboard.Navigate(new HelpPage(CurrentUser));

            }
            else miHelp.Visibility = Visibility.Hidden;

        }

        private void miSignOut_Click(object sender, RoutedEventArgs e)
        {
            //open the new window
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            //close login window
            Close();
        }

        private void miHelp_Click(object sender, RoutedEventArgs e)
        {
            frDashboard.Navigate(new HelpPage(CurrentUser));

        }

        private void miProfile_Click(object sender, RoutedEventArgs e)
        {
            frDashboard.Navigate(new ProfilePage(CurrentUser));

            //frDashboard.Source = new Uri("ProfilePage.xaml", UriKind.Relative);

        }

        private void miQuestion_Click(object sender, RoutedEventArgs e)
        {
            //ExamsPage examsPage = new ExamsPage(3);

            //frDashboard.Source = new Uri("ExamsPage.xaml", UriKind.Relative);
            frDashboard.Navigate(new ExamsPage(CurrentUser));
        }

        private void frDashboard_Initialized(object sender, EventArgs e)
        {
            frDashboard.Navigate(new ExamsPage(CurrentUser));

        }

        public bool isInstructor(string nev)
        {
            // Itt definiáljuk a kapcsolati sztringet
            string connectionString = "Server=localhost;Database=examSystem;Trusted_Connection=True; TrustServerCertificate=True";

            // SQL parancs, ami lekérdezi a vizsga kurzusait a megadott course_id alapján
            string sql = @"
SELECT username
FROM INSTRUCTORs
WHERE username LIKE @nev";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        // Paraméter hozzáadása a SQL parancshoz
                        command.Parameters.AddWithValue("@nev", nev);

                        // SQL parancs végrehajtása és az eredmény olvasása
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return true;

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }

            return false;
        }


    }
}
