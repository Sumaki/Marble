using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTriggerHold : MonoBehaviour
{
    public Animator ani;
    public enum DoorState { Normal, Secret}
    public DoorState doorState; 

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "PlayerBall" || other.gameObject.tag == "PlayerHumanoid" || other.gameObject.tag == "Pushable")
        {
            if (doorState == DoorState.Normal)
            {
                ani.ResetTrigger("Close");
                ani.SetTrigger("Hold");
            }

            if(doorState == DoorState.Secret)
            {
                ani.SetTrigger("Open");
                ani.SetFloat("Speed", 1);
            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "PlayerBall" || other.gameObject.tag == "PlayerHumanoid" || other.gameObject.tag == "Pushable") 
        {
            if (doorState == DoorState.Normal)
            {
                ani.ResetTrigger("Hold");
                ani.SetTrigger("Close");
            }

            if(doorState == DoorState.Secret)
            {
               ani.SetFloat("Speed", -1);
            }
           
           
        }
    }
}
