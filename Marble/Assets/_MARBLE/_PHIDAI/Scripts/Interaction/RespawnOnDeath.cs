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
        gm_ = GameObject.FindGameObjectWithTag("GameManager");
    }

    private void Update()
    {
        if(dead)
            OnDeath();
    }
 
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject == player)
        {
            Debug.Log("I died");
            player.transform.position = gm_.GetComponent<GameManager>().currentRespawn.position;
            player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

        if(collision.gameObject.tag == "Pushable")
        {
            collision.gameObject.transform.position = collision.gameObject.GetComponent<PushingBox>().initialPosition;
            //collision.gameObject.GetComponent<PushingBox>()
        }
    }

    void OnDeath()
    {
        if (dead)
        {            
            Debug.Log("I died");         
            CharacterController cc = playerHumanoid.GetComponent<CharacterController>();
            cc.enabled = false;
            cc.transform.position = gm_.GetComponent<GameManager>().currentRespawn.position;
            cc.enabled = true;                       
            dead = false;
        }
    }
}
