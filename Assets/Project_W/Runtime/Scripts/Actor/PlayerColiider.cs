namespace S
{
    using System.Collections.Generic;
    using UnityEngine;

    public class PlayerColiider : MonoBehaviour
    {
        private List<IInteractable> interactables = new List<IInteractable>();
        //private IInteractable currentTarget;
        private PlayerController player;

        private float interactAngle = 80f;
        private float dotAngle;
        private void Awake()
        {
            player = GetComponentInParent<PlayerController>();
            //interactAngle(도)를 radians(각도)로 변환 후
            //두 벡터 사이가 80일때의 내적값을 구하기위해 cos
            dotAngle = Mathf.Cos(interactAngle * Mathf.Deg2Rad);
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<IInteractable>(out var interactable))
            {
                if (interactable.GetNonPlayerType() == NonPlayerType.Npc)
                    return;

                if(!interactables.Contains(interactable))
                    interactables.Add(interactable);
            }
        }
        private void OnTriggerStay2D(Collider2D collision)
        {
            InteractCheck();
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent<IInteractable>(out var interactable))
            {
                if (interactable.GetNonPlayerType() == NonPlayerType.Npc)
                    return;

                interactables.Remove(interactable);
            }
        }
        private void Update()
        {
            //InteractCheck();
        }
        private void InteractCheck()
        {
            player.canInterect = false;

            if (interactables.Count == 0)
            {
                if(player.currentTarget != null)
                {
                    player.currentTarget.HideInteractUI();
                }
                player.currentTarget = null;
                return;
            }
            Vector2 playerPos = player.transform.position;
            //정규화해서 방향만 남김
            Vector2 playerLookDir = player.dir.normalized;
            float bestScore = -Mathf.Infinity;
            IInteractable bestTarget = null;

            foreach (var target in interactables)
            {
                if (!target.CanInteractType(player.interactType))
                    continue;

                //타겟 방향벡터 구하기
                Vector2 targetDirection = target.GetPosition() - playerPos;
                //방향빼고 크기만 남김(스칼라)
                float targetDistance = targetDirection.magnitude;
                //정규화해서 방향만 남김(단위 벡터 = 길이 1)
                Vector2 targetLookDir = targetDirection.normalized;
                //두 단위벡터의 내적값
                float dot = Vector2.Dot(playerLookDir, targetLookDir);

                if(dot >= dotAngle)
                {
                    player.canInterect = true;

                    float score = 1f / targetDistance;
                    if(score > bestScore)
                    {
                        bestScore = score;
                        bestTarget = target;
                    }
                }
            }
            if(player.currentTarget != bestTarget)
            {
                player.currentTarget?.HideInteractUI();
                player.currentTarget = bestTarget;
                player.currentTarget?.ShowInteractUI();
            }
        }
    }
}
    