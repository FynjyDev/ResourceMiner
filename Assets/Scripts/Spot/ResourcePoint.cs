using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcePoint : Spot
{
    [Header("Resource Point Pharameters")]

    public ParticleSystem hitFX;
    public ResourcePointHelth resourcePointHelth;

     public int tempHitCount;

    private void Start()
    {
        resourcePointHelth.Init(maxHitCount);
    }

    public override void OnPlayerStay()
    {
        if (characterCollision)
        {
            if (!isEnabled)
            {
                characterCollision.activeSpot = this;
                characterCollision.SetExtractAnimation(true);
            }
            else
            {
                characterCollision.activeSpot = null;
                characterCollision.SetExtractAnimation(false);
            }
        }

        base.OnPlayerStay();
    }

    public override void OnPlayerExit()
    {
        characterCollision.activeSpot = null;
        characterCollision.SetExtractAnimation(false);

        characterCollision = null;

        base.OnPlayerExit();
    }

    public override void SpawnResource()
    {
        if (tempHitCount < maxHitCount)
        {
            base.SpawnResource();

            resourcePointHelth.ChangeValue(maxHitCount - tempHitCount);
            hitFX.Play();

            tempHitCount++;
        }
        else StartCoroutine(SpotDelay());
       
    }

    public IEnumerator SpotDelay()
    {
        isEnabled = true;

        while (tempHitCount > 0)
        {
            tempHitCount--;
            resourcePointHelth.ChangeValue(maxHitCount - tempHitCount);

            yield return new WaitForSeconds(recoveryTime / maxHitCount);
        }

        isEnabled = false;
    }
}
