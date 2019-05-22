using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePipe : MonoBehaviour
{
    public GameObject objAffected;
    public GameObject pipePathStart; 
    public GameObject[] pathsToDisableWhenTriggered;
    public GameObject[] pathsToEnableWhenTriggered;
    Animator ani;

    private void Start()
    {
        ani = objAffected.GetComponent<Animator>();
        for (int i = 0; i <= pathsToDisableWhenTriggered.Length - 1; i++)
            pathsToDisableWhenTriggered[i].GetComponent<BoxCollider>().enabled = false;
        for (int i = 0; i <= pathsToEnableWhenTriggered.Length - 1; i++)
            pathsToEnableWhenTriggered[i].GetComponent<BoxCollider>().enabled = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Pushable")
        {
            // Play the animation           
            Debug.Log(objAffected + " is being turned.");
            ani.SetBool("Turn180", true);
            ani.SetBool("TurnBack180", false);
            pipePathStart.GetComponent<BoxCollider>().enabled = false;         
            DisableEnablePaths();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Pushable")
        {
            // Play the animation           
            Debug.Log(objAffected + " is being turned.");
            ani.SetBool("Turn180", false);
            ani.SetBool("TurnBack180", true);
            pipePathStart.GetComponent<BoxCollider>().enabled = true;
            Revert();
        }
    }

    void DisableEnablePaths()
    {
        for(int i = 0; i <= pathsToDisableWhenTriggered.Length - 1; i++)
        {
            pathsToDisableWhenTriggered[i].GetComponent<BoxCollider>().enabled = false;
        }

        for(int i = 0; i <= pathsToEnableWhenTriggered.Length - 1; i++)
        {
            pathsToEnableWhenTriggered[i].GetComponent<BoxCollider>().enabled = true;
        }
    }

    void Revert()
    {
        for (int i = 0; i <= pathsToDisableWhenTriggered.Length - 1; i++)
        {
            pathsToDisableWhenTriggered[i].GetComponent<BoxCollider>().enabled = false;
        }

        for (int i = 0; i <= pathsToEnableWhenTriggered.Length - 1; i++)
        {
            pathsToEnableWhenTriggered[i].GetComponent<BoxCollider>().enabled = false;
        }
    }
}
