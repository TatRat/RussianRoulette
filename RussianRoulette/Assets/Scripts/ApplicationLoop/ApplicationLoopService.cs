using System.Collections.Generic;
using TatRat.API;
using TatRat.API.API.ApplicationLoop;

namespace TatRat.ApplicationLoop.ApplicationLoop
{
    public class ApplicationLoopService : IApplicationLoopService
    {
        private readonly StateMachine _applicationStateMachine = new();

        public ApplicationLoopService(IList<IApplicationState> needApplicationStates)
        {
            foreach (IApplicationState state in needApplicationStates) 
                _applicationStateMachine.Add(state);
        }

        public void StartApplication()
        {
            _applicationStateMachine.ChangeState<LoadApplicationState>();
            _applicationStateMachine.GetCurrentState<LoadApplicationState>().ConfigApplied += ActivateApplication;
        }

        private void ActivateApplication()
        {
            _applicationStateMachine.GetCurrentState<LoadApplicationState>().ConfigApplied -= ActivateApplication;
            _applicationStateMachine.ChangeState<ActiveApplicationState>();
            _applicationStateMachine.GetCurrentState<ActiveApplicationState>().ApplicationDisabled += DisableApplication;
        }

        private void DisableApplication()
        {
            _applicationStateMachine.GetCurrentState<ActiveApplicationState>().ApplicationDisabled -= DisableApplication;
            _applicationStateMachine.ChangeState<InactiveApplicationState>();
            _applicationStateMachine.GetCurrentState<InactiveApplicationState>().ApplicationEnabled += EnableApplication;
        }

        private void EnableApplication()
        {
            _applicationStateMachine.GetCurrentState<InactiveApplicationState>().ApplicationEnabled -= EnableApplication;
            _applicationStateMachine.ChangeState<ActiveApplicationState>();
            _applicationStateMachine.GetCurrentState<ActiveApplicationState>().ApplicationDisabled += DisableApplication;
        }
    }
}