using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushingBox : MonoBehaviour
{

    public GameObject player;
    public bool pushing = false;
    CharacterController cc;
    CharacterAnimationState characterState; 

    Vector3 p1;
    Vector3 movePosition;
    bool canPush = false;
    bool isPushing = false;
    float fracJourney = 0f;
    public float input;


    // Start is called before the first frame update
    void Start()
    {
        cc = player.GetComponent<CharacterController>();
        characterState = player.GetComponent<CharacterAnimationState>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Final position of spherecast: " + p1);

        if (characterState.state == CharacterAnimationState.CharacterState.push && !canPush)
            Check2();//Check();

        if (canPush)
        {
            if (input > 0 && !isPushing ) // maybe adjust
            {
                isPushing = true;
            }
        }

        if (isPushing)
        {
            canPush = false;
            transform.position = Vector3.MoveTowards(transform.position, movePosition, 5 * Time.deltaTime);
        }

        if (transform.position == movePosition)
        {           
            canPush = false;
            isPushing = false;
        }
    }

    void Check()
    {
        // Vector3 characterDirection = cc.transform.forward;
        p1 = transform.position + (cc.transform.forward * 2.5f);//cc.transform.position + (cc.transform.forward * 4f);

        // Note: Maybe raycast within the obj to have a easier check to push?
        LayerMask mask = LayerMask.GetMask("BoxChecks");
        Collider [] cos = Physics.OverlapSphere(p1 + Vector3.up, 1.5f, mask);
        Debug.Log("Amount of hits: " + cos.Length);
        //for (int i = 0; i < cos.Length; i++)
        //    Debug.Log("Object hit: " + cos[i].transform.name);

        if(Physics.CheckSphere(p1,1.5f) && cos.Length > 0 && !canPush)
        {
            Debug.Log("Inside checksphere");
            for(int i = 0; i < cos.Length; i++)
            {
                Debug.Log(cos[i].transform.name);
            }

            //canPush = false;
                
           // Debug.Log("Hit something");
            //Debug.Log("I have spherecasted into: " + hit.transform.name);
           // Debug.Log("SphereCast Hit position: " + hit.transform.position);
        }
        else
        {
            Debug.Log("Time to move the block");
            movePosition =  transform.position + (cc.transform.forward * 2.5f);
            canPush = true;
            //MoveBox();
            
        }

        //Debug.Log("Hit: " + hit.distance);
    }

    void Check2()
    {
        p1 = transform.position;// + Vector3.right + Vector3.up + Vector3.back;
        Vector3 p2 = p1 + (cc.transform.forward * 2.5f);
        RaycastHit hit;
        float distance = Vector3.Distance(p1, p2);

        LayerMask mask = LayerMask.GetMask("BoxChecks");
        if (Physics.SphereCast(p1, 1.5f, cc.transform.forward * 2.5f,  out hit, distance, mask, QueryTriggerInteraction.UseGlobal)) {
            Debug.Log("Object spherecasted: " + hit.transform.name);          
            //Debug.Log("Distance between the cast and object: " + Vector3.Distance(p1, hit.transform.position));           
        }
        else
        {
            Vector3 positionToPush = transform.position + (cc.transform.forward * 2.5f);
            movePosition = positionToPush;
            canPush = true;
        }
    }

    void MoveBox()
    {
        // Based on player's controls: Maybe make a varible that stores a value based on the humanoid script.
        // Access the variable's value then based on the input, push the object in that direction. (For now just do the forward since it's the most common input)
        // Values to use : horizontal/vertical, p1 for direction (use it to gauge a initial distance then increase/decrease when necessary with a variable), distance pushed.

        if(input > 0)
        {
            
           // float distCovered = (Time.time - startTime);
           // Debug.Log("Distance Covered: " + distCovered);
            if (fracJourney < 1)
            {
                fracJourney += Time.deltaTime * 2f;
                Debug.Log("Travel time: " + fracJourney);

                transform.position = Vector3.MoveTowards(transform.position, movePosition, fracJourney);
            }

          
        }
                if (fracJourney >= 1)
                fracJourney = 0f;

    }

    private void OnDrawGizmos()
    {
        cc = player.GetComponent<CharacterController>();
        Gizmos.color = Color.yellow;
        Debug.DrawLine(p1, p1 + (cc.transform.forward * 2.5f));
        Gizmos.DrawWireSphere(p1 + (cc.transform.forward * 2.5f), 1.5f);// + (cc.transform.forward * 2.5f), 1.5f);
       //  Gizmos.DrawWireSphere(p1 + Vector3.up, 1.5f);
       // DebugExtension.DrawCapsule(p1 + Vector3.up, p1 + Vector3.up * 2);
       // DebugExtension.DebugWireSphere(p1 + Vector3.up);
    }


}
