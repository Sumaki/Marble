using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateRespawnPoint : MonoBehaviour
{

    public Transform newRespawn;
    GameObject gm_;

    private void Start()
    {
        gm_ = GameObject.Find("GameManager");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            gm_.GetComponent<GameManager>().currentRespawn = newRespawn;
        }
    }
}
