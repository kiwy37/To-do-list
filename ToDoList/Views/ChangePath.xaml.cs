using System.Windows;

namespace ToDoList.Views
{
    /// <summary>
    /// Interaction logic for ChangePath.xaml
    /// </summary>
    public partial class ChangePath : Window
    {
        public ChangePath()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
