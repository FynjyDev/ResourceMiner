using UnityEngine;

[CreateAssetMenu(fileName = "Game Settings", menuName = "Utils/Game Settings")]
public class SettingsAsset : ScriptableObject
{
    [Header("Character Settings")]
    public float extractSpeed;

    [Header("Resource Point Settings")]
    public float pointRecoveryTime;
    public int maxHitCount;

    [Header("Collectable Resource Settings")]
    public float impulseSpeed;
    public float collectDelay;

    [Header("UI Settings")]
    public AnimationCurve elementsMoveCurve;
    public float elementsMoveTime;
}
