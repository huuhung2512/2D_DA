using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : CoreComponent
{
    [SerializeField] private GameObject[] deathParticles;
    [SerializeField] private GameObject[] spawn;

    private ParticleManager ParticleManager => particleManager ? particleManager : core.GetCoreComponent(ref particleManager);
    private ParticleManager particleManager;
    private Stats Stats => stats ? stats : core.GetCoreComponent(ref stats);
    private Stats stats;

    public void Die()
    {
        string typeEnemyName = core.transform.parent.name.ToLower();
        foreach (var particle in deathParticles)
        {
            ParticleManager.StartParticle(particle);
        }
        if (typeEnemyName.Contains("slime"))
        {
            Slime s = core.transform.parent.GetComponent<Slime>();
            foreach (var spawns in spawn)
            {
                s.SpawnSlime(spawns.GetComponent<Slime>(), 3, 1);
            }
        }
        else
        {
            foreach (var spawns in spawn)
            {
                ParticleManager.SpawnObjectsInMultipleDirections(spawns, 3, 1);
            }
        }
        Destroy(core.transform.parent.gameObject);
        //core.transform.parent.gameObject.SetActive(false);
        if (core.transform.parent.CompareTag("Player"))
        {
            RespawnPlayer.Instance.Respawn();
        }
    }

    private void OnEnable()
    {
        Stats.Health.OnCurrentValueZero += Die;
    }
    private void OnDisable()
    {
        Stats.Health.OnCurrentValueZero -= Die;
    }
}
