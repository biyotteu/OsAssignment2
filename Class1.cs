using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsAssignment2
{
    public class Process
    {
        public int arrivalTime, burstTime, priority, num;
        public Color color;
    }

    public class Result
    {
        public int num, waitTime, completeTime;
    }
}
