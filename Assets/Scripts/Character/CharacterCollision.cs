using UnityEngine;

public class CharacterCollision : MonoBehaviour
{
    public CharacterMovement characterMovement;

    private Spot _Spot;
    private float _ExtractSpeed => Settings.s.extractSpeed;

    private void Start()
    {
        characterMovement.characterAnimator.SetFloat("ExtractSpeed", _ExtractSpeed);
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.GetComponent<Spot>()) return;

        _Spot = other.GetComponent<Spot>();

        bool _isExtract = _Spot != null && !characterMovement.joystick.isMove && !_Spot.isOnRecovery;

        characterMovement.characterAnimator.SetBool("IsExtract", _isExtract);
    }

    public void AddResources()
    {
        _Spot.SpawnResource();
    }

    public void RemoveResource()
    {

    }
}
