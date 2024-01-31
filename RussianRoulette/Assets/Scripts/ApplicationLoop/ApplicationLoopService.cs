using System.Collections.Generic;
using TatRat.API;
using TatRat.API.API.ApplicationLoop;

namespace TatRat.ApplicationLoop
{
    public class ApplicationLoopService : IApplicationLoopService
    {
        private readonly StateMachine _applicationStateMachine = new();

        public ApplicationLoopService(IList<ApplicationState> needApplicationStates)
        {
            foreach (ApplicationState state in needApplicationStates)
            {
                state.Initialize(_applicationStateMachine);
                _applicationStateMachine.Add(state);
            }
        }

        public void StartApplication() => 
            _applicationStateMachine.ChangeState<LoadApplicationState>();
    }
}