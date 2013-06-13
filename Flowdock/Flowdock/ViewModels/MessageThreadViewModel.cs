using Caliburn.Micro;
using Flowdock.Client.Context;
using Flowdock.Services.Navigation;
using Flowdock.Services.Progress;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Message = Flowdock.Client.Domain.Message;

using Flowdock.Extensions;
using Flowdock.Client.Domain;
using System.Windows.Media;
using Flowdock.Services.Util;

namespace Flowdock.ViewModels {
    public class MessageThreadViewModel : PropertyChangedBase, IActivate {
        private IFlowdockContext _context;
        private IProgressService _progressService;
        private INavigationManager _navigationManager;

        private string _flowName;

        private ObservableCollection<User> _users;
        private ObservableCollection<MessageViewModel> _messages;

        public MessageThreadViewModel(IFlowdockContext context, INavigationManager navigationManager, IProgressService progressService) {
            _context = context.ThrowIfNull("context");
            _navigationManager = navigationManager.ThrowIfNull("navigationManager");
            _progressService = progressService.ThrowIfNull("progressService");
        }

        private async void LoadMessageThread() {
            _progressService.Show("");

            try {
                Flow flow = await _context.GetFlow(FlowId);
                FlowName = flow.Name;
                Users = new ObservableCollection<User>(flow.Users);

                IEnumerable<Message> firstMessage = await _context.GetFirstMessageForThread(FlowId, ThreadId);
                IEnumerable<Message> restOfMessages = await _context.GetRestOfMessagesForThread(FlowId, ThreadId);

                IEnumerable<Message> messages = firstMessage.Concat(restOfMessages);

                // Converts string to Color using our own utility (ColorConverter doesn't exist in WP8)
                Color? threadColor = ColorConverter.ConvertFromArgbString(ThreadColor);

                if (messages != null) {
                    Messages = new ObservableCollection<MessageViewModel>(
                        messages.Where(m => m.Displayable).Select(m => new MessageViewModel(m, FlowId, threadColor, _navigationManager))
                    );
                }

                AssociateAvatarsToMessages();
            } finally {
                _progressService.Hide();
            }
        }

        private void AssociateAvatarsToMessages() {
            if (_messages != null) {
                foreach (var msg in _messages) {
                    FindAvatar(msg);
                }
            }
        }

        private void FindAvatar(MessageViewModel msg) {
            var user = _users.FirstOrDefault(u => u.Id == msg.UserId);
            if (user != null) {
                msg.Avatar = user.Avatar;
            }
        }

        public string FlowId { get; set; }
        public int ThreadId { get; set; }
        public string ThreadColor { get; set; }
        
        public string FlowName {
            get {
                return _flowName;
            }
            set {
                _flowName = value;
                NotifyOfPropertyChange(() => FlowName);
            }
        }

        public ObservableCollection<User> Users {
            get {
                return _users;
            }
            private set {
                _users = value;
                NotifyOfPropertyChange(() => Users);
            }
        }

        public ObservableCollection<MessageViewModel> Messages {
            get {
                return _messages;
            }
            private set {
                _messages = value;
                NotifyOfPropertyChange(() => Messages);
            }
        }

        public void Activate() {
            LoadMessageThread();
        }

        public event EventHandler<ActivationEventArgs> Activated;

        public bool IsActive {
            get {
                return Messages != null;
            }
        }
    }
}
