using System.Collections;
using UnityEngine;

public class CollectableResource : MonoBehaviour
{
    public Rigidbody resourceBody;
    public BoxCollider collectTrigger;

    private float _ImpulseSpeed => Settings.s.impulseSpeed;
    private float _CollectDelay => Settings.s.collectDelay;

    private bool _IsCollectable;

    private void Start()
    {
        _IsCollectable = false;
        resourceBody.AddForce(Vector3.up * _ImpulseSpeed, ForceMode.Impulse);
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

        Destroy(gameObject);
    }
}
