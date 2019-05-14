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
                CheckDoorStateEntered();
        }

        if(whichForm == PlayerState.Ball)
        {
            if (other.gameObject.tag == "PlayerBall")
                CheckDoorStateEntered();
        }

        if (objectAllowed)
        {
            if (other.gameObject.tag == "SpherePushable")
                CheckDoorStateEntered();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (whichForm == PlayerState.Humanoid)
        {
            if (other.gameObject.tag == "PlayerHumanoid")
                CheckDoorStateExit();
        }

        if (whichForm == PlayerState.Ball)
        {
            if (other.gameObject.tag == "PlayerBall")
                CheckDoorStateExit();
        }

        if (objectAllowed)
        {
            if (other.gameObject.tag == "SpherePushable")
                CheckDoorStateExit();
        }
    }

    void CheckDoorStateEntered()
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

    void CheckDoorStateExit()
    {
        if (doorState == DoorState.Normal)
        {
            ani.ResetTrigger("Hold");
            ani.SetTrigger("Close");
        }

        if (doorState == DoorState.Secret)
        {
            ani.SetFloat("Speed", -1);
        }
    }
}
