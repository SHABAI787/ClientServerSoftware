using System;

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
        I1 = 1,
        I2 = 2,
        I3 = 3,
        I4 = 4,
        I5 = 5,
        I6 = 6,
        I7 = 7,
        I8 = 8,
        I9 = 9,
    }
}
