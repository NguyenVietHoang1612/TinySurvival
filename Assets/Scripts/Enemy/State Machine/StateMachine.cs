namespace GameRPG
{
    public class StateMachine
    {
        public State CurrentState { get; private set; }

        public void Initialize(State startState)
        {
            CurrentState = startState;
        }

        public void ChangeState(State newState)
        {

            CurrentState.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }
    }
}

