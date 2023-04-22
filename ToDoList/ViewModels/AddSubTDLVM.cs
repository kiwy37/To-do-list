using ToDoList.Command;
using ToDoList.Models;
using ToDoList.Views;

namespace ToDoList.ViewModels
{
    internal class AddSubTDLVM
    {
        public AddSubTDLCommand CommandAddSubTDL { get; set; }

        public AddSubTDLVM()
        {
            CommandAddSubTDL = new AddSubTDLCommand(this);
        }
        public void OnExecute()
        {
            AddTDL newDB = new AddTDL();
            newDB.m_image = $"../Pics/i1.jpg";
            newDB.ShowDialog();
            TDL t = new TDL();
            t.name = newDB.m_name;
            t.image = newDB.m_image;
            ClasaDeBinding.selectedTDL.m_Content_TDL.Add(t);
        }
    }
}
