using System.Collections;
using UnityEngine;

public class Spot : MonoBehaviour
{
    public enum SpotTypes { ResourcePoint, Factory };

    public SpotTypes spotType;

    public GameObject spotResourcePrefab;
    public Transform spotResourceSpawnPos;

    public bool isOnRecovery;

    private int _TempHitCount;
    private int _MaxHitCount => Settings.s.maxHitCount;
    private float _RecoveryTime => Settings.s.pointRecoveryTime;

    public virtual void SpawnResource()
    {
        if (_TempHitCount < _MaxHitCount)
        {
            Quaternion _randomRotation = Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), 0);
            Instantiate(spotResourcePrefab, spotResourceSpawnPos.position, _randomRotation, transform);
            _TempHitCount++;
        }
        else StartCoroutine(RecoverySpot());
    }

    private IEnumerator RecoverySpot()
    {
        isOnRecovery = true;

        yield return new WaitForSeconds(_RecoveryTime);

        _TempHitCount = 0;
        isOnRecovery = false;
    }
}
