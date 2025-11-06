namespace W
{
    using UnityEngine;
    using System.Collections.Generic;

    public class ComUITools : ComUGui
    {
        [Header("Settings")]
        [SerializeField] private int toolCount = 8;
        [SerializeField] private float distance = 150f;

        #region public property
        public int ToolCount => toolCount;
        public float Distance => distance;
        #endregion
    }
}