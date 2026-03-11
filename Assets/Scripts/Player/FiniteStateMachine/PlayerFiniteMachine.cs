namespace GameRPG
{
    public class PlayerFiniteMachine
    {
        public PlayerState CurrentState { get; private set; }

        public void Initinize(PlayerState startingState)
        {
            CurrentState = startingState;
            CurrentState.Enter();

        }

        public void ChangeState(PlayerState newState)
        {
            if (CurrentState != null)
            {
                if (CurrentState.GetType() == newState.GetType()) return;

                CurrentState.Exit();
                CurrentState = newState;
                CurrentState.Enter();
            }
        }


    }

}
