using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePipe : MonoBehaviour
{
    public GameObject objAffected;
    Animator ani;

    private void Start()
    {
        ani = objAffected.GetComponent<Animator>();
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Pushable")
        {
            // Play the animation           
            Debug.Log(objAffected + " is being turned."); ani.SetBool("Turn180", true); ani.SetBool("TurnBack180", false); //play            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Pushable")
        {
            // Play the animation           
            Debug.Log(objAffected + " is being turned."); ani.SetBool("Turn180", false); ani.SetBool("TurnBack180", true); // play
            
        }
    }
}
