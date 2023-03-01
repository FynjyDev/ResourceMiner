using System.Collections.Generic;
using UnityEngine;

public class ResourceController : MonoBehaviour
{
    public enum ResourceTypes { wood, stone, crystal };

    public UiController uiController;

    public List<ResourceInfo> resourceInfos;

    public void OnResourceCollect(ResourceTypes _resourceType)
    {
        ResourceInfo _res = GetInfoByType(_resourceType);
        _res.resourceCount++;

        uiController.UpdateResourceValue(_resourceType);
    }

    public ResourceInfo GetInfoByType(ResourceTypes _resourceType)
    {
        for (int i = 0; i < resourceInfos.Count; i++)
        {
            if (_resourceType == resourceInfos[i].resourceType) return resourceInfos[i];
        }
        return null;
    }
}

[System.Serializable]
public class ResourceInfo
{
    public ResourceController.ResourceTypes resourceType;
    public int resourceCount;
}

