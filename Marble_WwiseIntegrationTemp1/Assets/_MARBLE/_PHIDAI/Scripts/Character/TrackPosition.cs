using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPosition : MonoBehaviour
{

    public Transform objToFollow; // follow this object's position

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = objToFollow.position;
    }


}
