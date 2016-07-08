using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FuelSupply.APP.Helper
{
    class RelayCommand : ICommand
    {
        private Action<object> _actionP;
        private Action _action;
        public RelayCommand(Action<object> action)
        {
            _actionP = action;

        }
        public RelayCommand(Action action)
        {
            _action = action;

        }

        public void Execute(object parameter)
        {
            if ((parameter != null))
            {
                _actionP(parameter);

            }
            else
            {

                _action();
            }
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;
    }
}
