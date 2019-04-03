using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorphManager : MonoBehaviour
{
    [Header("Morph Model Ball")]
    public GameObject blendObjectBall;
    [Header("Morph Model Humanoid")]
    public GameObject blendObjectHumanoid;
    //[Header("Ball Model")]
    //public GameObject playerBall;
    //[Header("Humanoid Model")]
    //public GameObject playerHumanoid;
    [Header("Camera")]
    public GameObject camera_;
    public GameObject ballCamera;
    public GameObject humanoidCamera;
    [Header("Player's Ball Scripts Object")]
    public GameObject ballObjScripts;
    [Header("Player's Humanoid Scripts Object")]
    public GameObject humanoidObjScripts;
    #region Morph Private Varibles
    SkinnedMeshRenderer smr_;
    SkinnedMeshRenderer smr_humanoid;
    bool morph = false;
    bool doneMorph;
    Quaternion startRotation;
    public enum MorphState { ball, humanoid }
    [Header("State of the Character")]
    public static MorphState state = MorphState.ball;
    float t = 0.0f;
    #endregion

   
    void Start()
    {
        smr_ =  blendObjectBall.GetComponent<SkinnedMeshRenderer>();
        doneMorph = true;
        startRotation = blendObjectBall.transform.rotation;
        CheckStartStatus();

        //Collider[] cols = GameObject.FindGameObjectWithTag("Player").GetComponents<Collider>();
        //foreach (Collider c in cols)
        //    Physics.IgnoreCollision(playerObjScripts.GetComponent<CharacterController>(), c);
    }

    
    void Update()
    {
        //Debug.Log(playerObjScripts.GetComponent<CharacterController>().detectCollisions);
        //Debug.Log("Morph Status: " + morph);
        GlobalInput();
        MorphChecker(state);
        Morph(); 
        IgnoreCollisionBetweenPlayerCollider();
        //humanoidObjScripts.transform.rotation = Quaternion.identity;
       
  
    }

    void CheckStartStatus()
    {
        switch (state)
        {
            case MorphState.ball:
                BallProperties();               
                break;
            case MorphState.humanoid:
                HumanoidProperties();              
                break;
        }
    }

    void GlobalInput()
    {
        if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Submit_B")) // temp input
        {
            if(state == MorphState.ball)
                humanoidObjScripts.GetComponent<CharacterAnimationState>().state = CharacterAnimationState.CharacterState.morphHumanoid;
            if (state == MorphState.humanoid)   
                humanoidObjScripts.GetComponent<CharacterAnimationState>().state = CharacterAnimationState.CharacterState.morphBall;
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
            DisableWhileMorphing();
            ballObjScripts.transform.rotation = startRotation;
            smr_.SetBlendShapeWeight(0, Mathf.Lerp(100, 0, t));
            t += 3f * Time.deltaTime;
            if(smr_.GetBlendShapeWeight(0) == 0) {
                ChangeMesh(MorphState.humanoid); smr_.SetBlendShapeWeight(0,100);
            state = MorphState.humanoid;  HumanoidProperties(); doneMorph = true;
            }
        }

        if (state == MorphState.humanoid && !doneMorph && morph)
        {
            DisableWhileMorphing();
            //blendObject.transform.rotation = startRotation;
            smr_.SetBlendShapeWeight(0, Mathf.Lerp(0, 100, t));
            t += 3f * Time.deltaTime;
            if (smr_.GetBlendShapeWeight(0) == 100) {
                ChangeMesh(MorphState.ball); smr_.SetBlendShapeWeight(0, 0);
                state = MorphState.ball;  BallProperties(); doneMorph = true;
            }
        }
    }

    void MorphChecker(MorphState state)
    {
        switch (state)
        {
            case MorphState.ball:
                smr_ = blendObjectBall.GetComponent<SkinnedMeshRenderer>();
                break;
            case MorphState.humanoid:
                smr_ = blendObjectHumanoid.GetComponent<SkinnedMeshRenderer>();
                break;
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
               
                EnableObject(ballObjScripts, humanoidObjScripts);
                DisableObject(humanoidObjScripts);
                ChangeCameraTarget(ballCamera, ballObjScripts);
                break;
            case MorphState.humanoid:
                //disable ball gameobject, enable humanoid gameobject
                 
                EnableObject(humanoidObjScripts, ballObjScripts);
               DisableObject(ballObjScripts);
               ChangeCameraTarget(humanoidCamera, humanoidObjScripts);
                break;
        }
    }

    void EnableObject(GameObject obj, GameObject obj2)
    {
        // enable
        obj.transform.position = obj2.transform.position; // temp
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

    void BallProperties()
    {
       
        ballObjScripts.GetComponent<Character_Ball>().enabled = true;
        ballObjScripts.GetComponent<SphereCollider>().enabled = true;
        ballObjScripts.GetComponent<Rigidbody>().isKinematic = false;
        ballObjScripts.GetComponent<Rigidbody>().detectCollisions = true;
        humanoidObjScripts.GetComponent<Character_Humanoid>().enabled = false;
        // playerObjScripts.GetComponent<CharacterController>().enabled = false;
        humanoidObjScripts.GetComponent<CharacterController>().detectCollisions = false;
        //humanoidObjScripts.GetComponent<CharacterController>().transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    void HumanoidProperties()
    {
       // humanoidObjScripts.transform.position = ballObjScripts.transform.position;
        ballObjScripts.GetComponent<Character_Ball>().enabled = false;
        ballObjScripts.GetComponent<SphereCollider>().enabled = false;
        ballObjScripts.GetComponent<Rigidbody>().isKinematic = true;
        ballObjScripts.GetComponent<Rigidbody>().detectCollisions = false;
        humanoidObjScripts.GetComponent<Character_Humanoid>().enabled = true;
        // playerObjScripts.GetComponent<CharacterController>().enabled = true;
        humanoidObjScripts.GetComponent<CharacterController>().detectCollisions = true;
    }

    /// <summary>
    /// Make sure to rotate the object to match it's forward upwards so that it animates and morphs into the form that we need (ball to humanoid)
    /// </summary>
    void RotateBallToMatchHumanoid()
    {
        ballObjScripts.transform.rotation = startRotation;
    }

    void DisableWhileMorphing()
    {
        // also stop ball from rolling
       // ballObjScripts.GetComponent<Rigidbody>().velocity = Vector3.zero;
        //ballObjScripts.GetComponent<Character_Ball>().enabled = false;
        //humanoidObjScripts.GetComponent<Character_Humanoid>().enabled = false;        
    }

    void IgnoreCollisionBetweenPlayerCollider()
    {   
        Physics.IgnoreCollision(humanoidObjScripts.GetComponent<CharacterController>(), ballObjScripts.GetComponent<SphereCollider>(),true);       
    }

}
