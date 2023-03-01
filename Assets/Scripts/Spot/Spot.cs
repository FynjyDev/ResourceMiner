using System.Collections;
using UnityEngine;

public class Spot : MonoBehaviour
{
    public enum SpotTypes { ResourcePoint, Factory };

    public SpotTypes spotType;

    public bool isOnRecovery;

    public CollectableResource spotResourcePrefab;
    public Transform spotResourceSpawnPos;

    [HideInInspector] public ResourceController resourceController;

    public int tempHitCount;
    public int maxHitCount => Settings.s.maxHitCount;
    public float recoveryTime => Settings.s.pointRecoveryTime;


    public virtual void SpawnResource()
    {
        if (tempHitCount < maxHitCount)
        {
            Quaternion _randomRotation = Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), 0);
            CollectableResource _newResource = Instantiate(spotResourcePrefab, spotResourceSpawnPos.position, _randomRotation, transform);

            _newResource.spot = this;

            tempHitCount++;
        }
        else StartCoroutine(RecoverySpot());
    }

    public virtual IEnumerator RecoverySpot()
    {
        isOnRecovery = true;

        while (tempHitCount > 0)
        {
            tempHitCount--;
            yield return new WaitForSeconds(recoveryTime / maxHitCount);
        }

        isOnRecovery = false;
    }
}
