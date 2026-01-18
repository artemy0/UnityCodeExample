namespace Gameplay.Character.Components.Attack
{
    public interface ICharacterAttack
    {
        bool CanAttack();
        void Attack();
    }
}