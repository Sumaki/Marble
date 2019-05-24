using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateChain : MonoBehaviour
{
    [Header("Which chain will activate")]
    public GameObject chain;
    [Header("Timer for chain")]
    public float chainTimer;

    BossManager bm_;
    float timer;

    void Start()
    {
        bm_ = GameObject.FindObjectOfType<BossManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Pushable")
        {
            // turn on chain
            bm_.hits += 1;
            ChainTimerCheck();
            Debug.Log("Boss got chained! Amount of chains on boss: " + bm_.hits);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Pushable")
        {
            // chain is off
            bm_.hits -= 1;
            ResetChainTimer();
            Debug.Log("Boss got unchained! Amount of chains on boss: " + bm_.hits);
        }
    }

    void ChainTimerCheck()
    {
        timer += Time.deltaTime; // maybe add another variable
        if(timer >= chainTimer)
        {
           // Remove box and send it back to initial position
        }
    }

    void ResetChainTimer()
    {
        timer = 0;
    }
}
