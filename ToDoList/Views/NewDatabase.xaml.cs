using System.Windows;
using ToDoList.Models;

namespace ToDoList.Pages
{
    /// <summary>
    /// Interaction logic for NewDatebase.xaml
    /// </summary>
    public partial class NewDatebase : Window
    {
        private readonly TDL aux = new TDL();
        public string DatabaseName { get; set; }

        public NewDatebase()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DatabaseName = databaseName.Text;
            string filePath = databaseName.Text + ".xml";
            aux.CreateDB(filePath);
            Close();
        }
    }
}
