using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class Character_Ball : MonoBehaviour
{
    #region Character Varibles
    Rigidbody rb;
    //public float xForce = 10.0f;
    //public float zForce = 10.0f;
    //public float yForce = 500.0f; // jump
    public Transform cameraBase;
    public float inputSpeed = 1f;
    public float movementForce = 50f;

    private Vector3 gravity = new Vector3(0f, -1000f, 0f);
    #endregion

    #region Movement Direction
    private float horizontalMovement;
    private float verticalMovement;
    private Vector3 force = Vector3.zero;
    private float distToGround;

    Vector3 finalDirection = Vector3.zero;
    #endregion

    #region Test Display Varibles
    public Text Horizontal;
    public Text Vertical;
    public Text Velocity;
    public Text Position;
    public Text FinalDirection;
    #endregion

    #region Public Testing Variables
    public float gravityAmount;
    #endregion

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        gravity.y = -gravityAmount;
        distToGround = GetComponent<Collider>().bounds.extents.y;
        
    }

    private void Update()
    {
        DebugVariables();
    }

    private void FixedUpdate()
    {       
        BallInputs();
        ApplyGravity();
    }


    /// <summary>
    /// User inputs -> adjusting force and direction within camera -> move
    /// </summary>
    void BallInputs()
    {


        horizontalMovement = Input.GetAxisRaw("Horizontal") * inputSpeed; // temp
        verticalMovement = Input.GetAxisRaw("Vertical") * inputSpeed;

        if (horizontalMovement != 0 || verticalMovement != 0)
        {
            float dt = Time.deltaTime;
            Vector3 pNew = new Vector3(transform.position.x + horizontalMovement, transform.position.y, transform.position.z + verticalMovement); //our new desired position, check
            Debug.Log("pNEW: " + pNew);
            Vector3 p = transform.position; //our current position
            Vector3 v = rb.velocity; //our current velocity
            force = rb.mass * (pNew - p - v * dt) / (dt);      
            
            //Debug.Log("Force: " + force);
            //return force;
            //rb.AddForce(force);


            Vector3 finalMovement = RotateInput();
            //Debug.Log("Final Movement Force: " + finalMovement);
            if (rb.velocity.magnitude <= 15f) // testing limit
                rb.AddForce(finalMovement);
        }

        //if (!IsGrounded())
        //    rb.AddForce(gravity * 2f);
        
    }

    Vector3 RotateInput()
    {
        Vector3 check = new Vector3(horizontalMovement, 0, verticalMovement);
        //Debug.Log("CHECK: " + check);
        finalDirection = cameraBase.transform.TransformDirection(check); // check
        //Debug.Log("FIRST FINAL DIRECTION: " + finalDirection);
        finalDirection.Set(finalDirection.x, 0, finalDirection.z);
        Vector3 move = finalDirection.normalized * movementForce; // * force.magnitude; TESTING VALUES INSTEAD
        //Debug.Log("Normalized finalDirection: " + finalDirection.normalized);

        //Debug.Log("finalDirection normalized: " + finalDirection.normalized);
        Debug.Log("Force magnitude: " + force.magnitude);      
        //Debug.Log(finalDirection.normalized * force.magnitude);
        return move;
    }

    void ApplyGravity()
    {
        rb.AddForce(gravity);
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }

    void DebugVariables()
    {
        Horizontal.text = "Horizontal: " + horizontalMovement.ToString();
        Vertical.text = "Vertical: " + verticalMovement.ToString();
        Velocity.text = "Velocity: " + rb.velocity.ToString();
        Position.text = "Position: " + transform.position.ToString();
        FinalDirection.text = "FINAL DIRECTION: " + finalDirection.ToString();
    }

}
