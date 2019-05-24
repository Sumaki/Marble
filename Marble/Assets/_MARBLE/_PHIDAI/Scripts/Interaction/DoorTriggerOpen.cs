using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTriggerOpen : MonoBehaviour
{
    public Animator ani;
    public AudioSource openDoorClip;
    public bool worksWithBox = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!worksWithBox)
        {
            if (other.gameObject.tag == "PlayerBall" || other.gameObject.tag == "PlayerHumanoid")
            {
                {
                    ani.SetTrigger("Open");
                    openDoorClip.Play();
                }
            }
        }

        if (worksWithBox)
        {
            if (other.gameObject.tag == "Pushable")
            {
                ani.SetTrigger("Open");
                openDoorClip.Play();
            }
        }
    }
}
