using System.Collections.ObjectModel;
using System.IO;
using System.Xml;
using TreeViewMVVM;

namespace ToDoList.Models
{
    internal class TDL : BaseVM
    {
        public string name { set; get; }
        public string image { set; get; }
        public ObservableCollection<TDL> m_Content_TDL { get; set; }
        public ObservableCollection<Task> m_Content_Task { get; set; }

        public TDL(TDL content)
        {
            name = content.name;
            image = content.image;
            m_Content_TDL = content.m_Content_TDL;
            m_Content_Task = content.m_Content_Task;
        }

        public void addTask(Task t)
        {
            m_Content_Task.Add(t);
        }

        public void addTDL(TDL t)
        {
            m_Content_TDL.Add(t);
        }

        public TDL()
        {
            m_Content_TDL = new ObservableCollection<TDL>();
            m_Content_Task = new ObservableCollection<Task>();
        }

        public ObservableCollection<TDL> Content_TDL
        {
            get
            {
                return m_Content_TDL;
            }
            set
            {
                m_Content_TDL = value;
                NotifyPropertyChanged("Content_TDL");
            }
        }

        public ObservableCollection<Task> Content_Task
        {
            get
            {
                return m_Content_Task;
            }
            set
            {
                m_Content_Task = value;
                NotifyPropertyChanged("Content_Task");
            }
        }

        public string Image
        {
            get
            {
                return image;
            }
            set
            {
                image = value;
                NotifyPropertyChanged("Image");
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                NotifyPropertyChanged("Name");
            }
        }

        public TDL(string itemName, ObservableCollection<TDL> subCollection)
        {
            m_Content_TDL = subCollection;
            name = itemName;
        }

        public TDL(string name, string image)
        {
            this.image = image;
            this.name = name;
            m_Content_TDL = new ObservableCollection<TDL>();
            m_Content_Task = new ObservableCollection<Task>();
        }

        public TDL(string name)
        {
            this.name = name;
            m_Content_TDL = new ObservableCollection<TDL>();
            m_Content_Task = new ObservableCollection<Task>();
        }
        public void CreateDB(string xmlFile)
        {
            string folderPath = @"..\..\DB";
            string fullPath = Path.Combine(folderPath, xmlFile);

            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("database");
            doc.AppendChild(root);
            doc.Save(fullPath);
        }
    }
}