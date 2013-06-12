using Flowdock.Client.Domain;
using Flowdock.Services.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Flowdock.Extensions;
using System.Windows.Media;

namespace Flowdock.Commands {
    public class GoToMessageThreadCommand : ICommand {
        private string _flowId;
        private INavigationManager _navigationManager;
        private Color? _threadColor;

        public GoToMessageThreadCommand(string flowId, INavigationManager navigationManager, Color? threadColor) {
            _flowId = flowId.ThrowIfNull("flow");
            _navigationManager = navigationManager.ThrowIfNull("navigationManager");
            _threadColor = threadColor;
        }

        public bool CanExecute(object threadId) {
            return threadId != null;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object threadId) {
            _navigationManager.GoToMessageThread(_flowId, (int)threadId, _threadColor);
        }
    }
}
