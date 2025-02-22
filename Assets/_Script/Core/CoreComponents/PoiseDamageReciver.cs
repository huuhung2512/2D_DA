using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoiseDamageReciver : CoreComponent, IPoiseDamageable
{
    private Stats stats;
    public void DamagePoise(float amount)
    {
        stats.Poise.Decrease(amount);
    }
    protected override void Awake()
    {
        base.Awake();

        stats = core.GetCoreComponent<Stats>();
    }
}
