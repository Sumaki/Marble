using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushingBox : MonoBehaviour
{

    public GameObject player;
    public bool pushing = false;
    public float faceDetection = 1.5f;
    public float distanceToPush = 2.5f;
    CharacterController cc;
    CharacterAnimationState characterState;

    Vector3 p1;
    Vector3 movePosition;
    bool canPush = false;
    public bool isPushing = false;
    float fracJourney = 0f;
    float distToGround;
    public float input;
    float inputTest;

    public Vector3 initialPosition = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        cc = player.GetComponent<CharacterController>();
        characterState = player.GetComponent<CharacterAnimationState>();
        initialPosition = transform.position;
        distToGround = GetComponent<Collider>().bounds.extents.y;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Move position: " + movePosition);
        inputTest = Input.GetAxisRaw("Vertical");

        //Debug.Log("Final position of spherecast: " + p1);

        // BoxChecks();

        if (characterState.state == CharacterAnimationState.CharacterState.push && !canPush)
        {
            // disable player's left and right direction but not z axis
            // player.GetComponent<Character_Humanoid>().enableInputs = false;
            Check2();//Check();
        }

        if (canPush)
        {
            if (inputTest > 0 && !isPushing) // maybe adjust
            {
                isPushing = true;
            }
        }

        if (isPushing)
        {
            // Debug.Log(isGrounded()); recheck for grounded 
          
            transform.position = Vector3.MoveTowards(transform.position, movePosition, 5 * Time.deltaTime);
        }

        if (transform.position == movePosition)
        {
            Debug.Log("Done moving to position");
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
        Collider[] cos = Physics.OverlapSphere(p1 + Vector3.up, 1.5f, mask);
        Debug.Log("Amount of hits: " + cos.Length);
        //for (int i = 0; i < cos.Length; i++)
        //    Debug.Log("Object hit: " + cos[i].transform.name);

        if (Physics.CheckSphere(p1, 1.5f) && cos.Length > 0 && !canPush)
        {
            Debug.Log("Inside checksphere");
            for (int i = 0; i < cos.Length; i++)
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
            movePosition = transform.position + (cc.transform.forward * 2.5f);
            canPush = true;
            //MoveBox();

        }

        //Debug.Log("Hit: " + hit.distance);
    }

    void Check2()
    {
        p1 = transform.position;// + Vector3.right + Vector3.up + Vector3.back;
        Vector3 p2 = p1 + (cc.transform.forward * 1.5f);
        RaycastHit hit;
        float distance = Vector3.Distance(p1, p2);

        LayerMask mask = LayerMask.GetMask("BoxChecks");

        // dont use cc.forward change the sphere check base on the the side they are on, set a variable and replace the state of it then use it after to check 
        if (Physics.SphereCast(p1 + Vector3.up, 0.7f, cc.transform.forward * 1.5f, out hit, distance, mask, QueryTriggerInteraction.UseGlobal))
        {
            Debug.Log("Object spherecasted: " + hit.transform.name);
            //Debug.Log("Distance between the cast and object: " + Vector3.Distance(p1, hit.transform.position));           
        }
        else
        {
            BoxChecks();
            // Vector3 positionToPush = transform.position + (transform.forward * 2.5f); // try to not get the diagonal
            //movePosition = positionToPush;
            //canPush = true;
        }
    }


    void BoxChecks()
    {
        BoxCheckForward();
        BoxCheckBack();
        BoxCheckLeft();
        BoxCheckRight();
    }

    void BoxCheckForward()
    {
        RaycastHit hit;
        //if(Physics.Raycast(transform.position + Vector3.up,transform.TransformDirection(Vector3.forward),out hit, faceDetection))
        //{
        //    Debug.DrawRay(transform.position + Vector3.up, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
        //    if (hit.transform.tag == "Player") { 
        //        Debug.Log("Player is facing the front of the block");               
        //        movePosition = transform.position + (Vector3.back * distanceToPush);
        //        canPush = true;
        //        }
        //}
        //else
        //{
        //    Debug.DrawRay(transform.position + Vector3.up, transform.TransformDirection(Vector3.forward) * faceDetection, Color.red);
        //    Debug.DrawRay(transform.position + Vector3.up + Vector3.left, transform.TransformDirection(Vector3.forward) * faceDetection, Color.red);
        //    Debug.DrawRay(transform.position + Vector3.up + Vector3.right, transform.TransformDirection(Vector3.forward) * faceDetection, Color.red);


        //}

        if (Physics.SphereCast(transform.position + Vector3.up, 1f, transform.TransformDirection(Vector3.forward) * faceDetection, out hit, faceDetection))
        {
            //Debug.Log("Spherecast location: " + hit.point);

            if (hit.transform.tag == "PlayerHumanoid")
            {
                //Debug.Log("Spherecast touched player");
                movePosition = transform.position + (Vector3.back * distanceToPush);
                canPush = true;
            }
        }

    }

    void BoxCheckBack()
    {
        RaycastHit hit;
        //if (Physics.Raycast(transform.position + Vector3.up, transform.TransformDirection(Vector3.back), out hit, faceDetection))
        //{
        //    Debug.DrawRay(transform.position + Vector3.up, transform.TransformDirection(Vector3.back) * hit.distance, Color.yellow);
        //    if (hit.transform.tag == "Player")
        //    {
        //        Debug.Log("Player is facing the back of the block");
        //        movePosition = transform.position + (Vector3.forward * distanceToPush);
        //        canPush = true;
        //    }
        //}
        //else
        //{
        //    Debug.DrawRay(transform.position + Vector3.up, transform.TransformDirection(Vector3.back) * faceDetection, Color.black);
        //}
        if (Physics.SphereCast(transform.position + Vector3.up, 1f, transform.TransformDirection(Vector3.back) * faceDetection, out hit, faceDetection))
        {
            // Debug.Log("Spherecast location: " + hit.point);

            if (hit.transform.tag == "PlayerHumanoid")
            {
                //Debug.Log("Spherecast touched player");
                movePosition = transform.position + (Vector3.forward * distanceToPush);
                canPush = true;
            }
        }
    }

    void BoxCheckLeft()
    {
        RaycastHit hit;
        //if (Physics.Raycast(transform.position + Vector3.up, transform.TransformDirection(Vector3.left), out hit, faceDetection))
        //{
        //    Debug.DrawRay(transform.position + Vector3.up, transform.TransformDirection(Vector3.left) * hit.distance, Color.yellow);
        //    if (hit.transform.tag == "Player")
        //    {
        //        Debug.Log("Player is facing the left side of the block");
        //        movePosition = transform.position + (Vector3.right * distanceToPush);
        //        canPush = true;
        //    }
        //}
        //else
        //{
        //    Debug.DrawRay(transform.position + Vector3.up, transform.TransformDirection(Vector3.left) * faceDetection, Color.blue);
        //}
        if (Physics.SphereCast(transform.position + Vector3.up, 1f, transform.TransformDirection(Vector3.left) * faceDetection, out hit, faceDetection))
        {
            // Debug.Log("Spherecast location: " + hit.point);

            if (hit.transform.tag == "PlayerHumanoid")
            {
                //Debug.Log("Spherecast touched player");
                movePosition = transform.position + (Vector3.right * distanceToPush);
                canPush = true;
            }
        }
    }

    void BoxCheckRight()
    {
        RaycastHit hit;
        //if (Physics.Raycast(transform.position + Vector3.up, transform.TransformDirection(Vector3.right), out hit, faceDetection))
        //{
        //    Debug.DrawRay(transform.position + Vector3.up, transform.TransformDirection(Vector3.right) * hit.distance, Color.yellow);
        //    if (hit.transform.tag == "Player")
        //    {
        //        Debug.Log("Player is facing the right side of the block");
        //        movePosition = transform.position + (Vector3.left * distanceToPush);
        //        canPush = true;
        //    }
        //}
        //else
        //{
        //    Debug.DrawRay(transform.position + Vector3.up, transform.TransformDirection(Vector3.right) * faceDetection, Color.cyan);
        //}
        if (Physics.SphereCast(transform.position + Vector3.up, 1f, transform.TransformDirection(Vector3.right) * faceDetection, out hit, faceDetection))
        {
            // Debug.Log("Spherecast location: " + hit.point);

            if (hit.transform.tag == "PlayerHumanoid")
            {
                movePosition = transform.position + (Vector3.left * distanceToPush);
                canPush = true;
            }
        }
    }

    void MoveBox()
    {
        // Based on player's controls: Maybe make a varible that stores a value based on the humanoid script.
        // Access the variable's value then based on the input, push the object in that direction. (For now just do the forward since it's the most common input)
        // Values to use : horizontal/vertical, p1 for direction (use it to gauge a initial distance then increase/decrease when necessary with a variable), distance pushed.
        if (input > 0)
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
        {
            fracJourney = 0f;
        }

    }

    bool isGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 1f);
    }

    private void OnDrawGizmos()
    {
        cc = player.GetComponent<CharacterController>();
        Gizmos.color = Color.yellow;
        Debug.DrawLine(p1, p1 + (cc.transform.forward * 2.5f));
        Gizmos.DrawWireSphere(p1 + Vector3.up + (cc.transform.forward * 1.5f), 0.7f);// + (cc.transform.forward * 2.5f), 1.5f);
        Gizmos.DrawWireSphere(transform.position + Vector3.up + (transform.TransformDirection(Vector3.forward) * faceDetection), 1f);
        Gizmos.DrawWireSphere(transform.position + Vector3.up + (transform.TransformDirection(Vector3.back) * faceDetection), 1f);
        Gizmos.DrawWireSphere(transform.position + Vector3.up + (transform.TransformDirection(Vector3.left) * faceDetection), 1f);
        Gizmos.DrawWireSphere(transform.position + Vector3.up + (transform.TransformDirection(Vector3.right) * faceDetection), 1f);
        //  Gizmos.DrawWireSphere(p1 + Vector3.up, 1.5f);
        // DebugExtension.DrawCapsule(p1 + Vector3.up, p1 + Vector3.up * 2);
        // DebugExtension.DebugWireSphere(p1 + Vector3.up);
    }


}