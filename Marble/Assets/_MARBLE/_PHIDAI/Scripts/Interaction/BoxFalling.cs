using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxFalling : MonoBehaviour
{
    public float waitTime = 1f;

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(FallTimer(gameObject));
        }
    }


    IEnumerator FallTimer(GameObject obj)
    {
        yield return new WaitForSeconds(waitTime);
        obj.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        obj.gameObject.GetComponent<Rigidbody>().useGravity = true;
       
    }
}
