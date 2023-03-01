using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcePoint : Spot
{
    public ParticleSystem hitFX;
    public HealthBar healthBar;

    private void Start()
    {
        healthBar.Init(maxHitCount);
    }

    public override void SpawnResource()
    {
        base.SpawnResource();

        healthBar.ChangeValue(maxHitCount - tempHitCount);
        hitFX.Play();
    }

    public override IEnumerator RecoverySpot()
    {
        isOnRecovery = true;

        while (tempHitCount > 0)
        {
            tempHitCount--;
            healthBar.ChangeValue(maxHitCount - tempHitCount);

            yield return new WaitForSeconds(recoveryTime / maxHitCount);
        }

        isOnRecovery = false;
    }
}
