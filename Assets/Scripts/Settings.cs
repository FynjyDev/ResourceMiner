using UnityEngine;

public class Settings : MonoBehaviour
{
    public SettingsAsset _settings;

    public static SettingsAsset s;
    public static Settings instance;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (Settings.instance == null)
        {
            Settings.instance = this;
        }
        else
        {
            Debug.LogWarning("A previously awakened Settings MonoBehaviour exists!", gameObject);
        }
        if (Settings.s == null)
        {
            Settings.s = _settings;
        }
    }
}