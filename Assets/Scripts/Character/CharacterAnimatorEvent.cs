using UnityEngine;
using UnityEngine.Events;

public class CharacterAnimatorEvent : MonoBehaviour
{
    public UnityEvent characterAnimationEvent;

    public void SetEvent()
    {
        characterAnimationEvent.Invoke();
    }
}
