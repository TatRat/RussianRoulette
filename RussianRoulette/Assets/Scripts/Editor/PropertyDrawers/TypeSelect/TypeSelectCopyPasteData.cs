using System;
using UnityEngine;

namespace TatRat.Editor.TypeSelect
{
    [Serializable]
    internal class TypeSelectCopyPasteData
    {
        [SerializeField]
        public string Json;

        [SerializeField]
        public string Typename;

        public bool IsValid
            => !string.IsNullOrWhiteSpace(Json) && !string.IsNullOrWhiteSpace(Typename);

        public TypeSelectCopyPasteData(string json, string typename)
        {
            Json = json;
            Typename = typename;
        }
    }
}