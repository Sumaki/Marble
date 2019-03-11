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
    float dstTravelled;
    Vector3 finishedPathPoint;
    bool entered = false;
    #endregion

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {     
        if (entered)
            StartTravel(player);
        if (!entered)
            EnablePlayer();          
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            entered = true;           
        }

    }

    void StartTravel(GameObject player)
    {
       // Debug.Log("Distance between them: " + Vector3.Distance(player.transform.position, pathCreator.path.GetPoint(1)));

        if (Vector3.Distance(player.transform.position, pathCreator.path.GetPoint(0.99f)) <= 1f && OneWay)
        {
            //Debug.Log("TURN OFF TRAVEL STATUS");            
            entered = false;
            
        }

        DisablePlayer();

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
        dstTravelled = 0;
        player.GetComponent<Character_Ball>().inputSpeed = 1f;
        player.GetComponent<Character_Ball>().movementForce = 40f;
    }
}
