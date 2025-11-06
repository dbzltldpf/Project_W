using UnityEngine;

namespace S
{
    public interface IInteractionStrategy
    {
        void Interact(PlayerController player, IInteractable target);
    }
    public class TalkInteraction : IInteractionStrategy
    {
        public void Interact(PlayerController player, IInteractable target)
        {
            player.ChangeState(player.state.Talk);
            target.Interact();
        }
    }
    public class GatherInteraction : IInteractionStrategy
    {
        public void Interact(PlayerController player, IInteractable target)
        {
            player.ChangeState(player.state.Gather);
            target.Interact();
        }
    }
    public class DrawInteraction : IInteractionStrategy
    {
        public void Interact(PlayerController player, IInteractable target)
        {
            player.ChangeState(player.state.Draw);
            target.Interact();
        }
    }

}
