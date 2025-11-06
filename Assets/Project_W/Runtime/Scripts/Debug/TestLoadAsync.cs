using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine.InputSystem.LowLevel;

public class TestLoadAsync : MonoBehaviour
{
    public AssetReference spriteRef;
    public AssetReference sheetRef;
    public AssetReference prefabRef;

    private KeySetting keySetting = new DefaultKeySetting();

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            keySetting = new ToolKeySetting();
        }

        keySetting.Update();
    }

    private void EAction()
    {
        Debug.Log("E");
    }
}

public class KeySetting
{
    public Dictionary<KeyCode, System.Action> actions = new();

    public void Add(KeyCode key, System.Action action)
    {
        if (!actions.ContainsKey(key))
            actions[key] = action;
    }

    public void Update()
    {
        foreach(var action in actions)
        {
            if (Input.GetKeyDown(action.Key))
                action.Value?.Invoke();
        }
    }
}

public class DefaultKeySetting : KeySetting
{
    public DefaultKeySetting()
    {
        
    }
}

public class ToolKeySetting : KeySetting
{

}