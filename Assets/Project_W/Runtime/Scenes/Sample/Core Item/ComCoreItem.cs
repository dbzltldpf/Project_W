namespace W
{
    using UnityEngine;

    public class ComCoreItem : MonoBehaviour
    {
        public ItemID potionID;
        public ItemID materialID;

        private PotionItemFactory potionItemFactory = new();
        private MaterialItemFactory materialItemFactory = new();

        private void Update()
        {
            if(Input.GetKeyUp(KeyCode.Alpha1))
            {
                CreateAndUsePotion();
            }
            if(Input.GetKeyUp(KeyCode.Alpha2))
            {
                CreateAndManufactureMaterial();
            }
        }

        public void CreateAndUsePotion()
        {
            var data = DataManager.Instance.GetItemData<PotionItemData>(ItemType.Potion, potionID);
            var item = potionItemFactory.Create(data);
            item.Use();
        }

        public void CreateAndManufactureMaterial()
        {
            var data = DataManager.Instance.GetItemData<MaterialItemData>(ItemType.Material, materialID);
            var item = materialItemFactory.Create(data);
            item.Manufacture();
        }
    }
}