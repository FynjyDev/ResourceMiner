using UnityEngine;

public class CharacterCollision : MonoBehaviour
{
    public CharacterMovement characterMovement;

    public Spot activeSpot;
    private float _ExtractSpeed => Settings.s.extractSpeed;

    private void Start()
    {
        characterMovement.characterAnimator.SetFloat("ExtractSpeed", _ExtractSpeed);
    }

    public void SetExtractAnimation(bool _isExtract)
    {
        characterMovement.characterAnimator.SetBool("IsExtract", _isExtract);
    }

    public void AddResources()
    {
        if (!activeSpot || activeSpot.characterCollision == null || activeSpot.characterCollision != this)
        {
            SetExtractAnimation(false);
            return;
        }

        activeSpot.SpawnResource();
    }
}
