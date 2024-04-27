using Microsoft.Win32;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ExamSystem
{
    /// <summary>
    /// Interaction logic for ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {

        private bool imageCahnged = false;
        string userPictureUri = "/img/defaultUserPicture.png";

        private void loadPorfileData() {

            imUserPicture.Source = new BitmapImage(new Uri(userPictureUri, uriKind: UriKind.Relative));

          
        }

        public ProfilePage()
        {
            InitializeComponent();

            loadPorfileData();
        }

        private void btUserPictureEdit_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files|*.bmp;*.jpg;*.png";
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == true)
            {
                imUserPictureEdit.Source = new BitmapImage(new Uri(openFileDialog.FileName));
                imageCahnged=true;
            }
        }

        private void btSaveUserPicture_Click(object sender, RoutedEventArgs e)
        {
            if (imageCahnged==true) { 
                imUserPicture.Source = new BitmapImage(new Uri(imUserPictureEdit.Source.ToString()));
                userPictureUri = imUserPictureEdit.Source.ToString();

                imUserPictureEdit.Source = new BitmapImage(new Uri("/img/addUserPicture.png",uriKind:UriKind.Relative));
                imageCahnged =false;
            }
        }

        private void btCancelUserPicture_Click(object sender, RoutedEventArgs e)
        {
            if (imageCahnged == true)
            {
                imUserPictureEdit.Source = new BitmapImage(new Uri("/img/addUserPicture.png", uriKind: UriKind.Relative));
                imageCahnged = false;
            }
        }
    }
}
