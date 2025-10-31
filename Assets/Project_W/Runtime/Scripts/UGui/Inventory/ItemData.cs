namespace W
{
    using UnityEngine;

    [System.Serializable]
    public class ItemData
    {
        public int Uid;
        public string Title;
        public string Content;
        public string Path;
        public bool IsUseable;
        public float Value;
        public bool HasEffect;
        public int Effect_Id;
        public float Effect_Value;
        public float Effect_Duration;
        public string Effect_Content;
    }
}