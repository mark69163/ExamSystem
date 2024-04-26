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
        int correctCounter;
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
            this.NavigationService.Navigate(pageFunctionUri,correctCounter);
        }

        private void btExamStart2_Click(object sender, RoutedEventArgs e)
        {

        }

        private void pbExam1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            pbExam1.Value = correctCounter*20;
            lbExamPoints1.Content = correctCounter*20;
        }
    }
}
