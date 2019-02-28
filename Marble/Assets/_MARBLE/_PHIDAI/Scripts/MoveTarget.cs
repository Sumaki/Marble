using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTarget : MonoBehaviour
{
    [Header("Places where the ball will travel to")]
    public Transform[] targets;
    [Header("Speed of the travel")]
    public float speed;

    public static int currentTarget = 0;
    public static int maxTargets;
    private bool collided = false;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        maxTargets = targets.Length - 1;
    }

    private void FixedUpdate()
    {
        Debug.Log("Collided State :" + collided);

        if (collided)
            MoveTheTarget(player);
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
            player.GetComponent<Character_Ball>().gravityAmount = 0f;
            collided = true;
        }
    }

    void MoveTheTarget(GameObject obj)
    {
        //Debug.Log("Targets Length: " + targets.Length);

        if (currentTarget >= maxTargets)
            collided = false;

        if ( obj.transform.position != targets[currentTarget].position && collided) // dont use position maybe waypoint?
        {
            Vector3 pos = Vector3.MoveTowards(obj.transform.position, targets[currentTarget].position, speed * Time.deltaTime);
            obj.GetComponent<Rigidbody>().MovePosition(pos);
        }
        //else 
        //{
        //    currentTarget = currentTarget + 1;
        //    Debug.Log("CurrentTarget is increased");
        //}
       
            Debug.Log("Current Target: " + currentTarget);
       
    }
}
