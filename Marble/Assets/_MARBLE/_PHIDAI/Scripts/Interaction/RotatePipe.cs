using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePipe : MonoBehaviour
{
    public GameObject objAffected;
    public GameObject pipePathStart;
    public GameObject secondPipePath;
    Animator ani;

    private void Start()
    {
        ani = objAffected.GetComponent<Animator>();
        secondPipePath.GetComponent<BoxCollider>().enabled = false;
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
            secondPipePath.GetComponent<BoxCollider>().enabled = true;
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
            secondPipePath.GetComponent<BoxCollider>().enabled = false;// play

        }
    }
}
