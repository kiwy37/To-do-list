using System;
using TreeViewMVVM;

namespace ToDoList.Models
{
    internal class Task : BaseVM
    {
        //declarari
        public string name { set; get; }
        public string description { set; get; }
        public Status status { set; get; }
        public Priority priority { set; get; }
        public DateTime deadline { set; get; }
        public DateTime finalisedDate { set; get; }
        public string category { set; get; }

        public Task() { }
        public Task(Task task)
        {
            name = task.name;
            description = task.description;
            status = task.status;
            priority = task.priority;
            deadline = task.deadline;
            finalisedDate = task.finalisedDate;
            category = task.category;
        }
        public Task(string name, string description, Status status, Priority priority, DateTime deadline, DateTime finalisedDate, string category)
        {
            this.name = name;
            this.description = description;
            this.status = status;
            this.priority = priority;
            this.deadline = deadline;
            this.finalisedDate = finalisedDate;
            this.category = category;
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
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
                NotifyPropertyChanged("Description");
            }
        }
        public Status Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
                NotifyPropertyChanged("Status");
            }
        }
        public Priority Priority
        {
            get
            {
                return priority;
            }
            set
            {
                priority = value;
                NotifyPropertyChanged("Priority");
            }
        }
        public DateTime Deadline
        {
            get
            {
                return deadline;
            }
            set
            {
                deadline = value;
                NotifyPropertyChanged("Deadline");
            }
        }
        public DateTime FinalisedDate
        {
            get
            {
                return finalisedDate;
            }
            set
            {
                finalisedDate = value;
                NotifyPropertyChanged("FinalisedDate");
            }
        }
        public string Category
        {
            get
            {
                return category;
            }
            set
            {
                category = value;
                NotifyPropertyChanged("Category");
            }
        }
    }
}