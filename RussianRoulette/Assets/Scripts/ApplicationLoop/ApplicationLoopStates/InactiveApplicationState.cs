using TatRat.API;
using UnityEngine;

namespace TatRat.ApplicationLoop
{
    public class InactiveApplicationState : ApplicationState, IEnterableState, IExitableState
    {
        private readonly IPlatformService _platformService;

        public InactiveApplicationState(IPlatformService platformService) => 
            _platformService = platformService;

        public void Enter() => 
            _platformService.ApplicationVisibilityTypeChanged += OnFocusChanged;

        public void Exit() => 
            _platformService.ApplicationVisibilityTypeChanged -= OnFocusChanged;

        private void OnFocusChanged(ApplicationVisibilityType applicationVisibilityType)
        {
            if (applicationVisibilityType == ApplicationVisibilityType.Disabled)
                return;
            
            StateMachine.ChangeState<ActiveApplicationState>();
        }
    }
}