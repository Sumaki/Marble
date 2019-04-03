using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnOnDeath : MonoBehaviour
{
    public GameObject player;
   
    public GameObject playerHumanoid;
    GameObject gm_;

    public  bool dead = false;

    private void Start()
    {      
        gm_ = GameObject.Find("GameManager");
    }

    private void Update()
    {
        if(dead)
            OnDeath();
    }


    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject  == player || other.gameObject == player)
    //    {

    //        //Debug.Log("I died");
    //        //player.transform.position = gm_.GetComponent<GameManager>().currentRespawn.position;
    //        //player.GetComponent<Rigidbody>().velocity = Vector3.zero;

    //        Debug.Log("I died");
    //        player.transform.position = gm_.GetComponent<GameManager>().currentRespawn.position;
    //        playerHumanoid.transform.position = gm_.GetComponent<GameManager>().currentRespawn.position;
    //        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
    //    }
    //}

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject == player || collision.gameObject == playerHumanoid)
        {
            Debug.Log("I died");
            player.transform.position = gm_.GetComponent<GameManager>().currentRespawn.position;
            playerHumanoid.transform.position = gm_.GetComponent<GameManager>().currentRespawn.position;
            player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }

    void OnDeath()
    {
        if (dead)
        {
            Debug.Log("I died");
            player.transform.position = gm_.GetComponent<GameManager>().currentRespawn.position;
            playerHumanoid.transform.position = gm_.GetComponent<GameManager>().currentRespawn.position;
            player.GetComponent<Rigidbody>().velocity = Vector3.zero;
            StartCoroutine(RespawnTime());
        }
    }

    IEnumerator RespawnTime()
    {
        yield return new WaitForSeconds(1f);
        dead = false;
    }
}
