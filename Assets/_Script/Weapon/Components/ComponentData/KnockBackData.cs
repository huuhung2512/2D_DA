using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBackData : ComponentData<AttackKnockBack>
{

    protected override void SetComponentDepedency()
    {
        ComponentDependecny = typeof(KnockBack);
    }
}
