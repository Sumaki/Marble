using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCheck : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        gameObject.GetComponent<BoxCollider>().enabled = false;       
        if(MoveTarget.currentTarget < MoveTarget.maxTargets)
            MoveTarget.currentTarget += 1;
        Debug.Log("Move Target Increase to: " + MoveTarget.currentTarget);
    }
}
