using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAfterImagePool : SingletonBehavior<PlayerAfterImagePool> 
{
    [SerializeField] private GameObject afterImagePrefab;
    private Queue<GameObject> availableObject = new Queue<GameObject>();
     
    private void Start()
    {
        GrowPool();
    }
    private void GrowPool()
    {
        for (int i = 0; i < 10; i++)
        {
            var instanceToAdd = Instantiate(afterImagePrefab);
            instanceToAdd.transform.SetParent(transform);
            AddToPool(instanceToAdd);
        }
    }

    public void AddToPool(GameObject instance)
    {
        instance.SetActive(false);
        availableObject.Enqueue(instance);
    }
    public GameObject GetFromPool()
    {
        if(availableObject.Count == 0)
        {
            GrowPool();
        }
        var instance = availableObject.Dequeue();
        instance.SetActive(true);
        return instance;
    }
}
