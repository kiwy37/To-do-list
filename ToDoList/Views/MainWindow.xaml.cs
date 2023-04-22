using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using ToDoList.Models;

namespace ToDoList.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void treeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var selectedItem = treeView.SelectedItem;
            if (selectedItem != null && selectedItem is TDL)
            {
                ClasaDeBinding.selectedTDL = (TDL)selectedItem;
            }
        }
        private void Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void About(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Nume: Bajan Ramona-Maria\n Grupa: 12LF211\n Adresa de mail: ramona.bajan@student.unitbv.ro");
        }
        private void ArchiveDatabase(object sender, RoutedEventArgs e)
        {
            ClasaDeBinding.WriteDataToXml();
        }
        private void AddTask(object sender, RoutedEventArgs e)
        {
            AddTask a = new AddTask();
            a.ShowDialog();
            Task t = new Task(a.name.Text, a.description.Text, Status.Created, (Priority)Enum.Parse(typeof(Priority), a.priority.Text), a.deadline.SelectedDate.Value, DateTime.Now, a.category.Text);
            ClasaDeBinding.selectedTDL.m_Content_Task.Add(t);
        }
        private void DeleteTDL(object sender, RoutedEventArgs e)
        {
            ClasaDeBinding.DeleteTDL();
            ClasaDeBinding.InitializePanel();
            ClasaDeBinding.UpdatePanel(ClasaDeBinding.m_TDL);
        }
        private void EditTDL(object sender, RoutedEventArgs e)
        {
            AddTDL a = new AddTDL(ClasaDeBinding.selectedTDL.name, ClasaDeBinding.selectedTDL.image);
            a.ShowDialog();
            ClasaDeBinding.selectedTDL.Name = a.m_name;
            ClasaDeBinding.selectedTDL.Image = a.m_image;
        }
        private void MoveUpTDL(object sender, RoutedEventArgs e)
        {
            ClasaDeBinding.MoveUpTDL();
        }
        private void MoveDownTDL(object sender, RoutedEventArgs e)
        {
            ClasaDeBinding.MoveDownTDL();
        }
        private void ChangePathTDL(object sender, RoutedEventArgs e)
        {
            ChangePath cp = new ChangePath();
            cp.ShowDialog();
            string newFolder = cp.daddyFolder.Text;

            TDL deAdaugat = new TDL(ClasaDeBinding.selectedTDL);

            //delete

            TDL tdlToDelete = ClasaDeBinding.FindTDLByName(ClasaDeBinding.selectedTDL.name, ClasaDeBinding.m_TDL);
            if (tdlToDelete != null)
            {
                TDL parentTDL = ClasaDeBinding.FindParentTDL(tdlToDelete, ClasaDeBinding.m_TDL);
                if (parentTDL != null)
                {
                    parentTDL.m_Content_TDL.Remove(tdlToDelete);
                }
                else
                {
                    ClasaDeBinding.m_TDL.Remove(tdlToDelete);
                }
            }

            //replace

            if (newFolder.Length != 0)
            {
                TDL tfind = ClasaDeBinding.FindTDLByName(newFolder, ClasaDeBinding.m_TDL);
                tfind.Content_TDL.Add(deAdaugat);
            }
            else
            {
                ClasaDeBinding.m_TDL.Add(deAdaugat);
            }

        }
        private void TT_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                var selectedTask = (Task)e.AddedItems[0];
                ClasaDeBinding.selectedTask = selectedTask;
            }
        }
        private void EditTask(object sender, RoutedEventArgs e)
        {
            AddTask a = new AddTask(ClasaDeBinding.selectedTask.name, ClasaDeBinding.selectedTask.description, ClasaDeBinding.selectedTask.deadline, ClasaDeBinding.selectedTask.category, ClasaDeBinding.selectedTask.priority);
            a.ShowDialog();
            ClasaDeBinding.selectedTask.Name = a.name.Text;
            ClasaDeBinding.selectedTask.Description = a.description.Text;
            ClasaDeBinding.selectedTask.Deadline = a.deadline.SelectedDate.Value;
            ClasaDeBinding.selectedTask.Category = a.category.Text;
            ClasaDeBinding.selectedTask.Priority = (Priority)Enum.Parse(typeof(Priority), a.priority.SelectedValue.ToString());
            ClasaDeBinding.InitializePanel();
            ClasaDeBinding.UpdatePanel(ClasaDeBinding.m_TDL);
        }
        private void DeleteTask(object sender, RoutedEventArgs e)
        {
            TDL parentTDL = ClasaDeBinding.FindParentTask(ClasaDeBinding.selectedTask, ClasaDeBinding.m_TDL);
            if (parentTDL != null)
            {
                parentTDL.m_Content_Task.Remove(ClasaDeBinding.selectedTask);
            }
            ClasaDeBinding.InitializePanel();
            ClasaDeBinding.UpdatePanel(ClasaDeBinding.m_TDL);
        }
        private void SetDone(object sender, RoutedEventArgs e)
        {
            ClasaDeBinding.selectedTask.FinalisedDate = DateTime.Now;
            ClasaDeBinding.selectedTask.Status = Status.Done;
            ClasaDeBinding.InitializePanel();
            ClasaDeBinding.UpdatePanel(ClasaDeBinding.m_TDL);
        }
        private void SetInProgress(object sender, RoutedEventArgs e)
        {
            ClasaDeBinding.selectedTask.Status = Status.InProgress;
            ClasaDeBinding.InitializePanel();
            ClasaDeBinding.UpdatePanel(ClasaDeBinding.m_TDL);
        }
        private void MoveUpTask(object sender, RoutedEventArgs e)
        {
            ClasaDeBinding.MoveUpTask();
        }
        private void MoveDownTask(object sender, RoutedEventArgs e)
        {
            ClasaDeBinding.MoveDownTask();
        }
        private void ManageCategory(object sender, RoutedEventArgs e)
        {
            ManageCategory m = new ManageCategory();
            m.ShowDialog();
        }
        private void FindTask(object sender, RoutedEventArgs e)
        {
            SearchTask s = new SearchTask();
            s.ShowDialog();
        }
        private void FilterCategory(object sender, RoutedEventArgs e)
        {
            ICollectionView tasksView = CollectionViewSource.GetDefaultView(TT.ItemsSource);
            AddCategory a = new AddCategory();
            a.ShowDialog();
            tasksView.Filter = (task) => ((Task)task).category == a.CategoryTextBox.Text;
        }
        private void DoneFilter(object sender, RoutedEventArgs e)
        {
            ICollectionView tasksView = CollectionViewSource.GetDefaultView(TT.ItemsSource);
            tasksView.Filter = (task) => ((Task)task).status == Status.Done;
        }
        private void OutdatadFilter(object sender, RoutedEventArgs e)
        {
            ICollectionView tasksView = CollectionViewSource.GetDefaultView(TT.ItemsSource);
            tasksView.Filter = (task) => ((Task)task).finalisedDate > ((Task)task).finalisedDate;
        }
        private void UndoneOutdated(object sender, RoutedEventArgs e)
        {
            ICollectionView tasksView = CollectionViewSource.GetDefaultView(TT.ItemsSource);
            tasksView.Filter = (task) => ((Task)task).status != Status.Done && ((Task)task).deadline < DateTime.Now;
        }
        private void UndoneNotOutdated(object sender, RoutedEventArgs e)
        {
            ICollectionView tasksView = CollectionViewSource.GetDefaultView(TT.ItemsSource);
            tasksView.Filter = (task) => ((Task)task).status != Status.Done && ((Task)task).deadline >= DateTime.Now;
        }
        private void FilterByDeadline(object sender, RoutedEventArgs e)
        {
            ICollectionView tasksView = CollectionViewSource.GetDefaultView(TT.ItemsSource);
            tasksView.SortDescriptions.Clear();
            tasksView.SortDescriptions.Add(new SortDescription("deadline", ListSortDirection.Ascending));
        }
        private void FilterByPriority(object sender, RoutedEventArgs e)
        {
            ICollectionView tasksView = CollectionViewSource.GetDefaultView(TT.ItemsSource);
            tasksView.SortDescriptions.Clear();
            tasksView.SortDescriptions.Add(new SortDescription("priority", ListSortDirection.Ascending));
        }
        private void Statistics(object sender, RoutedEventArgs e)
        {
            if (StackPanelGrid.Visibility == Visibility.Collapsed)
                StackPanelGrid.Visibility = Visibility.Visible;
            else
                StackPanelGrid.Visibility = Visibility.Collapsed;
        }
    }
}