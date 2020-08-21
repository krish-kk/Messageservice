using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolescheduler
{
    public class Messages
    {
        //defining all the properties of the table with get and set
        public int Id { get; set; }
        public string mes_content { get; set; }
        public DateTime delivered_dt { get; set; }
    }
}
