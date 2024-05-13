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
        public ExamsPage()
        {
            InitializeComponent();
            _context = new Model.Model();

            // Adatok betöltése
            lbExamName0.Content = _context.EXAMs.ToList()[2].title;
            lbExamName1.Content = _context.EXAMs.ToList()[4].title;
            lbExamName2.Content = _context.EXAMs.ToList()[0].title;

            //jó lenne, de nem működik, mivel az enitity developer nem generalta le
            //lbExamPoints1.Content = _context.STUDENTs_EXAMs.ToList()[1].result;
           
            if (GetExamResult("B2TN3S",5)!=null ) {
                pbExam1.Value = int.Parse(GetExamResult("B2TN3S", 5).ToString()); 
                lbExamPoints1.Content = GetExamResult("B2TN3S", 5);
            }
            else{
                pbExam1.Value =0 ;
                lbExamPoints1.Content = 0;
            }
        }
            



        

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
            this.NavigationService.Navigate(new examPage(lbExamName0.Content.ToString()));

        }

        private void btExamStart1_Click(object sender, RoutedEventArgs e)
        {
            //Uri pageFunctionUri = new Uri("examPage.xaml", UriKind.Relative);
            //this.NavigationService.Navigate(pageFunctionUri,correctCounter);
            this.NavigationService.Navigate(new examPage(lbExamName1.Content.ToString()));
        }

        private void btExamStart2_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new examPage(lbExamName2.Content.ToString()));

        }

        private void pbExam1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //pbExam1.Value = correctCounter*20;
            //lbExamPoints1.Content = correctCounter*20;

        }
    }
}
