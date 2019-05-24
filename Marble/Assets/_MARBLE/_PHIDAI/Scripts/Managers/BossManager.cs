using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public Animator ani;
    // amount of chains hit onto the boss (3)
    public int hits = 0;
    public bool canFinishOff = false;



    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        AnimationCheck();
        CheckBossStatus();

    }

    void CheckBossStatus()
    {
        if (hits >= 3)
        {
          //  Debug.Log("Boss has all chains");
            canFinishOff = true;
        }

        if(hits < 3)
        {
            canFinishOff = false;
        }
    }

    void AnimationCheck()
    {
        if(hits == 0)
        {
            ani.SetBool("Idle", true);
            ani.SetBool("Chain_1", false);
            ani.SetBool("Chain_2", false);
            ani.SetBool("Chain_3", false);
        }

        if(hits == 1)
        {
            ani.SetBool("Idle", false);
            ani.SetBool("Chain_1", true);
            ani.SetBool("Chain_2", false);
            ani.SetBool("Chain_3", false);
        }
        if (hits == 2)
        {
            ani.SetBool("Idle", false);
            ani.SetBool("Chain_1", true);
            ani.SetBool("Chain_2", true);
            ani.SetBool("Chain_3", false);
        }
        if (hits == 3)
        {
            ani.SetBool("Idle", false);
            ani.SetBool("Chain_1", true);
            ani.SetBool("Chain_2", true);
            ani.SetBool("Chain_3", true);
        }
    }
}
