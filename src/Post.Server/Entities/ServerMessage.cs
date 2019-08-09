using System;
using System.Collections.Generic;
using System.Text;
using Post.Base;

namespace Post.Server.Entities
{
    public class ServerMessage : Message
    {
        public string Address { get; set; }        
    }
}
