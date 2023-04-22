using System.Windows;
using System.Windows.Controls;

namespace ToDoList.Views
{
    /// <summary>
    /// Interaction logic for ManageCategory.xaml
    /// </summary>
    public partial class ManageCategory : Window
    {
        private string selectedCategory;
        public ManageCategory()
        {
            InitializeComponent();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                selectedCategory = e.AddedItems[0].ToString();
            }
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            AddCategory a = new AddCategory();
            a.ShowDialog();
            ClasaDeBinding.categorii.Add(a.CategoryTextBox.Text);
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            ClasaDeBinding.categorii.Remove(selectedCategory);
            ClasaDeBinding.CangeCategory("", selectedCategory, ClasaDeBinding.m_TDL);
        }

        private void Change(object sender, RoutedEventArgs e)
        {
            AddCategory a = new AddCategory();
            a.ShowDialog();
            for (int i = 0; i < ClasaDeBinding.categorii.Count; i++)
            {
                if (ClasaDeBinding.categorii[i] == selectedCategory)
                {
                    ClasaDeBinding.categorii[i] = a.CategoryTextBox.Text;
                    break;
                }
            }
            ClasaDeBinding.CangeCategory(a.CategoryTextBox.Text, selectedCategory, ClasaDeBinding.m_TDL);
        }
    }
}
