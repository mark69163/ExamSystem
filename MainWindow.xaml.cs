using System.Text;
using System.Windows;
using System.Windows.Controls;

using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Globalization;
using System.Configuration;
using System.Collections.ObjectModel;
using ExamSystem.Logic;


namespace ExamSystem
{
  
    public partial class MainWindow : Window
    {
        public LoggedInUser user { get; set; }

        // Define the UserName property
        public string UserName
        {
            get { return (string)GetValue(UserNameProperty); }
            set { SetValue(UserNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for UserName. 
        // This enables binding.
        public static readonly DependencyProperty UserNameProperty =
            DependencyProperty.Register("UserName", typeof(string), typeof(MainWindow), new PropertyMetadata(""));

        private void userAuthentication() {
            if (tbUsername.Text.ToUpper() == "ADMIN" && pbPassword.Password.ToUpper() == "ADMIN")
            {
                //open the new window
                DashboardWindow dashboardWindow = new DashboardWindow();
                dashboardWindow.Show();

                //close login window
                Close();
            }
            else
            {
                lbLoginError.Visibility = Visibility.Visible;
            }
        }
        public MainWindow()
        {
            InitializeComponent();

            user = new LoggedInUser();
            //user.userName = tbUsername.Text;
            //user.userPassword= pbPassword.Password;
        }

        private void btLogin_Click(object sender, RoutedEventArgs e)
        {
            //userAuthentication();

            //authentication bypass
            DashboardWindow dashboardWindow = new DashboardWindow();
            dashboardWindow.Show();

            //close login window
            Close();
        }

    

        private void pbPassword_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return) {
               userAuthentication();
            }
        }

        private void tbUsername_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
              
                userAuthentication();
            }
            
        }

    }
}

