using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsToggle : MonoBehaviour
{
    private GameObject[] Light;
    public float WaitTime = 1f;

    void Awake()
    {
        Light = GameObject.FindGameObjectsWithTag("Startlights");


        foreach (GameObject i in Light)
        {
            i.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        StartCoroutine(TurnOn());
    }

    IEnumerator TurnOn()
    {
        yield return new WaitForSeconds(WaitTime);

        foreach (GameObject i in Light)
        {
            i.SetActive(true);
        }
    }

}
