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
            InitializeComponent();
            CurrentUser = User;

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
            frDashboard.Source = new Uri("HelpPage.xaml",UriKind.Relative);
           
        }

        private void miProfile_Click(object sender, RoutedEventArgs e)
        {
            frDashboard.Navigate(new ProfilePage(CurrentUser));

            //frDashboard.Source = new Uri("ProfilePage.xaml", UriKind.Relative);

        }

        private void miQuestion_Click(object sender, RoutedEventArgs e)
        {
            //ExamsPage examsPage = new ExamsPage(3);

            frDashboard.Source = new Uri("ExamsPage.xaml", UriKind.Relative);

        }

    }
}
