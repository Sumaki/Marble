using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsToggle : MonoBehaviour
{
    private GameObject[] Light;
    public float WaitTime = 1f;

    void Awake()
    {
      
    Light = GameObject.FindGameObjectsWithTag("StartLights");
 
    }

    private void Start()
    {
        
       
        foreach (GameObject i in Light)
        {
            Debug.Log("Turning off lights");
           i.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerHumanoid" || other.gameObject.tag == "PlayerBall")
        {
            Debug.Log("WE ARE INSIDE THE TRIGGER");
            StartCoroutine(TurnOn());
        }
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
