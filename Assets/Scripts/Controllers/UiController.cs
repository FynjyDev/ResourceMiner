using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;

public class UiController : MonoBehaviour
{
    public ResourceController resourceController;

    public List<ResourceVizualization> resourceVizualizations;

    private AnimationCurve ElementsMoveCurve => Settings.s.elementsMoveCurve;
    private float _ElementsMoveTime => Settings.s.elementsMoveTime;

    public void UpdateResourceValue(ResourceController.ResourceTypes _resourceType, bool _isAdd)
    {
        ResourceVizualization _res = GetVizualizationByType(_resourceType);
        SpawnMoveElements(_res, _isAdd);
    }

    private void SpawnMoveElements(ResourceVizualization _res, bool _isAdd)
    {
        Vector3 spawnPos = _isAdd == true ? _res.startPos.position : _res.finalPos.position; 
        Vector3 finalPos = _isAdd == true ? _res.finalPos.position : _res.startPos.position;

        MovementElementUI newElement = Instantiate(_res.movementUIElementPrefabs, spawnPos, Quaternion.identity, transform);

        MovementElementUI.endMovementDelegate += OnValueUpdate;

        newElement.StartCoroutine(newElement.Move(_ElementsMoveTime, ElementsMoveCurve, _res, finalPos));
    }

    private void OnValueUpdate(ResourceVizualization _res)
    {
        _res.resourceCountText.text = resourceController.GetInfoByType(_res.resourceType).resourceCount.ToString();
    }


    private ResourceVizualization GetVizualizationByType(ResourceController.ResourceTypes _resourceType)
    {
        for (int i = 0; i < resourceVizualizations.Count; i++)
        {
            if (_resourceType == resourceVizualizations[i].resourceType) return resourceVizualizations[i];
        }
        return null;
    }
}
[System.Serializable]
public class ResourceVizualization
{
    public ResourceController.ResourceTypes resourceType;
    public MovementElementUI movementUIElementPrefabs;

    public RectTransform startPos;
    public RectTransform finalPos;

    public TextMeshProUGUI resourceCountText;
}
