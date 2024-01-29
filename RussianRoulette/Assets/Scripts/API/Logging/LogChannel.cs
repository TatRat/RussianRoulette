using System;
using UnityEngine;

namespace TatRat.API
{
    [Serializable]
    public struct LogChannel : IEquatable<LogChannel>
    {
        [SerializeField]
        private string _name;

        public string Name => _name;

        public LogChannel(string name)
        {
            _name = name;
        }

        public override int GetHashCode() =>
            !string.IsNullOrWhiteSpace(Name) ? Name.GetHashCode() : 0;

        public override bool Equals(object obj)
            => obj is LogChannel other && Equals(other);

        public bool Equals(LogChannel other)
            => Name == other.Name;

        public override string ToString() => Name;

        public static implicit operator LogChannel(string name) => new(name);

        public static implicit operator string(LogChannel channel) => channel.Name;
    }
}