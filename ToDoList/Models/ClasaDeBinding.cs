using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Xml;
using ToDoList.Models;

namespace ToDoList
{
    internal class ClasaDeBinding
    {
        public static ObservableCollection<TDL> m_TDL { get; set; }
        public static TDL selectedTDL { get; set; }
        public static Task selectedTask { get; set; }
        public static string m_databaseName { get; set; }      //toata calea
        public static ObservableCollection<string> categorii { get; set; }
        public static ObservableCollection<Tuple<string, DateTime, string>> foundedTasks { get; set; }
        public static int dueToday, dueTomorrow, overdue, done, toBeDone;
        public static int DueToday
        {
            get { return dueToday; }
            set
            {
                dueToday = value;
                NotifyStaticPropertyChanged("DueToday");
            }
        }
        public static int DueTomorrow
        {
            get { return dueTomorrow; }
            set
            {
                dueTomorrow = value;
                NotifyStaticPropertyChanged("DueTomorrow");
            }
        }
        public static int Overdue
        {
            get { return overdue; }
            set
            {
                overdue = value;
                NotifyStaticPropertyChanged("Overdue");
            }
        }
        public static int Done
        {
            get { return done; }
            set
            {
                done = value;
                NotifyStaticPropertyChanged("Done");
            }
        }
        public static int ToBeDone
        {
            get { return toBeDone; }
            set
            {
                toBeDone = value;
                NotifyStaticPropertyChanged("ToBeDone");
            }
        }
        public static void InitializePanel()
        {
            DueToday = 0;
            DueTomorrow = 0;
            Overdue = 0;
            Done = 0;
            ToBeDone = 0;
        }
        public static event PropertyChangedEventHandler StaticPropertyChanged;
        public static void NotifyStaticPropertyChanged(string propertyName)
        {
            StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(propertyName));
        }
        public static event EventHandler MyStaticFieldChanged;
        public static ObservableCollection<TDL> MyStaticProperty
        {
            get => m_TDL;
            set
            {
                if (m_TDL != value)
                {
                    m_TDL = value;
                    OnMyStaticFieldChanged();
                }
            }
        }
        private static void OnMyStaticFieldChanged()
        {
            MyStaticFieldChanged?.Invoke(null, EventArgs.Empty);
        }
        public ClasaDeBinding()
        {
            if (m_TDL == null)
            {
                m_TDL = new ObservableCollection<TDL>();
            }
            if (foundedTasks == null)
            {
                foundedTasks = new ObservableCollection<Tuple<string, DateTime, string>>();
                dueToday = 0;
                dueTomorrow = 0;
                overdue = 0;
                done = 0;
                toBeDone = 0;
            }
            if (categorii == null)
            {
                categorii = new ObservableCollection<string> { "Curatenie", "Teme", "Extra", "Pentru mine" };
            }
        }
        private static void LoadTDL(XmlNode tdlNode, TDL parentTDL)
        {
            string tdlName = tdlNode.SelectSingleNode("name").InnerText;
            string tdlImage = tdlNode.SelectSingleNode("image").InnerText;

            TDL tdl = new TDL(tdlName, tdlImage);

            if (parentTDL != null)
            {
                parentTDL.addTDL(tdl);
            }
            else
            {
                m_TDL.Add(tdl);
            }

            XmlNodeList contentNodes = tdlNode.SelectNodes("content/*");
            foreach (XmlNode contentNode in contentNodes)
            {
                if (contentNode.Name == "TDL")
                {
                    LoadTDL(contentNode, tdl);
                }
                else if (contentNode.Name == "task")
                {
                    string taskName = contentNode.SelectSingleNode("taskname").InnerText;
                    string taskDesc = contentNode.SelectSingleNode("description").InnerText;
                    Status taskStatus = (Status)Enum.Parse(typeof(Status), contentNode.SelectSingleNode("status").InnerText);
                    Priority taskPriority = (Priority)Enum.Parse(typeof(Priority), contentNode.SelectSingleNode("priority").InnerText);
                    DateTime taskDeadline = DateTime.Parse(contentNode.SelectSingleNode("deadline").InnerText);
                    DateTime finalisedDate = DateTime.Parse(contentNode.SelectSingleNode("finalisedDate").InnerText);
                    string taskCategory = contentNode.SelectSingleNode("category").InnerText;

                    Task task = new Task(taskName, taskDesc, taskStatus, taskPriority, taskDeadline, finalisedDate, taskCategory);
                    tdl.addTask(task);
                }
                else if (contentNode.Name == "content")
                {
                    LoadContent(contentNode, tdl);
                }
            }

            XmlNodeList tdlNodes = tdlNode.SelectNodes("TDL");
            foreach (XmlNode childTdlNode in tdlNodes)
            {
                LoadTDL(childTdlNode, tdl);
            }
        }
        private static void LoadContent(XmlNode contentNode, TDL parentTDL)
        {
            string contentName = contentNode.SelectSingleNode("name").InnerText;
            string contentImage = contentNode.SelectSingleNode("image").InnerText;

            TDL content = new TDL(contentName, contentImage);
            parentTDL.addTDL(content);

            XmlNodeList childContentNodes = contentNode.SelectNodes("content/*");
            foreach (XmlNode childContentNode in childContentNodes)
            {
                if (childContentNode.Name == "TDL")
                {
                    LoadTDL(childContentNode, content);
                }
                else if (childContentNode.Name == "task")
                {
                    string taskName = childContentNode.SelectSingleNode("taskname").InnerText;
                    string taskDesc = childContentNode.SelectSingleNode("description").InnerText;
                    Status taskStatus = (Status)Enum.Parse(typeof(Status), childContentNode.SelectSingleNode("status").InnerText);
                    Priority taskPriority = (Priority)Enum.Parse(typeof(Priority), childContentNode.SelectSingleNode("priority").InnerText);
                    DateTime taskDeadline = DateTime.Parse(childContentNode.SelectSingleNode("deadline").InnerText);
                    DateTime finalisedDate = DateTime.Parse(childContentNode.SelectSingleNode("finalisedDate").InnerText);
                    string taskCategory = childContentNode.SelectSingleNode("category").InnerText;

                    Task task = new Task(taskName, taskDesc, taskStatus, taskPriority, taskDeadline, finalisedDate, taskCategory);
                    content.addTask(task);
                }
                else
                {
                    LoadContent(childContentNode, content);
                }
            }
        }
        public static void LoadDataFromXml()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(m_databaseName);

            XmlNodeList tdlNodes = doc.SelectNodes("/database/TDL");
            foreach (XmlNode tdlNode in tdlNodes)
            {
                LoadTDL(tdlNode, null);
            }
        }
        public static void WriteDataToXml()
        {
            XmlDocument doc = new XmlDocument();

            XmlElement databaseElement = doc.CreateElement("database");
            doc.AppendChild(databaseElement);

            foreach (TDL tdl in m_TDL)
            {
                XmlElement tdlElement = doc.CreateElement("TDL");
                databaseElement.AppendChild(tdlElement);

                XmlElement nameElement = doc.CreateElement("name");
                nameElement.InnerText = tdl.name;
                tdlElement.AppendChild(nameElement);

                XmlElement imageElement = doc.CreateElement("image");
                imageElement.InnerText = tdl.image;
                tdlElement.AppendChild(imageElement);

                XmlElement contentElement = doc.CreateElement("content");
                tdlElement.AppendChild(contentElement);

                foreach (TDL childTDL in tdl.m_Content_TDL)
                {
                    WriteTDLToXml(doc, contentElement, childTDL);
                }

                foreach (Task task in tdl.m_Content_Task)
                {
                    WriteTaskToXml(doc, contentElement, task);
                }
            }

            doc.Save(m_databaseName);
        }
        private static void WriteTDLToXml(XmlDocument doc, XmlElement parentElement, TDL tdl)
        {
            XmlElement tdlElement = doc.CreateElement("TDL");
            parentElement.AppendChild(tdlElement);

            XmlElement nameElement = doc.CreateElement("name");
            nameElement.InnerText = tdl.name;
            tdlElement.AppendChild(nameElement);

            XmlElement imageElement = doc.CreateElement("image");
            imageElement.InnerText = tdl.image;
            tdlElement.AppendChild(imageElement);

            XmlElement contentElement = doc.CreateElement("content");
            tdlElement.AppendChild(contentElement);

            foreach (TDL childTDL in tdl.m_Content_TDL)
            {
                WriteTDLToXml(doc, contentElement, childTDL);
            }

            foreach (Task task in tdl.m_Content_Task)
            {
                WriteTaskToXml(doc, contentElement, task);
            }
        }
        private static void WriteTaskToXml(XmlDocument doc, XmlElement parentElement, Task task)
        {
            XmlElement taskElement = doc.CreateElement("task");
            parentElement.AppendChild(taskElement);

            XmlElement nameElement = doc.CreateElement("taskname");
            nameElement.InnerText = task.name;
            taskElement.AppendChild(nameElement);

            XmlElement descriptionElement = doc.CreateElement("description");
            descriptionElement.InnerText = task.description;
            taskElement.AppendChild(descriptionElement);

            XmlElement statusElement = doc.CreateElement("status");
            statusElement.InnerText = task.status.ToString();
            taskElement.AppendChild(statusElement);

            XmlElement priorityElement = doc.CreateElement("priority");
            priorityElement.InnerText = task.priority.ToString();
            taskElement.AppendChild(priorityElement);

            XmlElement deadlineElement = doc.CreateElement("deadline");
            deadlineElement.InnerText = task.deadline.ToString();
            taskElement.AppendChild(deadlineElement);

            XmlElement finalisedDateElement = doc.CreateElement("finalisedDate");
            finalisedDateElement.InnerText = task.finalisedDate.ToString();
            taskElement.AppendChild(finalisedDateElement);

            XmlElement categoryElement = doc.CreateElement("category");
            categoryElement.InnerText = task.category.ToString();
            taskElement.AppendChild(categoryElement);
        }
        public static void CangeCategory(string newCategory, string oldCategory, ObservableCollection<TDL> tdlCollection)
        {
            foreach (TDL tdl in tdlCollection)
            {
                for (int i = 0; i < tdl.Content_Task.Count; i++)
                    if (tdl.Content_Task[i].Category == oldCategory)
                    {
                        tdl.Content_Task[i].Category = newCategory;
                    }

                CangeCategory(newCategory, oldCategory, tdl.m_Content_TDL);
            }
        }
        public static void Search(DateTime date, ObservableCollection<TDL> tdlCollection, HashSet<Task> set)
        {
            foreach (TDL tdl in tdlCollection)
            {
                for (int i = 0; i < tdl.Content_Task.Count; i++)
                    if (tdl.Content_Task[i].deadline == date)
                    {
                        int oldDim = set.Count;
                        set.Add(tdl.Content_Task[i]);
                        if (oldDim != set.Count)
                        {
                            foundedTasks.Add(new Tuple<string, DateTime, string>(tdl.Content_Task[i].name, tdl.Content_Task[i].deadline, ""));
                        }
                    }
                Search(date, tdl.m_Content_TDL, set);
            }
        }
        public static void Search(string name, ObservableCollection<TDL> tdlCollection, HashSet<Task> set)
        {
            foreach (TDL tdl in tdlCollection)
            {
                for (int i = 0; i < tdl.Content_Task.Count; i++)
                    if (tdl.Content_Task[i].name == name)
                    {
                        int oldDim = set.Count;
                        set.Add(tdl.Content_Task[i]);
                        if (oldDim != set.Count)
                        {
                            foundedTasks.Add(new Tuple<string, DateTime, string>(tdl.Content_Task[i].name, tdl.Content_Task[i].deadline, ""));
                        }
                    }
                Search(name, tdl.m_Content_TDL, set);
            }
        }
        public static void Search(string name, DateTime date, ObservableCollection<TDL> tdlCollection, HashSet<Task> set)
        {
            foreach (TDL tdl in tdlCollection)
            {
                for (int i = 0; i < tdl.Content_Task.Count; i++)
                    if (tdl.Content_Task[i].deadline == date && tdl.Content_Task[i].name == name)
                    {
                        int oldDim = set.Count;
                        set.Add(tdl.Content_Task[i]);
                        if (oldDim != set.Count)
                        {
                            foundedTasks.Add(new Tuple<string, DateTime, string>(tdl.Content_Task[i].name, tdl.Content_Task[i].deadline, ""));
                        }
                    }
                Search(name, date, tdl.m_Content_TDL, set);
            }
        }
        public static TDL FindParentTask(Task childTask, ObservableCollection<TDL> tdlCollection)
        {
            foreach (TDL tdl in tdlCollection)
            {
                if (tdl.m_Content_Task.Contains(childTask))
                {
                    return tdl;
                }
                else
                {
                    TDL parentTDL = FindParentTask(childTask, tdl.m_Content_TDL);
                    if (parentTDL != null)
                    {
                        return parentTDL;
                    }
                }
            }
            return null;
        }
        public static TDL FindParentTDL(TDL childTDL, ObservableCollection<TDL> tdlCollection)
        {
            foreach (TDL tdl in tdlCollection)
            {
                if (tdl.m_Content_TDL.Contains(childTDL))
                {
                    return tdl;
                }
                else
                {
                    TDL parentTDL = FindParentTDL(childTDL, tdl.m_Content_TDL);
                    if (parentTDL != null)
                    {
                        return parentTDL;
                    }
                }
            }
            return null;
        }
        public static TDL FindParentTaskByName(string childTask, ObservableCollection<TDL> tdlCollection)
        {
            foreach (TDL tdl in tdlCollection)
            {
                bool ok = false;
                for (int i = 0; i < tdl.m_Content_Task.Count; i++)
                {
                    if (tdl.m_Content_Task[i].Name == childTask)
                        ok = true;
                }
                if (ok)
                {
                    return tdl;
                }
                else
                {
                    TDL parentTDL = FindParentTaskByName(childTask, tdl.m_Content_TDL);
                    if (parentTDL != null)
                    {
                        return parentTDL;
                    }
                }
            }
            return null;
        }
        public static TDL FindTDLByName(string name, ObservableCollection<TDL> tdlCollection)
        {
            foreach (TDL tdl in tdlCollection)
            {
                if (tdl.name == name)
                {
                    return tdl;
                }
                else
                {
                    TDL subTDL = FindTDLByName(name, tdl.m_Content_TDL);
                    if (subTDL != null)
                    {
                        return subTDL;
                    }
                }
            }
            return null;
        }
        public static void ColpleteParent()
        {
            for (int i = 0; i < foundedTasks.Count; i++)
            {
                TDL parent = FindParentTaskByName(foundedTasks[i].Item1, m_TDL);
                string p = parent.Name;
                parent = FindParentTDL(parent, m_TDL);
                while (parent != null)
                {
                    p = parent.Name + "->" + p;
                    parent = FindParentTDL(parent, m_TDL);
                }
                Tuple<string, DateTime, string> t = new Tuple<string, DateTime, string>(foundedTasks[i].Item1, foundedTasks[i].Item2, p);
                foundedTasks[i] = t;
            }
        }
        public static void UpdatePanel(ObservableCollection<TDL> tdlCollection)
        {
            foreach (TDL tdl in tdlCollection)
            {
                for (int i = 0; i < tdl.Content_Task.Count; i++)
                {
                    if (tdl.Content_Task[i].deadline == DateTime.Today)
                    {
                        DueToday++;
                    }
                    if (tdl.Content_Task[i].deadline == DateTime.Today.AddDays(1))
                    {
                        DueTomorrow++;
                    }
                    if (tdl.Content_Task[i].deadline < tdl.Content_Task[i].finalisedDate)
                    {
                        Overdue++;
                    }
                    if (tdl.Content_Task[i].status == Status.Done)
                    {
                        Done++;
                    }
                    else
                    {
                        ToBeDone++;
                    }
                }
                UpdatePanel(tdl.m_Content_TDL);
            }
        }
        public static void DeleteTDL()
        {
            if (selectedTDL != null)
            {
                TDL parentTDL = FindParentTDL(selectedTDL, m_TDL);
                if (parentTDL != null)
                {
                    parentTDL.m_Content_TDL.Remove(selectedTDL);
                }
                else
                {
                    m_TDL.Remove(selectedTDL);
                }
            }
        }
        public static void MoveUpTDL()
        {
            TDL parentTDL = FindParentTDL(selectedTDL, m_TDL);
            if (parentTDL != null)
            {
                for (int i = 0; i < parentTDL.m_Content_TDL.Count; i++)
                {
                    if (parentTDL.m_Content_TDL[i] == selectedTDL)
                    {
                        if (i == 0)
                        {
                            MessageBox.Show("Nu se poate muta mai sus.");
                            return;
                        }
                        TDL aux = new TDL(parentTDL.Content_TDL[i - 1]);
                        parentTDL.Content_TDL[i - 1] = parentTDL.Content_TDL[i];
                        parentTDL.Content_TDL[i] = aux;
                        selectedTDL = parentTDL.Content_TDL[i - 1];
                        return;
                    }
                }
            }
            else
            {
                for (int i = 0; i < m_TDL.Count; i++)
                {
                    if (m_TDL[i] == selectedTDL)
                    {
                        if (i == 0)
                        {
                            MessageBox.Show("Nu se poate muta mai sus.");
                            return;
                        }
                        TDL aux = new TDL(MyStaticProperty[i - 1]);
                        MyStaticProperty[i - 1] = MyStaticProperty[i];
                        MyStaticProperty[i] = aux;
                        selectedTDL = MyStaticProperty[i - 1];
                        return;
                    }
                }
            }
        }
        public static void MoveDownTDL()
        {
            TDL parentTDL = FindParentTDL(selectedTDL, m_TDL);
            if (parentTDL != null)
            {
                for (int i = 0; i < parentTDL.m_Content_TDL.Count; i++)
                {
                    if (parentTDL.m_Content_TDL[i] == selectedTDL)
                    {
                        if (i == parentTDL.m_Content_TDL.Count)
                        {
                            MessageBox.Show("Nu se poate muta mai jos.");
                            return;
                        }
                        TDL aux = new TDL(parentTDL.Content_TDL[i + 1]);
                        parentTDL.Content_TDL[i + 1] = parentTDL.Content_TDL[i];
                        parentTDL.Content_TDL[i] = aux;
                        selectedTDL = parentTDL.Content_TDL[i + 1];
                        return;
                    }
                }
            }
            else
            {
                for (int i = 0; i < m_TDL.Count; i++)
                {
                    if (m_TDL[i] == selectedTDL)
                    {
                        if (i == m_TDL.Count)
                        {
                            MessageBox.Show("Nu se poate muta mai jos.");
                            return;
                        }
                        TDL aux = new TDL(MyStaticProperty[i + 1]);
                        MyStaticProperty[i + 1] = MyStaticProperty[i];
                        MyStaticProperty[i] = aux;
                        selectedTDL = MyStaticProperty[i + 1];
                        return;
                    }
                }
            }
        }
        public static void MoveUpTask()
        {
            TDL parentTDL = FindParentTask(selectedTask, m_TDL);
            if (parentTDL != null)
            {
                for (int i = 0; i < parentTDL.m_Content_Task.Count; i++)
                {
                    if (parentTDL.m_Content_Task[i] == selectedTask)
                    {
                        if (i == 0)
                        {
                            MessageBox.Show("Nu se poate muta mai sus.");
                            return;
                        }
                        Task aux = new Task(parentTDL.m_Content_Task[i - 1]);
                        parentTDL.m_Content_Task[i - 1] = parentTDL.m_Content_Task[i];
                        parentTDL.m_Content_Task[i] = aux;
                        selectedTask = parentTDL.m_Content_Task[i - 1];
                        return;
                    }
                }
            }
        }
        public static void MoveDownTask()
        {
            TDL parentTDL = FindParentTask(selectedTask, m_TDL);
            if (parentTDL != null)
            {
                for (int i = 0; i < parentTDL.m_Content_Task.Count; i++)
                {
                    if (parentTDL.m_Content_Task[i] == selectedTask)
                    {
                        if (i == parentTDL.m_Content_Task.Count)
                        {
                            MessageBox.Show("Nu se poate muta mai jos.");
                            return;
                        }
                        Task aux = new Task(parentTDL.m_Content_Task[i + 1]);
                        parentTDL.m_Content_Task[i + 1] = parentTDL.m_Content_Task[i];
                        parentTDL.m_Content_Task[i] = aux;
                        selectedTask = parentTDL.m_Content_Task[i + 1];
                        return;
                    }
                }
            }
        }
    }
}