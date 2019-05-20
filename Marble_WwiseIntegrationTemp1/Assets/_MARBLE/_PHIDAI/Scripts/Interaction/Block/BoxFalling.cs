using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxFalling : MonoBehaviour
{
    public Animator ani;
    public float waitTime = 1f;
    public float respawnBoxTimer = 2f;

    public bool humanoidBoxFall = false;

    Vector3 startPosition = Vector3.zero;

    private void Start()
    {
         startPosition = gameObject.transform.position;
    }

    private void Update()
    {
        if (humanoidBoxFall)
        {
            Debug.Log("Box is falling");
            StartCoroutine(FallTimer(gameObject));
        }   
    }

    private void OnCollisionExit(Collision collision)
    {

        if (collision.gameObject.tag == "PlayerBall" || collision.gameObject.tag == "PlayerHumanoid")
        {
            //Debug.Log("Box is falling");
            StartCoroutine(FallTimer(gameObject));
        }
    }


    IEnumerator FallTimer(GameObject obj)
    {
        yield return new WaitForSeconds(waitTime);        
        ani.SetBool("Fall",true);
        yield return new WaitForSeconds(respawnBoxTimer);
        ani.SetBool("Fall", false);
        obj.transform.position = startPosition;
        humanoidBoxFall = false;
    }
}
