using System;

namespace Post.Base
{
    public abstract class Message : Entity
    {
        public string Text { get; set; }

        public DateTime Date { get; set; }
    }
}
