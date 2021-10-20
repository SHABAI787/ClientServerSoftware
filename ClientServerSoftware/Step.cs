using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServerSoftware
{
    [Serializable]
    public class Step
    {
        public Player Player { get; set; }
        /// <summary>
        /// 1 2 3 |
        /// 4 5 6 |
        /// 7 8 9 |
        /// </summary>
        public StepIndex Index { get; set; } 
    }

    public enum StepIndex
    {
        I1,
        I2,
        I3,
        I4,
        I5,
        I6,
        I7,
        I8,
        I9,
    }
}
