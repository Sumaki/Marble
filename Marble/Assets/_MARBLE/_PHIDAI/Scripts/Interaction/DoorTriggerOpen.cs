using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTriggerOpen : MonoBehaviour
{
    public Animator ani;
    public bool worksWithBox = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!worksWithBox)
        {
            if (other.gameObject.tag == "PlayerBall" || other.gameObject.tag == "PlayerHumanoid")
            {
                ani.SetTrigger("Open");
            }
        }

        if (worksWithBox)
        {
            if (other.gameObject.tag == "Pushable")
                ani.SetTrigger("Open");
        }
    }
}
