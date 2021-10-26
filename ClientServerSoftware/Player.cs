using System;

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
