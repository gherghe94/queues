﻿using NATS.Client;
using Newtonsoft.Json;
using Queues.Desktop.Services.CustomEventArgs;
using Queues.Desktop.Services.Messages;
using System;
using System.Text;

namespace Queues.Desktop.Services
{
    public class UserReceiverService : NatsService
    {
        public event EventHandler<UserCreatedMessageEventArgs> UserCreated;

        protected override string GetSubject()
        {
            return "receive-user";
        }

        protected override void OnMessageReceived(object sender, MsgHandlerEventArgs e)
        {
            var rawMessage = e.Message;
            var newUser = JsonConvert.DeserializeObject<UserCreatedMessage>(Encoding.UTF8.GetString(rawMessage.Data));
            UserCreated(this, new UserCreatedMessageEventArgs(newUser));
        }
    }
}
