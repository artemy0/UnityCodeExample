using Gameplay.Character.Components.Attack;
using Gameplay.Character.Components.Movement;
using UnityEngine;

namespace Gameplay.Character
{
    public class Character : MonoBehaviour
    {
        public ICharacterMovement Movement { get; private set; }
        public ICharacterAttack Attack { get; private set; }

        private void Awake()
        {
            // TODO: Add serialization by interface to cache data in OnValidate

            Movement = GetComponent<ICharacterMovement>();
            Attack = GetComponent<ICharacterAttack>();

            PostAwake();
        }

        protected virtual void PostAwake()
        {
        }
    }
}