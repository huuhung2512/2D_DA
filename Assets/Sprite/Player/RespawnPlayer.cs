using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPlayer : SingletonBehavior<RespawnPlayer>
{
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private GameObject player;
    [SerializeField] private float respawnTime;
    private float respawnTimeStart;
    public bool respawn;
    private CinemachineVirtualCamera CVC;

    private void Start()

    {
        CVC = GameObject.Find("Player Camera").GetComponent<CinemachineVirtualCamera>();

    }

    private void Update()
    {
        CheckRespawn();
    }
    public void Respawn()
    {
        respawnTimeStart = Time.time;
        respawn = true;
    }

    private void CheckRespawn()
    {
        if (Time.time >= respawnTimeStart + respawnTime && respawn)
        {
            var playerTemp = Instantiate(player, respawnPoint);
            playerTemp.transform.position = respawnPoint.position;
            CVC.m_Follow = playerTemp.transform;
            //player.gameObject.SetActive(true);
            Stats newStats = playerTemp.GetComponentInChildren<Stats>();
            PlayerHealthBar.Instance.SetStats(newStats);
            PlayerHealthBar.Instance.UpdateHealthBar();
            respawn = false;
        }
    }
}
