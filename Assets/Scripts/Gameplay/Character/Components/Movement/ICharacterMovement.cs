using UnityEngine;

namespace Gameplay.Character.Components.Movement
{
    public interface ICharacterMovement
    {
        void Move(Vector3 position);
        void Stop();
    }
}