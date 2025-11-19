namespace W
{
    using UnityEngine;

    public class WeedTerminatorPotion : PotionItem
    {
        public WeedTerminatorPotion(PotionItemData data) : base(data)
        {
        }

        public override void Use()
        {
            base.Use();

            // 던져서 풀을 제거한다.
        }
    }
}