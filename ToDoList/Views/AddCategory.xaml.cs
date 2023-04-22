using System.Windows;

namespace ToDoList.Views
{
    /// <summary>
    /// Interaction logic for AddCategory.xaml
    /// </summary>
    public partial class AddCategory : Window
    {
        public AddCategory()
        {
            InitializeComponent();
        }

        private void OK(object sender, RoutedEventArgs e)
        {
            if (CategoryTextBox.Text.Length == 0)
            {
                MessageBox.Show("Introduceti o categorie");
            }
            else
            {
                Close();
            }
        }
    }
}
