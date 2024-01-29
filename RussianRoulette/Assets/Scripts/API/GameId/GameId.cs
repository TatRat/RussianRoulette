using System;
using UnityEngine;

namespace TatRat.API
{
    [Serializable]
    public struct GameId :
        IEquatable<GameId>,
        IComparable<GameId>
    {
        [SerializeField]
        private int _value;

        public int Value => _value;

        public GameId(int value)
        {
            _value = value;
        }

        public override string ToString()
            => Value.ToString();

        public override int GetHashCode()
            => Value.GetHashCode();

        public override bool Equals(object obj)
            => obj is GameId other && Equals(other);

        public bool Equals(GameId other)
            => Value == other.Value;

        public int CompareTo(GameId other)
            => Value.CompareTo(other._value);

        public static bool operator ==(GameId left, GameId right)
            => left.Equals(right);

        public static bool operator !=(GameId left, GameId right)
            => !left.Equals(right);

        public static implicit operator int(GameId gameId)
            => gameId.Value;

        public static implicit operator GameId(int value)
            => new(value);
    }
}