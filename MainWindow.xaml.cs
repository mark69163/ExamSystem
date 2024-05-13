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
using Microsoft.VisualBasic.ApplicationServices;



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

        private bool userNameValidaton()
        {

            string userName = tbUsername.Text;
            string userPassword = pbPassword.Password;

            string pattern = @"^[a-zA-Z0-9]+$";

            // Teszt userek
            if (userName.ToUpper() == "TESTSTUDENT" || userName.ToUpper() == "PROGRAMMERSTUDENT")
            {
                lbLoginError.Visibility = Visibility.Hidden;
                return true; 
            }
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

        private bool userAuthentication()
        {
            List<STUDENT> students = _context.STUDENTs.ToList();
            int counter = 0;
            if (tbUsername.Text.ToUpper() == "TESTSTUDENT" || tbUsername.Text.ToUpper() == "PROGRAMMERSTUDENT")
                return true;
            foreach (STUDENT student in students)
            {
                string userName = tbUsername.Text.ToUpper();
                string userPassword = pbPassword.Password;
                userPassword = HashPassword(userPassword);

                //all good
                if (userName == student.neptun_id && userPassword == student.hash_password)
                {
                    //User.userPassword = userPassword;
                    //User.userName = userName;
                    return true;
                }
                else if (userName == student.neptun_id) counter++;


            }

            if (counter == 1) lbLoginError.Content = "Incorrect Password!";
            else lbLoginError.Content = "User does not exists!";

            lbLoginError.Visibility = Visibility.Visible;

            return false;
        }


        //login bypass
        private void btLogin_Click(object sender, RoutedEventArgs e)
        {
            if (userAuthentication())
            {
                User = new LoggedInUser();
                User.userName = tbUsername.Text.ToUpper();

                DashboardWindow dashboardWindow = new DashboardWindow(User);
                dashboardWindow.Show();
                //close login window
                Close();
            }


        }


        private void pbPassword_KeyUp(object sender, KeyEventArgs e)
        {
            userNameValidaton();

            if (e.Key == Key.Return && userNameValidaton())
            {
                if (userAuthentication())
                {
                    User = new LoggedInUser();
                    User.userName = tbUsername.Text.ToUpper();



                    DashboardWindow dashboardWindow = new DashboardWindow(User);
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
                if (userAuthentication())
                {
                    User = new LoggedInUser();
                    User.userName = tbUsername.Text.ToUpper();



                    DashboardWindow dashboardWindow = new DashboardWindow(User);
                    dashboardWindow.Show();

                    //close login window
                    Close();


                }

            }
        }
    }
}

