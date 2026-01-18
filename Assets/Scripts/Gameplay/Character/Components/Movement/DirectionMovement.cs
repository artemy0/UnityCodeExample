using UnityEngine;

namespace Gameplay.Character.Components.Movement
{
    [RequireComponent(typeof(CharacterController))]
    public class DirectionMovement : MonoBehaviour, ICharacterMovement
    {
        [SerializeField]
        private CharacterController controller;

        [SerializeField]
        private float speed = 5f;

#if UNITY_EDITOR
        private void OnValidate()
        {
            controller = GetComponent<CharacterController>();
        }
#endif

        public void Move(Vector3 direction)
        {
            if (direction.sqrMagnitude < float.Epsilon)
            {
                Stop();
                return;
            }

            var motion = direction.normalized * speed * Time.deltaTime;
            controller.Move(motion);
        }

        public void Stop()
        {
            // CharacterController has no inertia by default
            // so Stop() can be empty or used for animation sync
        }
    }
}