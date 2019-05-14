using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Character_Humanoid : MonoBehaviour
{
    
    public Transform cameraBase;

    #region Character Movement Variables
    CharacterController cc;
    CharacterAnimationState characterState;
    public float horizontalMovement;
    public float verticalMovement;
    float globalGravity = -1f;
    float gravity = -15f;
    public bool enableInputs = true;   
    [Header("Humanoid Stats")]
    [Range(1, 100)]
    public float movementSpeed;
    #endregion
    [Range(1,100)]
    public float jumpPower;
    [Range(1, 100)]
    public float pushingPower;
    Vector3 finalDirection = Vector3.zero;
    Vector3 movement;
    Vector3 check = Vector3.zero;

    #region Character Check Values
    bool jump;
    bool canGrab = false;
    bool isGrabbing = false;
    bool touchedDeath = false;
    Transform thingToPull = null;

    GameObject playerParent;
    #endregion


    float airGravity = -1f;

    // Start is called before the first frame update
    void Start()
    {
         cc = GetComponent<CharacterController>();
        characterState = GetComponent<CharacterAnimationState>();
        //gravity.y = -gravityAmount;
        playerParent = GameObject.FindGameObjectWithTag("PlayerParent");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (enableInputs)
        {
            Inputs();
          //  DebugColorGrounded();
            //PlayerState();
     
            ApplyGravity();
            // Debug.Log("Movement Y: " + movement.y);
            //Debug.Log("Grounded: " + cc.isGrounded);
            // Debug.Log("Global Gravity: " + globalGravity);
            // Debug.Log("Movement XYZ: " + movement);
            //Debug.Log(horizontalMovement);
        }
    }

    private void Update()
    {

      //  Debug.Log("Grab state: " + grab);

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Submit_A")) && cc.isGrounded && !jump)
        {
            
            jump = true;
            characterState.state = CharacterAnimationState.CharacterState.jump;

        }

        //if(Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Submit_A"))
        //    characterState.state = CharacterAnimationState.CharacterState.jump; // fix later



        if (cc.isGrounded && !jump  && (horizontalMovement != 0 || verticalMovement != 0)) {
            characterState.state = CharacterAnimationState.CharacterState.walk;
            characterState.walkSpeedAnim = Mathf.Abs((new Vector3(horizontalMovement,0,verticalMovement)).magnitude);
        }
        if (cc.isGrounded && !jump  && horizontalMovement == 0 && verticalMovement == 0 && !Input.GetKey(KeyCode.LeftShift)) { characterState.state = CharacterAnimationState.CharacterState.idle; }

        RaycastCheck();

        if ((Input.GetKey(KeyCode.LeftShift) || Input.GetAxis("LeftTrigger") > 0) && canGrab && cc.isGrounded)
        {
            movement = Vector3.zero;
            enableInputs = false;         
            Push(thingToPull);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) && !enableInputs)
        {
            enableInputs = true;
            gameObject.transform.parent = playerParent.transform;

        }



    }


    void Inputs()
    {
        if (enableInputs)
        {            
            verticalMovement = Input.GetAxis("Vertical");
        }
            horizontalMovement = Input.GetAxis("Horizontal");
        if (cc.isGrounded && (horizontalMovement !=0 || verticalMovement !=0)  )
        {
           
            movement = TestMovement();
            

        }

        if(cc.isGrounded && horizontalMovement == 0 && verticalMovement == 0 )
        {    
               
                movement = Vector3.zero;
        }

      
        if (!cc.isGrounded)
        {       // find midair movement that works with the current movement and camera
                //movement.y += gravity * Time.deltaTime;
            //   Vector3 camForward = Vector3.Scale(cameraBase.forward, new Vector3(1, 0, 1)).normalized;

            
          
            movement = AirMovement();
            //movement = cameraBase.transform.TransformDirection(movement);
           // movement = (verticalMovement * camForward + horizontalMovement * cameraBase.right).normalized;
        //  movement = AirMovement();
            //movement.y += gravity * Time.deltaTime;
            
           // Debug.Log("Movement in Air: " + movement);
          // movement *= movementSpeed;
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

    Vector3 AirMovement()
    {
        movement.x = (horizontalMovement * movementSpeed); // insert variable to consider slowing the mid-air movement
        movement.z = (verticalMovement * movementSpeed);
        finalDirection = cameraBase.transform.TransformDirection(new Vector3(movement.x, 0, movement.z));
        finalDirection.y = movement.y;
        if (horizontalMovement != 0 || verticalMovement != 0)
            transform.forward = Vector3.Normalize(new Vector3(finalDirection.x, 0, finalDirection.z));
        return finalDirection;
    }


    void ApplyGravity()
    {
        
        //Debug.Log("APPLYING GRAVITY");
        movement.y += gravity * Time.deltaTime;
    }

    float AirGravity()
    {
        movement.y -= 0.005f;
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
          //  Debug.Log("Hit something: " + hit.transform.name);
            if (hit.transform.tag == "Pushable")
            {
                canGrab = true;
                thingToPull = hit.transform;                         
            }
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
          //  Debug.Log("Hit nothing");
            canGrab = false;
            thingToPull = null;            
        }
    }

    void Push(Transform obj)
    {

        // Note: Keep the player stuck, use the controller stick variables (horizontal/vertical) for movement of the block
        //       Do a check of the block direction based on the direction they inputed. After do the lerp to the new position.


        //Debug.Log("Push method obj: " + obj);

        if(obj != null)
        { 
            obj.GetComponent<PushingBox>().input = verticalMovement;
            gameObject.transform.parent = obj.transform;
            characterState.state = CharacterAnimationState.CharacterState.push;
        }
    }


    private void OnControllerColliderHit(ControllerColliderHit hit)
    {

        if (hit.gameObject.tag == "Player")
        {
            Physics.IgnoreCollision(cc, hit.gameObject.GetComponent<Collider>());
        }

        if(hit.gameObject.tag == "DeathRespawn" && !touchedDeath)
        {
            hit.gameObject.GetComponent<RespawnOnDeath>().dead = true;
           
            StartCoroutine(DetectionResetTimer());
        }

        if (hit.gameObject.name == "Box_Falling")
        {
            Debug.Log("Hit the box");
            hit.gameObject.GetComponent<BoxFalling>().humanoidBoxFall = true;
        }

        if(hit.gameObject.tag == "SpherePushable")
        {
            Rigidbody body = hit.collider.attachedRigidbody;
           
            body.AddForce(cc.transform.forward * 30f);
        }

        //Rigidbody body = hit.collider.attachedRigidbody;
        //Vector3 force;
        //if (body != null && !body.isKinematic)
        //{

        //    characterState.state = CharacterAnimationState.CharacterState.push;
        //    if (hit.moveDirection.y < -0.3)
        //    {
        //        force = new Vector3(0, -0.5f, 0) * gravity * 6.0f;
        //    }
        //    else
        //    {
        //        force = hit.controller.velocity * p   ushingPower;
        //    }

        //    Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        //    body.velocity = pushDir * pushingPower;
        //}
    }

    IEnumerator DetectionResetTimer()
    {
        touchedDeath = true;
        yield return new WaitForSeconds(0.5f);
        touchedDeath = false;
    }




}
