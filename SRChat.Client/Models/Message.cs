using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRChat.Client.Models
{
    internal class Message
    {
        public string Sender { get; set; }
        public string Text { get; set; }
        public FlowDirection FlowDirection { get; set; }
        public DateTime Date { get; set; }
        public string Image { get; set; }

    }
}
