using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTriggerOpen : MonoBehaviour
{
    public Animator ani;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ani.SetTrigger("Open");
        }
    }
}
