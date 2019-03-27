using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnOnDeath : MonoBehaviour
{
    GameObject player;
    GameObject gm_;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gm_ = GameObject.Find("GameManager");
    }


    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.gameObject.tag == "PlayerParent" || other.gameObject.tag == "Player")
    //    {
    //        Debug.Log("I died");
    //        player.transform.position = gm_.GetComponent<GameManager>().currentRespawn.position;
    //        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
    //    }
    //}

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "PlayerParent" || collision.gameObject.tag == "Player")
        {
            Debug.Log("I died");
            player.transform.position = gm_.GetComponent<GameManager>().currentRespawn.position;
            player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}
