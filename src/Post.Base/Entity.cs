using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Post.Base
{
    public abstract class Entity
    {
        [Key]
        public long ID { get; set; }
    }
}
