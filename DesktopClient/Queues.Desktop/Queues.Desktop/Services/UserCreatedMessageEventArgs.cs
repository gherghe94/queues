namespace Queues.Desktop.Services
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