using System.Collections.Generic;
using UnityEngine;

public class ResourceController : MonoBehaviour
{
    public enum ResourceTypes { wood, stone, crystal };

    public SaveLoadSystem saveLoadSystem;
    public UiController uiController;

    public List<ResourceInfo> resourceInfos;

    public void Start()
    {
        LoadAllResource();
    }

    public void OnResourceValueChange(ResourceTypes _resourceType, bool _isAdd)
    {
        ResourceInfo _res = GetInfoByType(_resourceType);
        
        if(_isAdd) _res.resourceCount++;
        else _res.resourceCount--;

        uiController.UpdateResourceValue(_resourceType, _isAdd);
        saveLoadSystem.Save(resourceInfos);
    }

    public ResourceInfo GetInfoByType(ResourceTypes _resourceType)
    {
        for (int i = 0; i < resourceInfos.Count; i++)
        {
            if (_resourceType == resourceInfos[i].resourceType) return resourceInfos[i];
        }
        return null;
    }

    private void LoadAllResource()
    {
        SaveData _saveData = saveLoadSystem.Load();
        ResourceInfo _loadInfo = new ResourceInfo();

        for (int i = 0; i < resourceInfos.Count; i++)
        {
            if (_saveData != null)
            {
                for (int j = 0; j < _saveData.resourceType.Count; j++)
                    _loadInfo = new ResourceInfo() { resourceType = (ResourceTypes)_saveData.resourceType[i], resourceCount = _saveData.count[i] };


                if (resourceInfos[i].resourceType == _loadInfo.resourceType)
                    resourceInfos[i].resourceCount = _loadInfo.resourceCount;
            }
            else
            {
                resourceInfos[i].resourceCount = 0;
            }

            uiController.OnValueUpdate(uiController.GetVizualizationByType(resourceInfos[i].resourceType));
        }
    }
}

[System.Serializable]
public class ResourceInfo
{
    public ResourceController.ResourceTypes resourceType;
    public int resourceCount;
}

