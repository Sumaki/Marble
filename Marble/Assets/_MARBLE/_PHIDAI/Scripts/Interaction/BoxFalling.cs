﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxFalling : MonoBehaviour
{
    public float waitTime = 1f;
    public float respawnBoxTimer = 2f;

    Vector3 startPosition = Vector3.zero;

    private void Start()
    {
         startPosition = gameObject.transform.position;
    }

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
        yield return new WaitForSeconds(respawnBoxTimer);
        obj.transform.position = startPosition;
        obj.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        obj.gameObject.GetComponent<Rigidbody>().useGravity = false;
    }
}