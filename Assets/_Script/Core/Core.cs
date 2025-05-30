﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
using System.Linq;

public class Core : MonoBehaviour
{
    private readonly List<CoreComponent> CoreComponents = new List<CoreComponent>();
    [field: SerializeField] public GameObject Root { get; private set; }

    private void Awake()
    {
        Root = Root ? Root : transform.parent.gameObject;
    }
    public void LogicUpdate()
    {
        //Movement.LogicUpdate();
        //Combat.LogicUpdate();
        foreach (CoreComponent component in CoreComponents)
        {
            component.LogicUpdate();
        }
    }
    public void AddComponent(CoreComponent component)
    {
        if (!CoreComponents.Contains(component))
        {
            CoreComponents.Add(component);
        }
    }

    public T GetCoreComponent<T>() where T : CoreComponent
    {
        var comp = CoreComponents.OfType<T>().FirstOrDefault();

        if (comp)
        {
            return comp;
        }
        comp = GetComponentInChildren<T>();
        if (comp)
        {
            return comp;
        }
        Debug.LogWarning($"{typeof(T)} not found on {transform.parent.name}");
        return null;
    }

    public T GetCoreComponent<T>(ref T value) where T : CoreComponent
    {
        value = GetCoreComponent<T>();
        return value;
    }
}
