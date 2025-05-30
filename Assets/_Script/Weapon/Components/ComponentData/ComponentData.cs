using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Hung.Weapons.Components
{
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
        // True if component data is the same for every attack, avoiding the issue of having to set up repeat data
        [SerializeField] private bool repeatData;
        [SerializeField] private T[] attackData;
        // Use this to get the data of a specific attack. Accounts for components that repeats data for all attacks.
        public T GetAttackData(int index) => attackData[repeatData ? 0 : index];
        public T[] GetAllAttackData() => attackData;
        public override void SetAttackDataName()
        {
            base.SetAttackDataName();

            for (var i = 0; i < attackData.Length; i++)
            {
                attackData[i].SetAttackName(i + 1);
            }
        }
        public override void InitializeAttackData(int numberOfAttacks)
        {
            base.InitializeAttackData(numberOfAttacks);

            var newLen = repeatData ? 1 : numberOfAttacks;

            var oldLen = attackData != null ? attackData.Length : 0;

            if (oldLen == newLen)
                return;

            Array.Resize(ref attackData, newLen);

            if (oldLen < newLen)
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
}
