using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Character_Humanoid : MonoBehaviour
{
    
    public Transform cameraBase;
   // public Animator ani;
    
    CharacterController cc;
    CharacterAnimationState characterState;
    float horizontalMovement;
    float verticalMovement;
    float globalGravity = -1f;
    float gravity = -15f;
    bool enableScript = true;   
    [Header("Humanoid Stats")]
    [Range(1, 100)]
    public float movementSpeed;
    //[Range(0,10)]
    //public float gravityAmount = 9.8f;
    [Range(1,100)]
    public float jumpPower;
    [Range(1, 100)]
    public float pushingPower;
    Vector3 finalDirection = Vector3.zero;
    Vector3 movement;
    Vector3 check = Vector3.zero;
    bool jump;
    bool grab = false;
    Transform thingToPull = null;
    float airGravity = -1f;

    // Start is called before the first frame update
    void Start()
    {
         cc = GetComponent<CharacterController>();
        characterState = GetComponent<CharacterAnimationState>();
        //gravity.y = -gravityAmount;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (enableScript)
        {
            Inputs();
          //  DebugColorGrounded();
            //PlayerState();
     
            ApplyGravity();
            // Debug.Log("Movement Y: " + movement.y);
            //Debug.Log("Grounded: " + cc.isGrounded);
            // Debug.Log("Global Gravity: " + globalGravity);
           // Debug.Log("Movement XYZ: " + movement);
        }
    }

    private void Update()
    {

        Debug.Log("Grab state: " + grab);

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Submit_A")) && cc.isGrounded && !jump)
        {
            jump = true;           
        }

        if (cc.isGrounded && (horizontalMovement != 0 || verticalMovement != 0)) { characterState.state = CharacterAnimationState.CharacterState.walk; }
        if (cc.isGrounded && horizontalMovement == 0 && verticalMovement == 0) { characterState.state = CharacterAnimationState.CharacterState.idle; }

        //RaycastCheck();

        if (Input.GetKey(KeyCode.LeftShift) && grab)
        {
            //Push(thingToPull);
            //movement = Vector3.zero;
        }


    }


    void Inputs()
    {
        
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        if (cc.isGrounded && (horizontalMovement !=0 || verticalMovement !=0))
        {
           
            movement = TestMovement();
            

        }

        if(cc.isGrounded && horizontalMovement == 0 && verticalMovement == 0)
        {           
            movement = Vector3.zero;
        }

      
        if (!cc.isGrounded)
        {       // find midair movement that works with the current movement and camera
                //movement.y += gravity * Time.deltaTime;
                //Vector3 camForward = Vector3.Scale(cameraBase.forward, new Vector3(1, 0, 1)).normalized;

           
            //movement.x = horizontalMovement * movementSpeed;
            //movement.z = verticalMovement * movementSpeed;
          //  movement = cameraBase.transform.TransformDirection(movement);
            //movement = (verticalMovement * camForward + horizontalMovement * cameraBase.right).normalized;
           // movement = AirMovement();
            //movement.y += gravity * Time.deltaTime;
            
            //Debug.Log("Movement in Air: " + movement);
            //movement *= movementSpeed;
        }

        if (jump)
        {

            movement.y = jumpPower;
            jump = false;
        }

        cc.Move(movement * Time.deltaTime);
    }

    Vector3 TestMovement()
    {
        check = new Vector3(horizontalMovement, 0, verticalMovement);
        finalDirection = cameraBase.transform.TransformDirection(check);
        finalDirection.Set(finalDirection.x, -1, finalDirection.z);
        if (horizontalMovement != 0 || verticalMovement != 0)
            transform.forward = Vector3.Normalize(new Vector3(finalDirection.x, 0, finalDirection.z));
        finalDirection *= movementSpeed;
        return finalDirection;

    }

    Vector3 AirMovement() // figure it out
    {
        check = new Vector3(horizontalMovement, 0, verticalMovement);
        finalDirection = cameraBase.transform.TransformDirection(check);
        finalDirection.Set(finalDirection.x, AirGravity(), finalDirection.z);
        if (horizontalMovement != 0 || verticalMovement != 0)
            transform.forward = Vector3.Normalize(new Vector3(finalDirection.x, 0, finalDirection.z));
        finalDirection *= movementSpeed;
        return finalDirection;
    }


    void ApplyGravity()
    {
        
        //Debug.Log("APPLYING GRAVITY");
        movement.y += gravity * Time.deltaTime;
    }

    float AirGravity()
    {               
        movement.y -= 0.005f * Time.deltaTime;
        return movement.y;

    }

    void DebugColorGrounded()
    {
        gameObject.GetComponent<Renderer>().material.color = GetComponent<CharacterController>().isGrounded ? Color.green : Color.red;
    }

    void RaycastCheck()
    {

        RaycastHit hit;
        if(Physics.Raycast(transform.position,transform.TransformDirection(Vector3.forward),out hit, 1f))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Hit something: " + hit.transform.name);
            if (hit.transform.tag == "Pushable")
            {
                grab = true;
                thingToPull = hit.transform;                         
            }
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            Debug.Log("Hit nothing");
            grab = false;
            thingToPull = null;
        }
    }

    void Push(Transform obj)
    {

        //Debug.Log("Push method obj: " + obj);

        if(obj != null)
        {
            //Debug.Log("Inside the push method");
            Vector3 d = transform.position - obj.position;
            float dist = d.magnitude;
            Vector3 pullDir = d.normalized;
            Debug.Log("Distance between player and obj: " + dist);
            if (dist > 50) obj = null;
            else if (dist > 0.1f)
            {
                float pullF = 10;
                float pullForDist = (dist - 0.1f) / 2.0f;
                if (pullForDist > 20) pullForDist = 20;
                pullF += pullForDist;
                Debug.Log(obj.GetComponent<Rigidbody>().velocity);
                obj.GetComponent<Rigidbody>().velocity += pullDir * (pullF * Time.deltaTime);
            }
        }
    }


    private void OnControllerColliderHit(ControllerColliderHit hit)
    {

        if (hit.gameObject.tag == "Player")
        {
            Physics.IgnoreCollision(cc, hit.gameObject.GetComponent<Collider>());
        }

        Vector3 force;
        if (body != null && !body.isKinematic)
        {
            if (hit.moveDirection.y < -0.3)
            {
                force = new Vector3(0, -0.5f, 0) * gravity * 6.0f;
            }
            else
            {
                force = hit.controller.velocity * pushingPower;
            }
            Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
            body.velocity = pushDir * pushingPower;
            // body.AddForceAtPosition(force, hit.point);
        }
    }
}
