using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace ToDoList.Views
{
    public partial class OpenDatabase : Window
    {
        public List<string> Names { get; set; }
        public string databaseName { get; set; }

        public OpenDatabase()
        {
            InitializeComponent();
            Names = GetFileNames(@"..\..\DB");
            DataContext = this;
        }

        private List<string> GetFileNames(string folderPath)
        {
            var fileNames = new List<string>();
            try
            {
                var files = Directory.GetFiles(folderPath);
                foreach (var file in files)
                {
                    string s = Path.GetFileName(file);
                    s = s.Substring(0, s.Length - 4);
                    fileNames.Add(s);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while reading folder: {ex.Message}");
            }
            return fileNames;
        }

        private void NameList_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            var selectedName = NameList.SelectedItem as string;
            if (selectedName != null)
            {
                databaseName = selectedName;
                Close();
            }
        }
    }
}
