using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPuzzle : MonoBehaviour
{

    [Header("What objects do you want to reset?")]
    public GameObject[] objectsToReset;

    Vector3[] tempObjTransform = new Vector3[20];

    [Header("Is it a puzzle that locks?")]
    public bool puzzleResetLock;

    // Start is called before the first frame update
    void Start()
    {
        if (!puzzleResetLock)
        {
            for (int i = 0; i <= objectsToReset.Length - 1; i++)
            {
                tempObjTransform[i] = new Vector3(objectsToReset[i].transform.position.x, objectsToReset[i].transform.position.y, objectsToReset[i].transform.position.z);

            }
        }

        if (puzzleResetLock)
        {
            for (int i = 0; i <= objectsToReset.Length - 1; i++)
            {
                tempObjTransform[i] = new Vector3(objectsToReset[i].transform.position.x, objectsToReset[i].transform.position.y, objectsToReset[i].transform.position.z);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PlayerBall" || other.gameObject.tag == "PlayerHumanoid")
        {
            if (!puzzleResetLock)
            {
                for (int i = 0; i <= objectsToReset.Length - 1; i++)
                {
                    objectsToReset[i].transform.position = tempObjTransform[i];
                    // objectsToReset[i].transform.rotation = tempObjTransform[i].transform.rotation;

                }
            }

            if (puzzleResetLock)
            {
                for (int i = 0; i <= objectsToReset.Length - 1; i++)
                {
                    objectsToReset[i].transform.position = tempObjTransform[i];
                    objectsToReset[i].GetComponent<PushingBox>().enabled = true;

                }
            }
        }
    }
}
