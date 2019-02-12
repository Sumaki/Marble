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

    private Vector3 gravity = new Vector3(0f, -1000f, 0f);
    #endregion

    #region Movement Direction
    private float horizontalMovement;
    private float verticalMovement;
    private Vector3 force = Vector3.zero;

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


        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        if (horizontalMovement != 0 || verticalMovement != 0)
        {
            float dt = Time.deltaTime;
            Vector3 pNew = new Vector3(transform.position.x + horizontalMovement, transform.position.y, transform.position.z + verticalMovement); //our new desired position, check
                                                                                                                                                  //Debug.Log(pNew);
            Vector3 p = transform.position; //our current position
            Vector3 v = rb.velocity; //our current velocity
            force = rb.mass * (pNew - p - v * dt) / (dt); // check          
            
             Debug.Log("Force: " + force);
            //return force;
            //rb.AddForce(force);


            Vector3 finalMovement = RotateInput();
            if (rb.velocity.magnitude <= 15f)
                rb.AddForce(finalMovement);
        }
        
    }

    Vector3 RotateInput()
    {
        Vector3 check = new Vector3(horizontalMovement, 0, verticalMovement);
        finalDirection = cameraBase.transform.TransformDirection(check); // check
        finalDirection.Set(finalDirection.x, 0, finalDirection.z);        
        Vector3 move = finalDirection.normalized * force.magnitude;
        Debug.Log("Normalized finalDirection: " + finalDirection.normalized);
        Debug.Log("Force magnitude: " + force.magnitude);      
        //Debug.Log(finalDirection.normalized * force.magnitude);
        return move;
    }

    void ApplyGravity()
    {
        rb.AddForce(gravity);
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
