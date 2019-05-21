﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateRespawnPoint : MonoBehaviour
{

    public Transform newRespawn;
    GameObject gm_;

    private void Start()
    {
        gm_ = GameObject.FindGameObjectWithTag("GameManager");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PlayerBall" || other.gameObject.tag == "PlayerHumanoid")
        {
            gm_.GetComponent<GameManager>().currentRespawn = newRespawn;
        }
    }
}