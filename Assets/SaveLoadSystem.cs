using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveLoadSystem : MonoBehaviour
{
    public void Save(List<ResourceInfo> resourceInfo)
    {
        SaveData _data = new SaveData();

        for (int i = 0; i < resourceInfo.Count; i++)
        {
            _data.resourceType.Add((int)resourceInfo[i].resourceType);
            _data.count.Add(resourceInfo[i].resourceCount);
        }

        string json = JsonUtility.ToJson(_data, false);
        File.WriteAllText(Application.persistentDataPath + "/Data.json", json);
    }

    public SaveData Load()
    {
        if (!File.Exists(Application.persistentDataPath + "/Data.json")) return null;

        string json = File.ReadAllText(Application.dataPath + "/Data.json");
        SaveData data = JsonUtility.FromJson<SaveData>(json);

        return data;
    }
}

public class SaveData
{
    public List<int> resourceType = new List<int>();
    public List<int> count = new List<int>();
}
