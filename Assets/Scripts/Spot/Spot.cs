using System.Collections;
using UnityEngine;

public class Spot : MonoBehaviour
{
    public enum SpotTypes { ResourcePoint, Factory };

    [Header("Base Pharameters")]

    public SpotTypes spotType;

    public CharacterCollision characterCollision;
    public CollectableResource spotGiveResourcePrefab;
    public ResourceController resourceController;
    public Transform spotResourceSpawnPos;

    public bool isEnabled;

    public int maxHitCount => Settings.s.maxHitCount;
    public float recoveryTime => Settings.s.pointRecoveryTime;

    private void OnTriggerEnter(Collider other)
    {
        characterCollision = other.GetComponent<CharacterCollision>();
        OnPlayerEnter();
    }

    private void OnTriggerStay(Collider other)
    {
        characterCollision = other.GetComponent<CharacterCollision>();
        OnPlayerStay();
    }

    private void OnTriggerExit(Collider other)
    {
        OnPlayerExit();
    }

    public virtual void OnPlayerEnter()
    {

    }

    public virtual void OnPlayerStay()
    {
    }

    public virtual void OnPlayerExit()
    {

    }


    public virtual void SpawnResource()
    {
        Quaternion _randomRotation = Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), 0);
        CollectableResource _newResource = Instantiate(spotGiveResourcePrefab, spotResourceSpawnPos.position, _randomRotation, transform);

        _newResource.spot = this;
    }
}
