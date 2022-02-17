using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.Project.Domain.Entities
{
    public class Email
    {
        public string Id { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverAddress { get; set; }
        public string Subject { get; set; }
        public string Text { get; set; }
        public int TryCount { get; set; } = 0;
    }
}
