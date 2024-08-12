using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCRMExpert.Models
{
    public class Meeting
    {
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime Reminder { get; set; }

        public Meeting(string name, DateTime startTime, DateTime endTime, DateTime reminder)
        {
            Name = name;
            StartTime = startTime;
            EndTime = endTime;
            Reminder = reminder;
        }
    }
}
