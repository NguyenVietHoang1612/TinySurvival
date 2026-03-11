using UnityEngine;

namespace GameRPG
{
    public interface IInteractable
    {
        void Interact(int speedDamage, Transform targetTransform);
    }
}
