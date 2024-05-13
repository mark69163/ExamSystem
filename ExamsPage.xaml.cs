﻿using System;
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
            
            foreach (int id in validExams)
            {
                //meg nem vizsgazott
                if (GetExamResult(currentUser.userName, id) == null) {

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

                }

            }

            try
            {
                //loading the villamossagtan exam
                //amennyiben nem null, azaz mar egyszer megcsinalta a user
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

                //loading the adatbazisok exam
                if (GetExamResult(currentUser.userName, 3) != null)
                {
                    pbExam0.Value = int.Parse(GetExamResult(currentUser.userName, 3).ToString());
                    lbExamPoints0.Content = GetExamResult(currentUser.userName, 3);
                }
                else
                {
                    pbExam0.Value = 0;
                    lbExamPoints0.Content = 0;
                }
                //loading the programming exam
                if (GetExamResult(currentUser.userName, 1) != null)
                {
                    pbExam2.Value = int.Parse(GetExamResult(currentUser.userName, 1).ToString());
                    lbExamPoints2.Content = GetExamResult(currentUser.userName, 1);
                }
                else
                {
                    pbExam2.Value = 0;
                    lbExamPoints2.Content = 0;
                }


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
