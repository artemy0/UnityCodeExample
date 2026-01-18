using Gameplay.Character.AI.FSM;
using Gameplay.Character.Enemy.Components;

namespace Gameplay.Character.Enemy.States
{
    internal class ChaseState : IState
    {
        private EnemySensor sensor;
        private Enemy enemy;

        public ChaseState(Enemy enemy)
        {
            sensor = enemy.EnemySensor;
            this.enemy = enemy;
        }

        public void Enter()
        {
        }

        public void Update()
        {
            if (sensor.CanAttackPlayer())
            {
                enemy.StateMachine.GoTo<AttackState>();
            }

            if (!sensor.CanSeePlayer())
            {
                enemy.StateMachine.GoTo<PatrolState>();
            }

            enemy.Movement.Move(sensor.PlayerPosition);
        }

        public void Exit()
        {
        }
    }
}