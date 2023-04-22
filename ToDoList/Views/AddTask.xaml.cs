using System;
using System.Linq;
using System.Windows;

namespace ToDoList.Views
{
    /// <summary>
    /// Interaction logic for AddTask.xaml
    /// </summary>
    public partial class AddTask : Window
    {
        public AddTask()
        {
            InitializeComponent();
            category.ItemsSource = ClasaDeBinding.categorii;
            priority.ItemsSource = Enum.GetValues(typeof(Priority)).Cast<Priority>();
        }

        public AddTask(string nume, string descriere, DateTime termenLimita, string categorie, Priority prioritate)
        {
            InitializeComponent();
            category.ItemsSource = ClasaDeBinding.categorii;
            priority.ItemsSource = Enum.GetValues(typeof(Priority)).Cast<Priority>();
            name.Text = nume;
            description.Text = descriere;
            deadline.Text = termenLimita.ToString();
            category.Text = categorie;
            priority.Text = prioritate.ToString();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
