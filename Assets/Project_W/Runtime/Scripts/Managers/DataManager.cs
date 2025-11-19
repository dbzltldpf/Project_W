namespace W
{
    using System.Collections.Generic;
    using UnityEngine;
    using Newtonsoft.Json;
    using System.Threading.Tasks;

    public class DataManager : Singleton<DataManager>
    {
        [SerializeField] private TextAsset potionJson;
        [SerializeField] private TextAsset materialJson;

        private readonly Dictionary<ItemType, object> itemDatabases = new();
        private readonly Dictionary<int, PotionItemData> potionItemDatabase = new();
        private readonly Dictionary<int, MaterialItemData> materialItemDatabase = new();

        private bool isInit = false;

        public async Task Initialize()
        {
            if (isInit) return;

            potionJson = await ResourceManager.Instance.LoadAsync<TextAsset>(ResourceKeys.Tables.Potions);
            materialJson = await ResourceManager.Instance.LoadAsync<TextAsset>(ResourceKeys.Tables.Materials);

            InitItemDatabase(potionJson, potionItemDatabase);
            InitItemDatabase(materialJson, materialItemDatabase);

            itemDatabases[ItemType.Potion] = potionItemDatabase;
            itemDatabases[ItemType.Material] = materialItemDatabase;

            isInit = true;
        }

        private void InitItemDatabase<TData>(TextAsset json, Dictionary<int, TData> database)
            where TData : IItemData
        {
            var items = JsonConvert.DeserializeObject<TData[]>(json.text);
            for(int i = 0; i < items.Length; ++i)
            {
                int key = items[i].ID;
                database[key] = items[i];
            }
        }

        public T GetItemData<T>(ItemType type, ItemID id)
            where T : class, IItemData
        {
            if(!isInit)
            {
                Debug.LogError("DataManager is not initialized!");
                return null;
            }

            var db = GetDatabase<T>(type);
            db.TryGetValue((int)id, out var data);
            return data;
        }

        private Dictionary<int, T> GetDatabase<T>(ItemType type)
            where T : class, IItemData
        {
            return type switch
            {
                ItemType.Potion => potionItemDatabase as Dictionary<int, T>,
                ItemType.Material => materialItemDatabase as Dictionary<int, T>,
                _ => null
            };
        }
    }
}