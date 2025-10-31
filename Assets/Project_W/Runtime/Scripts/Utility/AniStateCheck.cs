namespace S
{
    using UnityEngine;

    public static class AniStateCheck
    {
        public static bool IsPlaying(Animator ani, string name)
        {
            var aniInfo = ani.GetCurrentAnimatorStateInfo(0);
            return aniInfo.IsName(name) && aniInfo.normalizedTime >= 1f;
        }
    }
}
