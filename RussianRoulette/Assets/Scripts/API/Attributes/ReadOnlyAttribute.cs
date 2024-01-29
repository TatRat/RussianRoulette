using System;
using UnityEngine;

namespace TatRat.API
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class ReadOnlyAttribute : PropertyAttribute
    {
    }
}