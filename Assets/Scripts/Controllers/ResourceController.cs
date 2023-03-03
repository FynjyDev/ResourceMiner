using System.Collections.Generic;
using UnityEngine;

public class ResourceController : MonoBehaviour
{
    public enum ResourceTypes { wood, stone, crystal };

    public UiController uiController;

    public List<ResourceInfo> resourceInfos;

    public void OnResourceValueChange(ResourceTypes _resourceType, bool _isAdd)
    {
        ResourceInfo _res = GetInfoByType(_resourceType);
        
        if(_isAdd) _res.resourceCount++;
        else _res.resourceCount--;

        uiController.UpdateResourceValue(_resourceType, _isAdd);
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

