using System;
using System.Collections.Generic;
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

namespace ExamSystem
{
    /// <summary>
    /// Interaction logic for ExamsPage.xaml
    /// </summary>
    public partial class ExamsPage : Page
    {
        public ExamsPage()
        {
            InitializeComponent();
        }

        private void btExamStart0_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btExamStart1_Click(object sender, RoutedEventArgs e)
        {
            Uri pageFunctionUri = new Uri("examPage.xaml", UriKind.Relative);
            this.NavigationService.Navigate(pageFunctionUri);
        }

        private void btExamStart2_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
