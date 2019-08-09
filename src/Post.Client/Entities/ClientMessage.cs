using System;
using System.Collections.Generic;
using System.Text;
using Post.Base;

namespace Post.Client.Entities
{
    public class ClientMessage : Message
    {
        public MessageState State { get; set; }
    }
}
