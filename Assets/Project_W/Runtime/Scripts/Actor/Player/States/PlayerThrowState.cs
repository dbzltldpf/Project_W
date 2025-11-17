namespace S
{
    using UnityEngine;
    using UnityEngine.Playables;

    public class PlayerThrowState : ICharacterState
    {
        private PlayerController player;
        private GameObject potion;
        private float speed = 3f;
        private Vector2 finalPos;
        public PlayerThrowState(PlayerController player)
        {
            this.player = player;
        }

        public void Enter()
        {
            potion = player.throwPotion;
            finalPos = player.potionIndicator.finalPos;
            player.potionIndicator.IndicatorHide();
            player.animator.SetBool("isThrow", true);
        }

        public void Exit()
        {
            Debug.Log("Exit");
            player.throwAniEnd = false;
        }

        public void FixedUpdate()
        {
            player.rb.linearVelocity = Vector2.zero;
        }

        public void Update()
        {
            if (!player.throwAniEnd)
                return;

            potion.transform.position = Vector2.MoveTowards(
                potion.transform.position,
                finalPos, 
                speed * Time.deltaTime
                );

            if ((Vector2)potion.transform.position == finalPos)
            {
                player.ChangeState(player.state.Idle);
                InputSystem.Instance.ChangeState(new DefaultInputSystem());
                potion.gameObject.SetActive(false);
            }
        }
    }
}
