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



namespace ExamSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    class ErrorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var sb = new StringBuilder();
            var hibak = value as ReadOnlyCollection<ValidationError>;
            if (hibak != null)
            {
                foreach (var e in hibak.Where(e => e.ErrorContent != null))
                {
                    sb.AppendLine(e.ErrorContent.ToString());
                }
            }
            return sb.ToString();

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class UserNameValidationRule : ValidationRule
    {
        public string UserName { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var str = value as string;
            if (str == null)
            {
                return new ValidationResult(false, "Please enter the user name!");
            }
            if (UserName.Length != 6)
            {
                return new ValidationResult(false, String.Format("The user name must be 6 characters long!"));
            }

            return new ValidationResult(true, null);

        }
    }




    public partial class MainWindow : Window
    {
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

