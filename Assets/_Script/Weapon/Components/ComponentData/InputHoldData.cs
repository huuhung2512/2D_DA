using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHoldData : ComponentData
{
    protected override void SetComponentDepedency()
    {
        ComponentDependecny = typeof(InputHold);
    }
}
