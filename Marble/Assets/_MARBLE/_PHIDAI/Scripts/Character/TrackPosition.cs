using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPosition : MonoBehaviour
{
    public bool human = false;
    public GameObject objToFollow;

    // Update is called once per frame
    void Update()
    {
        //if (MorphManager.state == MorphManager.MorphState.ball)
        //{
        //    human = false;
        //}


        if (!human)
        {
            if (MorphManager.state == MorphManager.MorphState.humanoid)
            {
                gameObject.transform.position = objToFollow.transform.position;
                human = true;
            }
        }

    }
}
