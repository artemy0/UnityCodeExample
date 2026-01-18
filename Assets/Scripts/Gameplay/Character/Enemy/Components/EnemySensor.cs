using UnityEngine;

namespace Gameplay.Character.Enemy.Components
{
    public class EnemySensor : MonoBehaviour
    {
        [SerializeField]
        private float visionRange = 10f;

        [SerializeField]
        private float attackRange = 0.5f;

        private Transform playerTransform;

        private float sqrVisionRange;
        private float sqrAttackRange;

        public float SqrDistanceToPlayer => Vector3.SqrMagnitude(PlayerPosition - transform.position);
        public Vector3 PlayerPosition => playerTransform.position;

        public void Initialize(Transform playerTransform)
        {
            this.playerTransform = playerTransform;

            sqrVisionRange = visionRange * visionRange;
            sqrAttackRange = attackRange * attackRange;
        }

        public bool CanSeePlayer()
        {
            return SqrDistanceToPlayer <= sqrVisionRange;
        }

        public bool CanAttackPlayer()
        {
            return sqrAttackRange <= sqrVisionRange;
        }
    }
}