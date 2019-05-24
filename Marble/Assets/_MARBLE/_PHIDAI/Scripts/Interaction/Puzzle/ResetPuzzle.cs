using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPuzzle : MonoBehaviour
{

    [Header("What objects do you want to reset?")]
    public GameObject[] objectsToReset;

    GameObject[] tempObjectsToReset;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i <= objectsToReset.Length - 1; i++)
        {
            tempObjectsToReset[i].transform.position = objectsToReset[i].transform.position;
            tempObjectsToReset[i].transform.rotation = objectsToReset[i].transform.rotation;
        }           
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PlayerBall" || other.gameObject.tag == "PlayerHumanoid")
        {
            for(int i = 0; i <= objectsToReset.Length - 1; i++)
            {
                objectsToReset[i].transform.position = tempObjectsToReset[i].transform.position;
                objectsToReset[i].transform.rotation = tempObjectsToReset[i].transform.rotation;

            }
        }
    }
}
