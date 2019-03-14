using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorphManager : MonoBehaviour
{
    [Header("Morph Model")]
    public GameObject blendObject;
    [Header("Ball Model")]
    public GameObject playerBall;
    [Header("Humanoid Model")]
    public GameObject playerHumanoid;
    [Header("Camera")]
    public GameObject camera_;
    public GameObject ballCamera;
    public GameObject humanoidCamera;
    #region Morph Private Varibles
    SkinnedMeshRenderer smr_;
    bool morph = false;
    bool doneMorph;
    enum MorphState { ball, humanoid }
    MorphState state = MorphState.humanoid  ;
    float t = 0.0f;
    #endregion

   
    void Start()
    {
        smr_ = blendObject.GetComponent<SkinnedMeshRenderer>();
        doneMorph = true;
    }

    
    void Update()
    {
        //Debug.Log("Morph Status: " + morph);
        GlobalInput();
        Morph();
    }

    void GlobalInput()
    {
        if (Input.GetKeyDown(KeyCode.E)) // temp input
        {
            if (doneMorph && !morph)
            {
                doneMorph = false;
                morph = true;
            }
        }
    }

    void Morph()
    {   
        if(smr_.GetBlendShapeWeight(0) == 100 && doneMorph)
        {
            morph = false;
            t = 0.0f;           
        }

        if(smr_.GetBlendShapeWeight(0) == 0 && doneMorph)
        {
            morph = false;
            t = 0.0f;          
        }

        if (state == MorphState.ball && !doneMorph && morph)
        {          
            smr_.SetBlendShapeWeight(0, Mathf.Lerp(0, 100, t));
            t += 1f * Time.deltaTime;
                if(smr_.GetBlendShapeWeight(0) == 100) { doneMorph = true; state = MorphState.humanoid; ChangeMesh(state); }
        }

        if (state == MorphState.humanoid && !doneMorph && morph)
        {
            smr_.SetBlendShapeWeight(0, Mathf.Lerp(100, 0, t));
            t += 1f * Time.deltaTime;
            if (smr_.GetBlendShapeWeight(0) == 0) { doneMorph = true; state = MorphState.ball; ChangeMesh(state); }
        }
    }

    void CheckCameraDirection()
    {

    }

    void ChangeMesh(MorphState state)
    {
        switch (state)
        {
            case MorphState.ball:
                //disable humanoid gameobject, enable ball gameobject
                DisableObject(playerHumanoid);
                EnableObject(playerBall,playerHumanoid);
                ChangeCameraTarget(ballCamera, playerBall);
                break;
            case MorphState.humanoid:
                //disable ball gameobject, enable humanoid gameobject
                DisableObject(playerBall);
                EnableObject(playerHumanoid,playerBall);
                ChangeCameraTarget(humanoidCamera, playerHumanoid);
                break;
        }
    }

    void EnableObject(GameObject obj, GameObject obj2)
    {
        // enable
        obj.transform.position = obj2.transform.position;
        obj.SetActive(true);

    }

    void DisableObject(GameObject obj)
    {
        // disable
        obj.SetActive(false);
    }

    void ChangeCameraTarget(GameObject cameraObj, GameObject playerObj)
    {
        // switch camera targets
        camera_.GetComponent<CameraFollow>().cameraFollowObj = cameraObj;
        camera_.GetComponent<CameraFollow>().playerObj = playerObj;
    }


}
