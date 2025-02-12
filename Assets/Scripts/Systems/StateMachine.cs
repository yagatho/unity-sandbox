namespace Project.Systems.StateMachine
{
    public abstract class State
    {
        public abstract void Enter();
        public abstract void Update();
        public abstract State Exit();
    }

    public class StateMachine
    {
        private State currentState;

        public void Start(State state)
        {
            currentState = state;
            currentState?.Enter();
        }

        public void ChangeState()
        {
            currentState = currentState?.Exit();
            currentState?.Enter();
        }

        public void Update()
        {
            currentState?.Update();
        }

        public State GetState()
        {
            return currentState;
        }
    }
}
