﻿using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPlayer : SingletonBehavior<RespawnPlayer>
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.SetCheckpoint(transform.position);
        }
    }
}
