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
    float gravity = -3f;
    [Header("Humanoid Stats")]
    [Range(0,1)]
    public float movementSpeed;
    //[Range(0,10)]
    //public float gravityAmount = 9.8f;
    [Range(0,1)]
    public float jumpPower = 0.5f;
    
    Vector3 finalDirection = Vector3.zero;
    Vector3 movement = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        //gravity.y = -gravityAmount;
    }

    // Update is called once per frame
    void Update()
    {
        Inputs();
        DebugColorGrounded();
        //PlayerState();
        ApplyGravity();
        Debug.Log("Movement Direction: " + movement);
        Debug.Log("Grounded: " + cc.isGrounded);
    }

    void Inputs()
    {        
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");
       
        
            movement = RotateInput();
         if (Input.GetKeyDown(KeyCode.Space) && cc.isGrounded)
            movement.y = jumpPower;      
        //finalDirection = new Vector3(horizontalMovement, 0, verticalMovement);
        cc.Move(movement);       
    }

    void PlayerState()
    {

    }                                                                           

    Vector3 RotateInput()
    {
        if (cc.isGrounded)
        {
            Vector3 check = new Vector3(horizontalMovement, 0, verticalMovement);
            finalDirection = cameraBase.transform.TransformDirection(check);
            finalDirection.Set(finalDirection.x, -1 * Time.deltaTime, finalDirection.z);
            Vector3 move = finalDirection.normalized * movementSpeed;
            return move;
        }
        if (!cc.isGrounded)
        {
            Vector3 check = new Vector3(horizontalMovement, 0, verticalMovement);
            finalDirection = cameraBase.transform.TransformDirection(check);
            finalDirection.Set(finalDirection.x,gravity * Time.deltaTime, finalDirection.z);
            Vector3 move = finalDirection.normalized * movementSpeed;
            return move;
        }

        return Vector3.zero;
    }

    void ApplyGravity()
    {
        //Debug.Log("APPLYING GRAVITY"); 
        //if (cc.isGrounded)
        //    movement.y += -1f;
        if (!cc.isGrounded)
            movement.y += gravity * Time.deltaTime;

    }

    void DebugColorGrounded()
    {
        gameObject.GetComponent<Renderer>().material.color = GetComponent<CharacterController>().isGrounded ? Color.green : Color.red;
    }
}
