using Gameplay.Character.AI.FSM;
using Gameplay.Character.Enemy.Components;
using Gameplay.Character.Enemy.States;
using UnityEngine;

namespace Gameplay.Character.Enemy
{
    public sealed class Enemy : Character
    {
        [SerializeField]
        private EnemySensor enemySensor;

        public StateMachine StateMachine { get; private set; }

        public EnemySensor EnemySensor => enemySensor;

        protected sealed override void PostAwake()
        {
            StateMachine = new StateMachine();

            StateMachine.TryAdd(new PatrolState());
            StateMachine.TryAdd(new ChaseState(this));
            StateMachine.TryAdd(new AttackState());

            StateMachine.GoTo<PatrolState>();
        }

        public void Initialize(Transform playerTransform)
        {
            enemySensor.Initialize(playerTransform);
        }

        void Update()
        {
            StateMachine.Update();
        }
    }
}