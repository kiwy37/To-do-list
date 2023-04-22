using System;
using System.Windows.Input;
using ToDoList.ViewModels;

namespace ToDoList.Command
{
    internal class NewDBCommand : ICommand
    {
        private readonly NewDBVM _buttonViewModel;

        public NewDBCommand(NewDBVM viewModel)
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
