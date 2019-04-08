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

    #region Private Variables
    GameObject player;
    GameObject otherObj;
    GameObject cameraObj;
    MorphManager mm_;
    float minDistStart;
    float maxDistStart; // cam

    float dstTravelled;
    Vector3 finishedPathPoint;
    bool entered = false;
    bool isPlayer = false;
    float initialMovement;
    #endregion

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        initialMovement = player.GetComponent<Character_Ball>().movementForce;
        cameraObj = GameObject.FindWithTag("MainCamera");
        minDistStart = cameraObj.GetComponent<CameraCollision>().minDistance;
        maxDistStart = cameraObj.GetComponent<CameraCollision>().maxDistance;
        mm_ = GameObject.FindObjectOfType<MorphManager>();
    }

    void Update()
    {


        if (entered && isPlayer)
            StartTravel(player);


        if (entered && !isPlayer)
            StartTravel(otherObj);
       
        if (!entered) 
            EnablePlayer();          
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            mm_.canMorph = false;
            entered = true;
            isPlayer = true;        
        }

        if(other.gameObject.tag == "Pushable")
        {
            entered = true;
            isPlayer = false;
            otherObj = other.gameObject;
        }

    }

    void StartTravel(GameObject player)
    {
        // Debug.Log("Distance between them: " + Vector3.Distance(player.transform.position, pathCreator.path.GetPoint(1)));
        cameraObj.GetComponent<CameraCollision>().minDistance = 3f;
        cameraObj.GetComponent<CameraCollision>().maxDistance = 6f;

        if (Vector3.Distance(player.transform.position, pathCreator.path.GetPoint(0.99f)) <= 1f && OneWay)
        {
          
            //Debug.Log("TURN OFF TRAVEL STATUS");            
            entered = false;
            mm_.canMorph = true;
            // keep the rigidbody velocity set to the speed of the travel?
            //player.GetComponent<Rigidbody>().velocity = Vector3.forward * speed; // use object's forward?
            
        }

        //if(isPlayer)
        //    DisablePlayer();

        dstTravelled += speed * Time.deltaTime;
        player.transform.position = pathCreator.path.GetPointAtDistance(dstTravelled,end);
        player.transform.rotation = pathCreator.path.GetRotationAtDistance(dstTravelled,end);
    }

    void DisablePlayer()
    {      
         player.GetComponent<Character_Ball>().inputSpeed = 0f;
         player.GetComponent<Character_Ball>().movementForce = 0f;    
    }

    void EnablePlayer()
    {
        // cameraObj.GetComponent<CameraCollision>().minDistance = minDistStart;
        //cameraObj.GetComponent<CameraCollision>().maxDistance = maxDistStart;       
        dstTravelled = 0;
        player.GetComponent<Character_Ball>().inputSpeed = 1f;
        player.GetComponent<Character_Ball>().movementForce = initialMovement;
    }
}
