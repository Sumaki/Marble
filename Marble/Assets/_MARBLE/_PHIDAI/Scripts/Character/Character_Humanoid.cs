using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Character_Humanoid : MonoBehaviour
{

    public Transform cameraBase;


    CharacterController cc;
    float horizontalMovement;
    float verticalMovement;
    float globalGravity = -1f;
    float gravity = -15f;
    bool enableScript = true;   
    [Header("Humanoid Stats")]
    [Range(1, 10)]
    public float movementSpeed;
    //[Range(0,10)]
    //public float gravityAmount = 9.8f;
    [Range(1,10)]
    public float jumpPower;

    Vector3 finalDirection = Vector3.zero;
    Vector3 movement = Vector3.zero;
    Vector3 check = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        //gravity.y = -gravityAmount;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (enableScript)
        {
            Inputs();
           // DebugColorGrounded();
            //PlayerState();
            ApplyGravity();
            Debug.Log("Movement Y: " + movement.y);
            //Debug.Log("Grounded: " + cc.isGrounded);
            Debug.Log("Global Gravity: " + globalGravity);
        }
    }

    void Inputs()
    {
        
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        if (cc.isGrounded)
            movement = TestMovement();
        //if (!cc.isGrounded)           // find midair movement that works with the current movement
        //    movement = AirMovement();

        if (Input.GetKeyDown(KeyCode.Space) && cc.isGrounded)
        {
           movement.y = jumpPower;          
        }

        cc.Move(movement * Time.deltaTime);
    }

    void PlayerState()
    {

    }

    Vector3 RotateInput()
    {
        check = new Vector3(horizontalMovement, 0, verticalMovement);
        finalDirection = cameraBase.transform.TransformDirection(check);       
        finalDirection.Set(finalDirection.x, 0, finalDirection.z);       
        Vector3 move = finalDirection.normalized * movementSpeed;
        return move;
  
    }

    Vector3 TestMovement()
    {
        check = new Vector3(horizontalMovement, 0, verticalMovement);
        finalDirection = cameraBase.transform.TransformDirection(check);
        finalDirection.Set(finalDirection.x, -1, finalDirection.z);
        finalDirection *= movementSpeed;
        return finalDirection;

    }

    Vector3 AirMovement()
    {
        check = new Vector3(horizontalMovement, 0, verticalMovement);
        finalDirection = cameraBase.transform.TransformDirection(check);
        finalDirection.Set(finalDirection.x, AirGravity(), finalDirection.z);
        finalDirection *= movementSpeed;
        return finalDirection;
    }


    void ApplyGravity()
    {              
           Debug.Log("APPLYING GRAVITY");
           movement.y += gravity * Time.deltaTime;       
    }

    float AirGravity()
    {
        if (!cc.isGrounded)
        {
            return movement.y = gravity * Time.deltaTime;
        }
        return 0f;
            
    }

    //void DebugColorGrounded()
    //{
    //    gameObject.GetComponent<Renderer>().material.color = GetComponent<CharacterController>().isGrounded ? Color.green : Color.red;
    //}

    //private void OnControllerColliderHit(ControllerColliderHit hit)
    //{
    //    if(hit.gameObject.tag == "Player")
    //    {
    //        Physics.IgnoreCollision(cc, hit.gameObject.GetComponent<Collider>());
    //    }
    //}
}
