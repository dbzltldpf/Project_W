namespace S
{
    using UnityEngine;
    public interface IAnimationReceiver
    {
        void OnAnimationEnd(AnimationTag animationTag);
    }
    public enum AnimationTag
    {
        Throw,
        Drink,
    }
    public class AnimationNotifier : StateMachineBehaviour
    {
        [SerializeField]
        private AnimationTag animationTag;
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Debug.Log("start");
        }
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Debug.Log("end");
            var receiver = animator.GetComponent<IAnimationReceiver>();
            receiver?.OnAnimationEnd(animationTag);
        }
    }
}
