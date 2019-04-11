using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxFalling : MonoBehaviour
{
    public Animator ani;
    public float waitTime = 1f;
    public float respawnBoxTimer = 2f;

    Vector3 startPosition = Vector3.zero;

    private void Start()
    {
         startPosition = gameObject.transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "PlayerBall" || collision.gameObject.tag == "PlayerHumanoid")
        {
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
    }
}
