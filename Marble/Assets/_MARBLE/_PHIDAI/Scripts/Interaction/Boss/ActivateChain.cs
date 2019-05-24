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
    bool startTime = false;

    void Start()
    {
        bm_ = GameObject.FindObjectOfType<BossManager>();
    }

    private void Update()
    {
        if (startTime)
            ChainTimerCheck();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Pushable" || other.gameObject.tag == "PlayerBall")
        {
            // turn on chain
            chain.GetComponent<MeshRenderer>().enabled = true;
            bm_.hits += 1;
            startTime = true;
            Debug.Log("Boss got chained! Amount of chains on boss: " + bm_.hits);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Pushable")
        {
            // chain is off
            chain.GetComponent<MeshRenderer>().enabled = false;
            bm_.hits -= 1;
            ResetChainTimer();
            Debug.Log("Boss got unchained! Amount of chains on boss: " + bm_.hits);
        }
    }

    void ChainTimerCheck()
    {
        timer += Time.deltaTime; // maybe add another variable
        //Debug.Log("Timer: " + timer);
        if(timer >= chainTimer)
        {
            bm_.hits -= 1;
            ResetChainTimer();
            chain.GetComponent<MeshRenderer>().enabled = false;           
            // Remove box and send it back to initial position
        }
    }

    void ResetChainTimer()
    {
        timer = 0;
        startTime = false;
    }
}
