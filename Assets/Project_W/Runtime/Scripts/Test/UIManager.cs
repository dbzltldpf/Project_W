namespace S
{
    using UnityEngine;
    public class UIManager : Singleton<UIManager>
    {
        public GameObject toolSelector;
        public GameObject potionSelector;
        public GameObject potionThrow;
        public void ShowUI(GameObject obj)
        {
            obj.SetActive(true);
        }
        public void HideUI(GameObject obj)
        {
            obj.SetActive(false);
        }
    }
}
