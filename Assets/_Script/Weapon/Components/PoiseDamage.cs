using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoiseDamage : WeaponComponent<PoiseDamageData, AttackPoiseDamage>
{

    private ActionHitbox hitBox;

    private void HandleDetectCollider2D(Collider2D[] colliders)
    {
        foreach (var item in colliders)
        {
            if(item.TryGetComponent(out IPoiseDamageable poiseDamageable))
            {
                poiseDamageable.DamagePoise(currentAttackData.Amount);
            }
        }
    }

    protected override void Start()
    {
        base.Start();

        hitBox = GetComponent<ActionHitbox>();
        hitBox.OnDetectedCollider2D += HandleDetectCollider2D;
    }
    protected override void OnDesTroy()
    {
        base.OnDesTroy();

        hitBox.OnDetectedCollider2D -= HandleDetectCollider2D;
    }

}
