﻿using Hung.Combat.Damage;
using UnityEngine;
using static Hung.Utilities.CombatDamageUtilities; //(2)



namespace Hung.Weapons.Components
{
    public class DamageOnHitBoxAction : WeaponComponent<DamageOnHitBoxActionData, AttackDamage>
    {
        private ActionHitbox hitBox;
        
        private void HandleDetectCollider2D(Collider2D[] colliders)
        {
            // Notice that this is equal to (1), the logic has just been offloaded to a static helper class. Notice the using statement (2) is static, allowing as to call the Damage function directly instead of saying
            // Bardent.Utilities.CombatUtilities.Damage(...);
            
            
            TryDamage(colliders, new DamageData(currentAttackData.Amount, Core.Root), out _); 
            
            //(1)
            // foreach (var item in colliders)
            // {
            //     if (item.TryGetComponent(out IDamageable damageable))
            //     {
            //         damageable.Damage(new Combat.Damage.DamageData(currentAttackData.Amount, Core.Root));
            //     }
            // }
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
}