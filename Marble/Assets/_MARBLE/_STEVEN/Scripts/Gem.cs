﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    public Animator ani;
    public Material newElementMaterial;
    public GameObject humanoid_mesh;

    // Floating Gem
    public float degreePerSecond;
    public float amplitude;
    public float frequency;

    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerBall" || other.gameObject.tag == "PlayerHumanoid")
        {
            
          
            StartCoroutine(TimerToSwtich());
            ani.SetTrigger("Open");
            Destroy(this.gameObject, 0.5f);
        }
    }

    IEnumerator TimerToSwtich()
    {
        yield return new WaitForSeconds(0.4f);
        humanoid_mesh.GetComponent<SkinnedMeshRenderer>().material = newElementMaterial;

    }

    void Start()
    {
        posOffset = transform.position;
    }

    void Update()
    {
        transform.Rotate(new Vector3(0f, Time.deltaTime * degreePerSecond, 0f), Space.World);

        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

        transform.position = tempPos;
    }
}
