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
using System.Text.RegularExpressions;
using Model;
using System.Security.Cryptography;



namespace ExamSystem
{
  
    public partial class MainWindow : Window
    {
        Model.Model _context;

        public LoggedInUser User { get; set; }

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


        public MainWindow()
        {
            InitializeComponent();

            _context = new Model.Model();

            User = new LoggedInUser();
            //user.userName = tbUsername.Text;
            //user.userPassword= pbPassword.Password;
        }

        //hashing
        public static string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Convert byte array to a string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private bool userNameValidaton() {

            string userName = tbUsername.Text;
            string userPassword = pbPassword.Password;

            string pattern = @"^[a-zA-Z0-9]+$";

            //nem 6 karakter hosszu
            if (userName.Length != 6)
            {
                lbLoginError.Content = "Username must be 6 chars long!";
                lbLoginError.Visibility = Visibility.Visible;
                return false;
            }

            //nincs specialis karakter
            else if (!Regex.IsMatch(userName, pattern))
            {
                lbLoginError.Content = "Username must be a Neptun code!";
                lbLoginError.Visibility = Visibility.Visible;
                return false;
            }
            //szintaktikailag helyes felhasznalo nev
            else
            {
                lbLoginError.Visibility = Visibility.Hidden;
                return true;
            }
        }

        private bool userAuthentication() {
            List<STUDENT> students = _context.STUDENTs.ToList();
            int counter = 0;

            foreach (STUDENT student in students) {
                string userName = tbUsername.Text.ToUpper();
                string userPassword = pbPassword.Password;
                userPassword = HashPassword(userPassword);
                Console.WriteLine();
                //all good
                if (userName == student.neptun_id && userPassword == student.hash_password)
                {
                    User.userPassword = userPassword;
                    User.userName = userName;
                    return true;
                }
                else if(userName == student.neptun_id)counter ++;


            }

            if (counter == 1) lbLoginError.Content = "Incorrect Password!";
            else lbLoginError.Content = "User does not exists!";

            lbLoginError.Visibility = Visibility.Visible;

            return false;
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
            userNameValidaton();

            if (e.Key == Key.Return && userNameValidaton())
            {
                if (userAuthentication())
                {
                    DashboardWindow dashboardWindow = new DashboardWindow();
                    dashboardWindow.Show();

                    //close login window
                    Close();
                }
            }
        }

        private void tbUsername_KeyUp(object sender, KeyEventArgs e)
        {
            userNameValidaton();

            if (e.Key == Key.Return && userNameValidaton())
            {
                if (userAuthentication()){
                    DashboardWindow dashboardWindow = new DashboardWindow();
                    dashboardWindow.Show();

                    //close login window
                    Close();
                }
            }
            
        }

    }
}

