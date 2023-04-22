using System.Collections.Generic;
using System.Windows;
using ToDoList.Models;

namespace ToDoList.Views
{
    /// <summary>
    /// Interaction logic for SearchTask.xaml
    /// </summary>
    public partial class SearchTask : Window
    {
        public SearchTask()
        {
            InitializeComponent();
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            HashSet<Task> set = new HashSet<Task>();
            ClasaDeBinding.foundedTasks.Clear();
            if (dateBox.SelectedDate == null && nameBox.Text == "")
            {
                MessageBox.Show("Complete at least one field!");
            }
            else if (dateBox.SelectedDate == null)
            {
                ClasaDeBinding.Search(nameBox.Text, ClasaDeBinding.m_TDL, set);
                ClasaDeBinding.ColpleteParent();
            }
            else if (nameBox.Text == "")
            {
                ClasaDeBinding.Search(dateBox.SelectedDate.Value, ClasaDeBinding.m_TDL, set);
                ClasaDeBinding.ColpleteParent();
            }
            else
            {
                ClasaDeBinding.Search(nameBox.Text, dateBox.SelectedDate.Value, ClasaDeBinding.m_TDL, set);
                ClasaDeBinding.ColpleteParent();
            }
        }
    }
}
