namespace W
{
    using System.Collections.Generic;
    using UnityEngine;

    public class ComUIMain : MonoBehaviour
    {
        private void Awake()
        {
            AwakeOnOpen();
        }

        private void AwakeOnOpen()
        {
            int count = transform.childCount;
            for (int i = 0; i < count; ++i)
            {
                if(transform.GetChild(i).TryGetComponent<ComUGui>(out var component))
                {
                    if(component.OnAwake)
                        component.Open();
                    else
                        component.Close();
                }
            }
        }
    }
}