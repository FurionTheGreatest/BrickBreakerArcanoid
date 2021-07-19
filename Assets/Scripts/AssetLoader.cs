using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

public class AssetLoader : MonoBehaviour
{
    public AssetReference backgroundAudioReference;
    private List<AsyncOperationHandle> _handles = new List<AsyncOperationHandle>();
    public async Task<object> LoadResourceByKey<T>(string key)
    {
        if (Addressables.InitializeAsync().Status == AsyncOperationStatus.Succeeded)
        {
            if (AssetExists(key))
            {
                var asyncOperationHandle = Addressables.LoadAssetAsync<T>(key);
                await asyncOperationHandle.Task;
                if (asyncOperationHandle.Status == AsyncOperationStatus.Succeeded)
                {
                    _handles.Add(asyncOperationHandle);
                    return asyncOperationHandle.Result;
                }
                
                Addressables.Release(asyncOperationHandle);
            }
        }

        Debug.LogError("Addressables are not initialised");
        return null;
    }

    public async Task<object> LoadResourceByReference<T>(AssetReference reference)
    {
        if (Addressables.InitializeAsync().Status == AsyncOperationStatus.Succeeded)
        {
            var asyncOperationHandle = reference.LoadAssetAsync<T>();
            await asyncOperationHandle.Task;
            if (asyncOperationHandle.Status == AsyncOperationStatus.Succeeded)
            {
                _handles.Add(asyncOperationHandle);
                return asyncOperationHandle.Result;
            }
            Addressables.Release(asyncOperationHandle);
        }

        Debug.LogError("Addressables are not initialised");
        return null;
    }

    private static bool AssetExists(object key)
    {
        foreach (var l in Addressables.ResourceLocators)
        {
            IList<IResourceLocation> locs;
            if (l.Locate(key, null, out locs))
            {
                return true;
            }
        }

        return false;
    }

    private void OnDisable()
    {
        foreach (var asyncOperationHandle in _handles)
        {
            Addressables.Release(asyncOperationHandle);
        }
    }
}