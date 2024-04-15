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

namespace ExamSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btLogin_Click(object sender, RoutedEventArgs e)
        {
            /*authenttication code here*/
            //if authetntication passes, then upon clicking the login button close the current form and open the "main" window

            //open the new window
            DashboardWindow dashboardWindow = new DashboardWindow();
            dashboardWindow.Show();

            //close login window
            Close();

        }
    }
}