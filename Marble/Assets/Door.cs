using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject door;

    void OnTriggerEnter(Collider other)
    {
        Destroy(door);
        Destroy(this.gameObject, 0.5f);
    }

}
