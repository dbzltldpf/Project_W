namespace W
{
    using UnityEngine;
    using System.Collections.Generic;

    public class UIManager : Singleton<UIManager>
    {
        [SerializeField]
        private Dictionary<UGuiId, ComUGui> uguis = new Dictionary<UGuiId, ComUGui>();

        private bool IsRegisted(UGuiId id) => uguis.ContainsKey(id);

        public void Regist(UGuiId id, ComUGui ugui)
        {
            if (IsRegisted(id))
                return;

            uguis.Add(id, ugui);
        }

        public void UnRegist(UGuiId id)
        {
            if (!IsRegisted(id))
                return;

            uguis.Remove(id);
        }

        public T Get<T>(UGuiId id)
        {
            if (!IsRegisted(id))
                throw new System.Exception($"{id} is not register.");

            return uguis[id].GetComponent<T>();
        }

        public void Open(UGuiId id)
        {
            if (!IsRegisted(id))
                throw new System.Exception($"{id} is not register.");

            uguis[id].Open();
        }

        public void Close(UGuiId id)
        {
            if (!IsRegisted(id))
                throw new System.Exception($"{id} is not register.");

            uguis[id].Close();
        }
    }
}