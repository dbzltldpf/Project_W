namespace S
{
    using UnityEngine;
    public class NonPlayerCollider : MonoBehaviour
    {
        private NonPlayerController nonPlayer;
        private void Awake()
        {
            nonPlayer = GetComponentInParent<NonPlayerController>();
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.TryGetComponent<PlayerController>(out var player))
            {
                nonPlayer.ShowInteractUI();
                player.SetCanInterect(true);
                player.currentTarget = nonPlayer;
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent<PlayerController>(out var player))
            {
                nonPlayer.HideInteractUI();
                player.SetCanInterect(false);
                player.currentTarget = null;
            }
        }
    }
}
