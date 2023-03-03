using System.Collections;
using UnityEngine;

public class MovementElementUI : MonoBehaviour
{
    public delegate void EndMovementDelegateTemplate(ResourceVizualization _res);

    public static EndMovementDelegateTemplate endMovementDelegate;

    public bool isDestroyOnEnd;
    public float rotateAngle;

    public IEnumerator Move(float time, AnimationCurve animCurve, ResourceVizualization _res, Vector3 finalPos)
    {
        float startRotation = transform.eulerAngles.z;
        float endRotation = startRotation + rotateAngle;

        Vector3 _startingPos = transform.position;

        float _elapsedTime = 0;

        while (_elapsedTime < time)
        {
            Move(_startingPos, finalPos, _elapsedTime / time, animCurve);
            if (rotateAngle != 0) Rotate(startRotation, endRotation, _elapsedTime / time);

            _elapsedTime += Time.deltaTime;
            yield return null;
        }

        OnMovementEnd(_res);
    }

    private void OnMovementEnd(ResourceVizualization _res)
    {
        if (endMovementDelegate != null) endMovementDelegate(_res);
        endMovementDelegate = null;

        if (isDestroyOnEnd) Destroy(gameObject);
    }

    private void Move(Vector3 startPos, Vector3 finalPos, float time, AnimationCurve curve)
    {
        Vector3 _pos = Vector3.Lerp(startPos, finalPos, time);
        _pos.x = _pos.x * curve.Evaluate(time);

        transform.position = _pos;
    }

    private void Rotate(float startRot, float endRot, float time)
    {
        float zRotation = Mathf.Lerp(startRot, endRot, time) % rotateAngle;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y,zRotation);
    }
}
