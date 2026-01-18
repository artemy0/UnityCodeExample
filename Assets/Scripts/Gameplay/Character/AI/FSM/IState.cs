namespace Gameplay.Character.AI.FSM
{
    public interface IState
    {
        void Enter();
        void Update();
        void Exit();
    }
}