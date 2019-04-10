using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTriggerHold : MonoBehaviour
{
    public Animator ani;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PlayerBall" || other.gameObject.tag == "PlayerHumanoid")
        {
            ani.SetTrigger("Open");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "PlayerBall" || other.gameObject.tag == "PlayerHumanoid") 
        {
            ani.SetTrigger("Closed");
        }
    }
}
