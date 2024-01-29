using System;
using TatRat.API;
using UnityEngine;

namespace TatRat.Editor.GameId
{
    [Serializable]
    internal sealed class GameIdEntry :
        IEquatable<GameIdEntry>,
        IComparable<GameIdEntry>
    {
        [SerializeField]
        [ReadOnly]
        private int _value;

        [SerializeField]
        private string _name;

        public int Value => _value;

        public string Name => _name;

        public GameIdEntry() : this(0, string.Empty)
        {
        }

        public GameIdEntry(int value, string name)
        {
            _value = value;
            _name = name;
        }

        public override int GetHashCode()
            => Value.GetHashCode();

        public override string ToString()
            => Name;

        public override bool Equals(object obj)
            => obj is GameIdEntry other && Equals(other);

        public bool Equals(GameIdEntry other)
            => Value == other.Value;

        public int CompareTo(GameIdEntry other)
            => Value.CompareTo(other.Value);
    }
}