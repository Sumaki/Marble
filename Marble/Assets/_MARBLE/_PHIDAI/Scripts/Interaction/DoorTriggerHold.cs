using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTriggerHold : MonoBehaviour
{
    public Animator ani;

   
    public enum PlayerState { Humanoid, Ball}
    [Header("What object can interact?")]
    public PlayerState whichForm;

   
    public enum DoorState { Normal, Secret}
    [Header("What kind of door will it activate?")]
    public DoorState doorState;

    [Header("Can objects interact with it? (Put a Pushable tag on it!)")]
    public bool objectAllowed;

   
    private void OnTriggerStay(Collider other)
    {
        if (whichForm == PlayerState.Humanoid)
        {
            if (other.gameObject.tag == "PlayerHumanoid")
                CheckDoorState();
        }

        if(whichForm == PlayerState.Ball)
        {
            if (other.gameObject.tag == "PlayerBall")
                CheckDoorState();
        }

        if (objectAllowed)
        {
            if (other.gameObject.tag == "Pushable")
                CheckDoorState();
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

    void CheckDoorState()
    {
        if (doorState == DoorState.Normal)
        {
            ani.ResetTrigger("Close");
            ani.SetTrigger("Hold");
        }

        if (doorState == DoorState.Secret)
        {
            ani.SetTrigger("Open");
            ani.SetFloat("Speed", 1);
        }
    }
}
