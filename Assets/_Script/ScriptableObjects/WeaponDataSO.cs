using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[CreateAssetMenu(fileName = "newWeaponData", menuName = "Data/Weapon Data/Basic Weapon Data", order = 0)]
public class WeaponDataSO : ScriptableObject
{
    [field: SerializeField] public RuntimeAnimatorController AnimationController {  get;private set; }
    [field: SerializeField] public int NumberOfAttacks { get; private set; }

    [field: SerializeReference] public List<ComponentData> ComponentData { get; private set; }

    public T GetData<T>()
    {
        return ComponentData.OfType<T>().FirstOrDefault();
    }

    public List<Type> GetAllDependencies()
    {
        return ComponentData.Select(component => component.ComponentDependecny).ToList();
    }

    public void AddData(ComponentData data)
    {
        if (ComponentData.FirstOrDefault(t => t.GetType() == data.GetType()) != null)
        {
            return;
        }

        ComponentData.Add(data);
    }

    //[ContextMenu(itemName:"Add Sprite Data")]
    //private void AddSpriteData() => ComponentData.Add(new WeaponSpriteData());

    //[ContextMenu(itemName: "Add Movement Data")]
    //private void AddMovementData() => ComponentData.Add(new MovementData());

}
