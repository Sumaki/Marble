using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class FollowPath : MonoBehaviour
{

    [Header("Tool for following path")]
    public PathCreator pathCreator;
    public EndOfPathInstruction end;

    [Header("Speed of path")]
    public float speed;

    [Header("Enable/Disable One-Way Path")]
    public bool OneWay = true;

    [Header("Camera Distance")]
    public float CameraMaxDistance;
    public float CameraMinDistance;

    [Header("Field Of View")]
    public float desiredFov;
    //public float fovSwitchTravelSpeed; // max of one
    float startFov;
    //float startTime = 0.0f;


    #region Private Variables
    public GameObject player;
    GameObject otherObj;
    GameObject cameraObj;
    MorphManager mm_;
    float minDistStart;
    float maxDistStart; // cam

    float dstTravelled;
    Vector3 finishedPathPoint;
    bool entered = false;
    bool isPlayer = false;
    bool reset = false;
    float initialMovement = 0f;
    #endregion

    private void Start()
    {
        //player = GameObject.FindWithTag("PlayerBall");
        initialMovement = player.GetComponent<Character_Ball>().movementForce;
        Debug.Log(initialMovement);
        cameraObj = GameObject.FindWithTag("MainCamera");
        minDistStart = cameraObj.GetComponent<CameraCollision>().minDistance;
        maxDistStart = cameraObj.GetComponent<CameraCollision>().maxDistance;
        mm_ = GameObject.FindObjectOfType<MorphManager>();
        startFov = Camera.main.fieldOfView;
        // Debug.Log("StartFOV: " + startFov);
    }

    void Update()
    {


        if (entered && isPlayer)
        {
            if (Camera.main.fieldOfView != desiredFov)
                if (Camera.main.fieldOfView <= desiredFov)
                    Camera.main.fieldOfView = desiredFov;


            Debug.Log("Our FOV is: " + Camera.main.fieldOfView);
            StartTravel(player);
        }


        if (entered && !isPlayer)
            StartTravel(otherObj);

        if (!entered && isPlayer)
            EnablePlayer();

        if (reset)
            Reset();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerBall")
        {
            entered = true;
            isPlayer = true;
            mm_.canMorph = false;
        }

        if (other.gameObject.tag == "SpherePushable")
        {
            entered = true;
            isPlayer = false;
            otherObj = other.gameObject;
        }

    }

    void StartTravel(GameObject player)
    {


        // Debug.Log("Distance between them: " + Vector3.Distance(player.transform.position, pathCreator.path.GetPoint(1)));
        if (isPlayer)
        {
            cameraObj.GetComponent<CameraCollision>().minDistance = CameraMinDistance;
            cameraObj.GetComponent<CameraCollision>().maxDistance = CameraMaxDistance;
        }

        if (Vector3.Distance(player.transform.position, pathCreator.path.GetPoint(0.99f)) <= 1f && OneWay)
        {

            //Debug.Log("TURN OFF TRAVEL STATUS");            
            reset = true;
            entered = false;

            // keep the rigidbody velocity set to the speed of the travel?
            //player.GetComponent<Rigidbody>().AddForce(player.transform.forward); // use object's forward?
            if (isPlayer)
            {
                if (Camera.main.fieldOfView != startFov)
                    if (Camera.main.fieldOfView >= desiredFov)
                        Camera.main.fieldOfView = startFov;

                mm_.canMorph = true;
                player.GetComponent<Rigidbody>().velocity = player.transform.forward * speed;
            }
        }

        if(Vector3.Distance(player.transform.position, pathCreator.path.GetPoint(0.1f)) <= 1f && !OneWay)
        {
            reset = true;
            entered = false;
            if (isPlayer)
            {
                if (Camera.main.fieldOfView != startFov)
                    if (Camera.main.fieldOfView >= desiredFov)
                        Camera.main.fieldOfView = startFov;

                mm_.canMorph = true;
                player.GetComponent<Rigidbody>().velocity = player.transform.forward * speed;
            }
        }

        //if(isPlayer)
        //    DisablePlayer();

        if (end == EndOfPathInstruction.Stop)
        {
            dstTravelled += speed * Time.deltaTime;
            player.transform.position = pathCreator.path.GetPointAtDistance(dstTravelled, end);
            player.transform.rotation = pathCreator.path.GetRotationAtDistance(dstTravelled, end);
        }

        if(end == EndOfPathInstruction.Reverse)
        {
            dstTravelled -= speed * Time.deltaTime;
            player.transform.position = pathCreator.path.GetPointAtDistance(dstTravelled, end);
            player.transform.rotation = pathCreator.path.GetRotationAtDistance(dstTravelled, end);
        }
    }

    void DisablePlayer()
    {
        player.GetComponent<Character_Ball>().inputSpeed = 0f;
        player.GetComponent<Character_Ball>().movementForce = 0f;
    }

    void EnablePlayer()
    {
        cameraObj.GetComponent<CameraCollision>().minDistance = minDistStart;
        cameraObj.GetComponent<CameraCollision>().maxDistance = maxDistStart;
        //  dstTravelled = 0;
        player.GetComponent<Character_Ball>().inputSpeed = 1f;
        player.GetComponent<Character_Ball>().movementForce = initialMovement;
        isPlayer = false;
    }

    private void Reset()
    {
        dstTravelled = 0;
        reset = false;
    }
}