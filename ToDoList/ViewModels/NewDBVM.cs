using System;
using ToDoList.Command;
using ToDoList.Pages;

namespace ToDoList.ViewModels
{
    internal class NewDBVM
    {
        public NewDBCommand CommandNewDB { get; set; }

        public NewDBVM()
        {
            CommandNewDB = new NewDBCommand(this);
        }
        public void OnExecute()
        {
            NewDatebase newDB = new NewDatebase();
            newDB.ShowDialog();
            string s = newDB.databaseName.Text;
            ClasaDeBinding.m_databaseName = @"..\..\DB\" + s + ".xml";
            ClasaDeBinding.LoadDataFromXml();
            ClasaDeBinding.InitializePanel();
            ClasaDeBinding.UpdatePanel(ClasaDeBinding.m_TDL);
            Console.WriteLine("Begin: ");
            Console.WriteLine(ClasaDeBinding.m_TDL.Count);
        }
    }
}
