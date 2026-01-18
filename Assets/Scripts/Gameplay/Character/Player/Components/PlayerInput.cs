using General.Extensions;
using UnityEngine;

namespace Gameplay.Character.Player.Components
{
    [RequireComponent(typeof(Player))]
    public sealed class PlayerInput : MonoBehaviour
    {
        [SerializeField]
        private Player player;

        private ProjectInput input;

#if UNITY_EDITOR
        private void OnValidate()
        {
            this.BakeComponentIfExist(ref player);
        }
#endif

        private void Awake()
        {
            input = new ProjectInput();
        }

        private void OnEnable()
        {
            input.Enable();
        }

        private void OnDisable()
        {
            input.Disable();
        }

        private void Update()
        {
            var currentMove = input.Player.Move.ReadValue<Vector2>();
            player.Movement.Move(new Vector3(currentMove.x, 0, currentMove.y));
        }
    }
}