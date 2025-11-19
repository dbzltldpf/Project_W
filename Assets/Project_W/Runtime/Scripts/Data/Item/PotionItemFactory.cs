namespace W
{
    public class PotionItemFactory : ItemFactory<PotionItemData, PotionItem>
    {
        public override PotionItem Create(PotionItemData data)
        {
            return new PotionItem(data);
        }
    }
}