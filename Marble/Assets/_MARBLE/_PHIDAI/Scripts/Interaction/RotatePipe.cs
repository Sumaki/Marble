using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePipe : MonoBehaviour
{
    [Header("Animator that's affected of the object")]
    public GameObject objAffected;
    [Header("Beginning Path that will be enable/disable with triggers")]
    public GameObject pipePathStart; 
    [Header("Paths to disable when triggered")]
    public GameObject[] pathsToDisableWhenTriggered;
    [Header("Paths to enable when triggered")]
    public GameObject[] pathsToEnableWhenTriggered;

    [Header("Does this trigger play with other object triggers?")]
    public bool multiplePathTriggers;
    [Header("Triggers that play with this")]
    public GameObject[] triggers;
    [Header("Paths to needs to be off to be able to open the path we don't need")]
    public GameObject[] pathsOffForFinalPath;
    [Header("Paths to needs to be on to be able to open the path we need")]
    public GameObject[] pathsOnForFinalPath;
    //[Header("What path will open when the triggers are enabled")]
    //public GameObject[] pathsThatWillOpen;
    Animator ani;

    bool multipleTriggers = false;
    [Header("This is a check if it is triggered. Do not touch!")]
    public bool triggered = false;
    bool objTriggered = false;

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
            objTriggered = true;
            if(multiplePathTriggers)
                TriggerOverlap();
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
            objTriggered = false;
        }
    }

    void DisableEnablePaths()
    {
        if (!multipleTriggers)
        {
            for (int i = 0; i <= pathsToDisableWhenTriggered.Length - 1; i++)
            {
                pathsToDisableWhenTriggered[i].GetComponent<BoxCollider>().enabled = false;
            }

            for (int i = 0; i <= pathsToEnableWhenTriggered.Length - 1; i++)
            {
                pathsToEnableWhenTriggered[i].GetComponent<BoxCollider>().enabled = true;
            }
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

    void TriggerOverlap()
    {
        // condition for this to happen
        if (objTriggered)
        {
            for (int i = 0; i <= triggers.Length - 1; i++)
            {
                if (!triggers[i].GetComponent<RotatePipe>().triggered)
                {
                    multipleTriggers = false;
                    break;
                }
               else if (triggers[i].GetComponent<RotatePipe>().triggered)
                {
                    multipleTriggers = true;
                }
            }

            if (multipleTriggers)
                OpenFinalPaths();
        }
    }

    void OpenFinalPaths()
    {
        if (multipleTriggers)
        {
            for (int i = 0; i <= pathsOffForFinalPath.Length - 1; i++) { pathsOffForFinalPath[i].GetComponent<BoxCollider>().enabled = false; }
            for (int i = 0; i <= pathsOnForFinalPath.Length - 1; i++) { pathsOnForFinalPath[i].GetComponent<BoxCollider>().enabled = true; }
        }
    }
}
