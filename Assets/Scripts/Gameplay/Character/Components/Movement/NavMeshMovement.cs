using UnityEngine;
using UnityEngine.AI;

namespace Gameplay.Character.Components.Movement
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class NavMeshMovement : MonoBehaviour, ICharacterMovement
    {
        [SerializeField]
        private NavMeshAgent agent;

#if UNITY_EDITOR
        private void OnValidate()
        {
            agent = GetComponent<NavMeshAgent>();
        }
#endif

        public void Move(Vector3 position)
        {
            agent.isStopped = false;
            agent.SetDestination(position);
        }

        public void Stop()
        {
            agent.isStopped = true;
            agent.ResetPath();
        }
    }
}