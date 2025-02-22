using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class ComponentData
{
    [SerializeField, HideInInspector] private string name;
    public Type ComponentDependecny { get; protected set; }

    public ComponentData()
    {
        SetComponentName();
        SetComponentDepedency();
    }

    public void SetComponentName() => name = GetType().Name;

    protected abstract void SetComponentDepedency();
    public virtual void SetAttackDataName() { }
    public virtual void InitializeAttackData(int numberOfAttack) { }
}
[Serializable]
public abstract class ComponentData<T> : ComponentData where T : AttackData
{
    [SerializeField] private T[] attackData;
    public T[] AttackData { get => attackData; private set => attackData = value; }

    public override void SetAttackDataName()
    {
        base.SetAttackDataName();
        for (var i = 0; i < AttackData.Length; i++)
        {
            AttackData[i].SetAttackName(i + 1);
        }
    }
    public override void InitializeAttackData(int numberOfAttack)
    {
        base.InitializeAttackData(numberOfAttack);

        var oldLen = attackData != null ? AttackData.Length : 0;

        if (oldLen == numberOfAttack)
        {
            return;
        }

        Array.Resize(ref attackData, numberOfAttack);
        if (oldLen < numberOfAttack)
        {
            for (var i = oldLen; i < attackData.Length; i++)
            {
                var newObj = Activator.CreateInstance(typeof(T)) as T;
                attackData[i] = newObj;
            }
        }
        SetAttackDataName();
    }
}