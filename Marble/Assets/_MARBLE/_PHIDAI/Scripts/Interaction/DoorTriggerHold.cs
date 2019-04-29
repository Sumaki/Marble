using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTriggerHold : MonoBehaviour
{
    public Animator ani;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PlayerBall" || other.gameObject.tag == "PlayerHumanoid" || other.gameObject.tag == "Pushable")
        {
            ani.SetTrigger("Hold");
            ani.SetFloat("Speed", 1);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "PlayerBall" || other.gameObject.tag == "PlayerHumanoid" || other.gameObject.tag == "Pushable") 
        {           
            ani.SetFloat("Speed", -1);
        }
    }
}
