using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableResource : MonoBehaviour
{
    public ResourceController.ResourceTypes resourceType;

    [HideInInspector] public Spot spot;

    public Rigidbody resourceBody;
    public BoxCollider collectTrigger;

    private float _ImpulseSpeed => Settings.s.impulseSpeed;
    private float _CollectDelay => Settings.s.collectDelay;

    private bool _IsCollectable;

    private void Start()
    {
        _IsCollectable = false;

        SetImpulse();
        StartCoroutine(CollectDelay());
    }

    private IEnumerator CollectDelay()
    {
        yield return new WaitForSeconds(_CollectDelay);
        _IsCollectable = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_IsCollectable || !other.GetComponent<CharacterCollision>()) return;
        OnCollect();
    }

    private void OnCollect()
    {
        _IsCollectable = false;

        spot.resourceController.OnResourceCollect(resourceType);
        Destroy(gameObject);
    }

    private void SetImpulse()
    {
        List<float> _directionConstants = new List<float>() { -0.2f, 0.2f };

        float x = _directionConstants[Random.Range(0, _directionConstants.Count)];
        float z = _directionConstants[Random.Range(0, _directionConstants.Count)];

        Vector3 _impulseDirection = new Vector3(x, 1, z);

        resourceBody.AddForce(_impulseDirection * _ImpulseSpeed, ForceMode.Impulse);
    }
}
