using System;

namespace AutoBattle
{
    public readonly struct TeamData
    {
        public string DisplaySymbol { get; }

        readonly string _name;
        
        public TeamData(string name, string displaySymbol)
        {
            _name = name;
            DisplaySymbol = displaySymbol;
        }

        public override string ToString()
        { 
            return _name;
        }

        public override bool Equals(object obj)
        {
            return obj is TeamData data &&
                   DisplaySymbol == data.DisplaySymbol &&
                   _name == data._name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(DisplaySymbol, _name);
        }

        public static bool operator ==(TeamData t1, TeamData t2)
        {
            return t1.Equals(t2);
        }

        public static bool operator !=(TeamData t1, TeamData t2)
        {
            return !t1.Equals(t2);
        }
    }
 
}
