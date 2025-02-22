using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : WeaponComponent<DamageData, AttackDamage>
{
    private ActionHitbox hitBox;
    private void HandleDetectedCollider2D(Collider2D[] collider)
    {
        foreach (var item in collider)
        {
            if (item.TryGetComponent(out IDamageable damageable))
            {
                damageable.Damage(currentAttackData.Amount);
            }
        }
    }
    protected override void Start()
    {
        base.Start();

        hitBox = GetComponent<ActionHitbox>();
        hitBox.OnDetectedCollider2D += HandleDetectedCollider2D;
    }
  
    protected override void OnDesTroy()
    {
        base.OnDesTroy();

        hitBox.OnDetectedCollider2D -= HandleDetectedCollider2D;
    }
}
