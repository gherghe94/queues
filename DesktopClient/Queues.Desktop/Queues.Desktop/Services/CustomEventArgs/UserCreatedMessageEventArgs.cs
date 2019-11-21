using Queues.Desktop.Services.Messages;

namespace Queues.Desktop.Services.CustomEventArgs
{
    public class UserCreatedMessageEventArgs
    {
        public UserCreatedMessage User { get; private set; }

        public UserCreatedMessageEventArgs(UserCreatedMessage newUser)
        {
            User = newUser;
        }
    }
}