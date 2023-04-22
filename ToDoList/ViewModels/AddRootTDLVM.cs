using System.Windows;
using ToDoList.Command;
using ToDoList.Models;
using ToDoList.Views;

namespace ToDoList.ViewModels
{
    internal class AddRootTDLVM
    {
        public AddRootTDLCommand CommandAddRootTDL { get; set; }

        public AddRootTDLVM()
        {
            CommandAddRootTDL = new AddRootTDLCommand(this);
        }
        public void OnExecute()
        {
            MessageBox.Show(ClasaDeBinding.m_databaseName);
            AddTDL newDB = new AddTDL();
            newDB.m_image = $"../Pics/i1.jpg";
            newDB.ShowDialog();
            TDL t = new TDL();
            t.name = newDB.m_name;
            t.image = newDB.m_image;
            ClasaDeBinding.m_TDL.Add(t);
        }
    }
}
