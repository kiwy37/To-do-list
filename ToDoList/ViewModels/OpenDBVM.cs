using ToDoList.Command;
using ToDoList.Views;

namespace ToDoList.ViewModels
{
    internal class OpenDBVM
    {
        public OpenDBCommand CommandOpenDB { get; set; }

        public OpenDBVM()
        {
            CommandOpenDB = new OpenDBCommand(this);
        }
        public void OnExecute()
        {
            ClasaDeBinding.m_TDL.Clear();
            OpenDatabase openDB = new OpenDatabase();
            openDB.ShowDialog();
            string s = openDB.databaseName.ToString();
            ClasaDeBinding.m_databaseName = @"..\..\DB\" + s + ".xml";
            ClasaDeBinding.LoadDataFromXml();
            ClasaDeBinding.InitializePanel();
            ClasaDeBinding.UpdatePanel(ClasaDeBinding.m_TDL);
        }
    }
}
