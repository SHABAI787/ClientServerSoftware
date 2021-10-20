using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServerSoftware
{
    [Serializable]
    public class Player
    {
        public string Name { get; set; }
        public TypePlayer Type { get; set; }
    }

    public enum TypePlayer
    {
        Zero,
        Cross,
    }
}
