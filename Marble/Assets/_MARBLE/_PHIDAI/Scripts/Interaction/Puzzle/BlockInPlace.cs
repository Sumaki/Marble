using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockInPlace : MonoBehaviour
{

    public GameObject puzzleObj;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Pushable")
        {
            other.gameObject.transform.position = gameObject.transform.position;
            // set that the box cannot be pushed anymore or perhaps change the color
            other.gameObject.GetComponent<PushingBox>().enabled = false;
            puzzleObj.GetComponent<PuzzleConditions>().amountOfCompletedPieces += 1;
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
