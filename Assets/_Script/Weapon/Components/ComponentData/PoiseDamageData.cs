using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoiseDamageData : ComponentData<AttackPoiseDamage>
{
    protected override void SetComponentDepedency()
    {
        ComponentDependecny = typeof(PoiseDamage);
    }
}
