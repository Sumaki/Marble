using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishOff : MonoBehaviour
{
    BossManager bm_;

    private void Start()
    {
        bm_ = GameObject.FindObjectOfType<BossManager>();
    }

    private void Update()
    {
        if (bm_.canFinishOff)
        {
            gameObject.GetComponent<BoxCollider>().enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerBall" && bm_.canFinishOff)
        {
            // finish
            Debug.Log("Killed Boss");
        }
    }

}
