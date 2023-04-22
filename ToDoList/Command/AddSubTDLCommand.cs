using System;
using System.Windows.Input;
using ToDoList.ViewModels;

namespace ToDoList.Command
{
    internal class AddSubTDLCommand : ICommand
    {
        private readonly AddSubTDLVM _buttonViewModel;

        public AddSubTDLCommand(AddSubTDLVM viewModel)
        {
            _buttonViewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _buttonViewModel.OnExecute();
        }
    }
}
