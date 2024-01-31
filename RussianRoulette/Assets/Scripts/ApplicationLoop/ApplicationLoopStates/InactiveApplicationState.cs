using System;
using TatRat.API;
using UnityEngine;

namespace TatRat.ApplicationLoop
{
    public class InactiveApplicationState : IApplicationState, IEnterableState, IExitableState
    {
        public event Action ApplicationEnabled;

        public void Enter() => 
            Application.focusChanged += OnFocusChanged;

        public void Exit() => 
            Application.focusChanged -= OnFocusChanged;

        private void OnFocusChanged(bool isFocused)
        {
            if (!isFocused)
                return;
            
            ApplicationEnabled?.Invoke();
        }
    }
}