using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableResource : MonoBehaviour
{
    [HideInInspector] public Transform target;
    [HideInInspector] public float moveTime;

    public IEnumerator Move(ResourceFactory _resourceFactory)
    {
        Vector3 startPos = transform.position;
        Vector3 finalPos = target.position;

        float _elapsedTime = 0;

        while (_elapsedTime < moveTime)
        {
            Vector3 _pos = Vector3.Lerp(startPos, finalPos, _elapsedTime / moveTime);

            transform.position = _pos;

            _elapsedTime += Time.deltaTime;
            yield return null;
        }

        _resourceFactory.OnResourceTaken();

        Destroy(gameObject);
    }
}
