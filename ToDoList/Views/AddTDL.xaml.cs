using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media.Imaging;
using ToDoList.Models;

namespace ToDoList.Views
{
    /// <summary>
    /// Interaction logic for AddTDL.xaml
    /// </summary>
    public partial class AddTDL : Window
    {
        public string m_image { set; get; }
        public string m_name { set; get; }
        private int currentImageIndex = 0;
        private const int m_numberOfPics = 7;

        public AddTDL()
        {
            InitializeComponent();
        }

        public AddTDL(string nume, string imagine)
        {
            InitializeComponent();
            numeText.Text = nume;
            m_name = nume;
            image.Source = new BitmapImage(new Uri(imagine, UriKind.Relative));
            m_image = imagine;
        }

        //private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    TextBox textBox = (TextBox)sender;
        //    m_name = textBox.Text;
        //}

        private void BackButton(object sender, RoutedEventArgs e)
        {
            currentImageIndex--;
            if (currentImageIndex < 0)
                currentImageIndex = m_numberOfPics + currentImageIndex;
            currentImageIndex %= m_numberOfPics;
            m_image = $"../Pics/i{currentImageIndex + 1}.jpg";
            image.Source = new BitmapImage(new Uri($"../Pics/{m_image}", UriKind.Relative));
        }
        private void NextButton(object sender, RoutedEventArgs e)
        {
            currentImageIndex++;
            currentImageIndex %= m_numberOfPics;
            m_image = $"../Pics/i{currentImageIndex + 1}.jpg";
            image.Source = new BitmapImage(new Uri($"../Pics/{m_image}", UriKind.Relative));
        }
        private bool CheckIfTDLExists(string nameTDL, string imageTDL, ObservableCollection<TDL> tdlist)
        {
            foreach (var tdl in tdlist)
            {
                if (tdl.name == nameTDL && tdl.image == imageTDL)
                {
                    return true;
                }
                if (tdl.m_Content_TDL != null && CheckIfTDLExists(nameTDL, imageTDL, tdl.m_Content_TDL))
                {
                    return true;
                }
            }
            return false;
        }
        private void AddTDLButton(object sender, RoutedEventArgs e)
        {
            m_name = numeText.Text;
            if (CheckIfTDLExists(m_name, m_image, ClasaDeBinding.m_TDL))
            {
                MessageBox.Show("A TDL like this already exist.");
            }
            else
            {
                Close();
                return;
            }
        }
    }
}
