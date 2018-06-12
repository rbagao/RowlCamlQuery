using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RowlCamlQuery
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _Execute;
        private readonly Predicate<object> _CanExecute;

        public RelayCommand(Action<object> execute)
            : this(execute, null)
        {
        }

        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            this._Execute = execute;
            this._CanExecute = canExecute;
        }

        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return this._CanExecute == null ? true : this._CanExecute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            this._Execute(parameter);
        }

    }
}
