using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
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
using static System.Net.Mime.MediaTypeNames;
using System.Globalization;
using Path = System.IO.Path;
using Model;
using ExamSystem.Logic;

namespace ExamSystem
{
    public partial class ProfilePage : Page
    {
        Model.Model _context;

        public LoggedInUser CurrentUser { get; }


        private bool imageCahnged = false;
        //string userPictureUri = "/img/userPicture.png";
        private string fileName = "";
        private string sourceFilePath = "";
        private void loadPorfileData()
        {
           
            List<STUDENT> students = _context.STUDENTs.ToList();

            foreach (STUDENT student in students) {
                if (student.neptun_id == this.CurrentUser.userName) {
                    lbUserName.Content = student.neptun_id;
                    lbFirstName.Content = student.first_name;
                    lbLastName.Content = student.last_name;
                    lbState.Content = student.user_status;
                }
            }
          

        }

        public ProfilePage(LoggedInUser User)
        {
            InitializeComponent();

            _context = new Model.Model();

            CurrentUser = User;

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
                fileName = openFileDialog.FileName;
                sourceFilePath = fileName;

                fileName = System.IO.Path.GetFileName(fileName);
                imageCahnged = true;
            }
        }

        private void btSaveUserPicture_Click(object sender, RoutedEventArgs e)
        {
            if (imageCahnged)
            {
                try
                {
                    string destinationFolder = Path.Combine(Environment.CurrentDirectory, "img");
                    string destinationPath = Path.Combine(destinationFolder, "userPicture.png");

                    imUserPictureEdit.Source = null;
                    imUserPicture.Source = null;


                    // Copy the new profile picture file to the destination folder
                    File.Copy(sourceFilePath, destinationPath, true);
                    Thread.Sleep(1000);

                    imUserPicture.Source = new BitmapImage(new Uri(sourceFilePath));
                    imUserPictureEdit.Source = new BitmapImage(new Uri("/img/addUserPicture.png", UriKind.Relative));

                    //MessageBox.Show("Profile Picture Updated Successfully!");
                    imageCahnged = true;
                }
                catch (IOException ex)
                {
                    MessageBox.Show("Unable to update profile picture. Please try again later.");

                }

            }
        }


        private void btCancelUserPicture_Click(object sender, RoutedEventArgs e)
        {
            if (imageCahnged)
            {
                imUserPictureEdit.Source = new BitmapImage(new Uri("/img/addUserPicture.png", uriKind: UriKind.Relative));
                imageCahnged = false;
            }
        }

        private void btResetUserPicture_Click(object sender, RoutedEventArgs e)
        {

            if(imageCahnged){
                try
                {
                    imUserPictureEdit.Source = null;
                    imUserPicture.Source = null;


                    string destinationFolder = Path.Combine(Environment.CurrentDirectory, "img");
                    string destinationPath = Path.Combine(destinationFolder, "userPicture.png");
                    sourceFilePath = Path.Combine(destinationFolder, "defaultUserPicture.png");

                    // Copy the new profile picture file to the destination folder
                    File.Copy(sourceFilePath, destinationPath, true);
                    Thread.Sleep(1000);

                    imUserPicture.Source = new BitmapImage(new Uri(sourceFilePath));
                    imUserPictureEdit.Source = new BitmapImage(new Uri("/img/addUserPicture.png", UriKind.Relative));

                    //MessageBox.Show("Profile Picture Updated Successfully!");
                    imageCahnged = false;

                }
                catch (IOException ex)
                {
                    MessageBox.Show("Unable to update profile picture. Please try again later.");

                }
            }
        }
    }
}
