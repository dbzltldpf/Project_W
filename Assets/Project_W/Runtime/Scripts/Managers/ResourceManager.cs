using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections.Generic;
using System.Threading.Tasks;

public class ResourceManager : Singleton<ResourceManager>
{
    // 비제네릭 AsyncOperationHandle을 저장 (AsyncOperationHandle<T>는 비제네릭으로 변환 가능)
    private readonly Dictionary<string, AsyncOperationHandle> handles = new();
    // 캐시 : Unity 에셋(Sprite, Prefab, AudioClip 등)을 보관.
    private readonly Dictionary<string, Object> cache = new();

    /// <summary>
    /// 일반적인 Unity 에셋 로드 (T는 UnityEngine.Object를 상속해야 함)
    /// 예: Sprite, GameObject(Prefab), AudioClip 등
    /// </summary>
    public async Task<T> LoadAsync<T>(string key, bool cacheAsset = false) where T : Object
    {
        if (cache.TryGetValue(key, out var obj))
            return obj as T;

        var handle = Addressables.LoadAssetAsync<T>(key);
        await handle.Task;

        if(handle.Status == AsyncOperationStatus.Succeeded)
        {
            handles[key] = handle;
            if (cacheAsset)
                cache[key] = handle.Result;
            return handle.Result;
        }

        Debug.LogError($"[ResourceManager] Failed to load {key}");
        return default;
    }

    /// <summary>
    /// 배열, 컬렉션 에셋 로드 (T는 UnityEngine.Object를 상속해야 함)
    /// 예: SpriteSheets
    /// </summary>
    public async Task<List<T>> LoadAssetsAsync<T>(string key) where T : Object
    {
        var resultList = new List<T>();
        var handle = Addressables.LoadAssetsAsync<T>(key, obj => resultList.Add(obj));
        await handle.Task;

        if(handle.Status == AsyncOperationStatus.Succeeded)
        {
            handles[key] = handle;
            return resultList;
        }

        Debug.LogError($"[ResourceManager] Failed to load {key}");
        return null;
    }

    /// <summary>
    /// 프리팹 인스턴스화 (Addressables.InstantiateAsync 사용)
    /// </summary>
    public async Task<GameObject> InstantiateAsync(string key, Transform parent = null)
    {
        var handle = Addressables.InstantiateAsync(key, parent);
        await handle.Task;

        if(handle.Status == AsyncOperationStatus.Succeeded)
        {
            handles[key] = handle;
            return handle.Result;
        }

        Debug.LogError($"[ResourceManager] Failed to instantiate {key}");
        return null;
    }

    /// <summary>
    /// 캐시 및 핸들 해제
    /// </summary>
    public void Release(string key)
    {
        if(handles.TryGetValue(key, out var handle))
        {
            Addressables.Release(handle);
            handles.Remove(key);
        }

        if (cache.ContainsKey(key))
            cache.Remove(key);
    }

    /// <summary>
    /// Addressables로 생성된 인스턴스 해제
    /// </summary>
    public void ReleaseInstance(GameObject obj)
    {
        Addressables.ReleaseInstance(obj);
    }

    public T GetCached<T>(string key) where T : Object
    {
        if (cache.TryGetValue(key, out var obj))
            return obj as T;
        return null;
    }
}
