using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class Character_Ball : MonoBehaviour
{
    #region Character Varibles
    
    //public float xForce = 10.0f;
    //public float zForce = 10.0f;
    //public float yForce = 500.0f; // jump
    [Header ("Character Settings")]
    public Transform cameraBase;
    public float inputSpeed;

    public float movementForce;
    [SerializeField]
    float airMovementForce;
    public float gravityAmount;
    bool enableScript = true;
    // Private Variables
    public static Vector3 gravity = new Vector3(0f, -1000f, 0f);
    Rigidbody rb;
    #endregion

    #region Movement Direction
    private float horizontalMovement;
    private float verticalMovement;
    private Vector3 force = Vector3.zero;
    private float distToGround;

    Vector3 finalDirection = Vector3.zero;
    #endregion

    [Header("Test Settings")]
    #region Test Display Varibles
    public bool TEST_VARIABLES;
    public Text Horizontal;
    public Text Vertical;
    public Text Velocity;
    public Text Position;
    public Text FinalDirection;
    #endregion



    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        gravity.y = -gravityAmount;
        distToGround = GetComponent<Collider>().bounds.extents.y;
       // airMovementForce =  1f;
    }

    private void Update()
    {
        DebugVariables();
        //Debug.Log("Velocity: " + rb.velocity.magnitude);    
        //Debug.Log("On Ground: " + IsGrounded());
       // Debug.Log("Movement Force: " + movementForce);
    }

    private void FixedUpdate()
    {
        if (enableScript)
        {
            BallInputs();
            ApplyGravity();          
        }
    }


    /// <summary>
    /// User inputs -> adjusting force and direction within camera -> move
    /// </summary>
    void BallInputs()
    {



        // limit velocity to set value for movement
        //if (IsGrounded())
        //{
        if (rb.velocity.magnitude > 80f)
        {
            Vector3 normalizedVelocity = Vector3.Normalize(rb.velocity);
            normalizedVelocity *= 80f;
            rb.velocity = normalizedVelocity;
        }

        //    }

        //if (!IsGrounded())
        //{
        //    if (rb.velocity.magnitude > 10f)
        //    {
        //        Vector3 normalizedVelocity = Vector3.Normalize(rb.velocity);
        //        normalizedVelocity *= 10f;
        //        rb.velocity = normalizedVelocity;
        //    }
        //}

        horizontalMovement = Input.GetAxisRaw("Horizontal") * inputSpeed; // temp
        verticalMovement = Input.GetAxisRaw("Vertical") * inputSpeed;

        if ((horizontalMovement != 0 || verticalMovement != 0) && IsGrounded()) // one for grounded and one for not grounded
        {
            //float dt = Time.deltaTime;
            //Vector3 pNew = new Vector3(transform.position.x + horizontalMovement, transform.position.y, transform.position.z + verticalMovement); //our new desired position, check
            //Debug.Log("pNEW: " + pNew);
            //Vector3 p = transform.position; //our current position
            //Vector3 v = rb.velocity; //our current velocity
            //force = rb.mass * (pNew - p - v * dt) / (dt);      
            
            //Debug.Log("Force: " + force);
            //return force;
            //rb.AddForce(force);


            Vector3 finalMovement = RotateInput();
           // Debug.Log("GROUNDED MOVEMENT: " + finalMovement);
            //Debug.Log("Final Movement Force: " + finalMovement);
           // if (rb.velocity.magnitude <= 15f)
           // { // testing limit
                rb.AddForce(finalMovement, ForceMode.Force);
           // Debug.Log("Velocity Y: " + rb.velocity.y);
            //rb.AddTorque(finalMovement);
            if (rb.velocity.y < -10)
                rb.velocity *= 1.15f;
            // }
        }

        if (!IsGrounded()) // limit velocity for air time
        {

            // temp turning in air
            //gameObject.transform.right =
            //Vector3.Slerp(gameObject.transform.right, rb.velocity.normalized, Time.deltaTime);

            Vector3 finalMovement = AirRoll();
           // rb.angularDrag = 0;
           Debug.Log("AIR MOVEMENT: " + finalMovement);
            //if (rb.velocity != Vector3.zero)
            //    transform.rotation = Quaternion.LookRotation(rb.velocity);
            ApplyTorque();
            rb.AddForce(finalMovement);
            //rb.AddRelativeTorque(finalMovement);
            //   rb.velocity = 30 * (rb.velocity.normalized);
            // rb.AddForce(gravity * 2f);
        }

    }

    // Spin when not grounded
    void ApplyTorque()
    {
        Vector3 rbVelocity = rb.velocity;
        Vector3 correctedAxes = new Vector3(rbVelocity.z, 0, -rbVelocity.x) * 1f;
        gameObject.transform.Rotate(correctedAxes, Space.World);
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
        //Debug.Log("Force magnitude: " + force.magnitude);      
        //Debug.Log(finalDirection.normalized * force.magnitude);
        return move;
    }

    Vector3 AirRoll()
    {
       
       Vector3 check = new Vector3(horizontalMovement, 0, verticalMovement);
       
        finalDirection = cameraBase.transform.TransformDirection(check);
        finalDirection.Set(finalDirection.x, 0, finalDirection.z);
         //Debug.Log("Air force: " + airMovementForce);
        Vector3 move = (finalDirection.normalized * movementForce) * 0.5f;
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
        if (TEST_VARIABLES)
        {
            Horizontal.text = "Horizontal: " + horizontalMovement.ToString();
            Vertical.text = "Vertical: " + verticalMovement.ToString();
            Velocity.text = "Velocity: " + rb.velocity.ToString();
            Position.text = "Position: " + transform.position.ToString();
            FinalDirection.text = "FINAL DIRECTION: " + finalDirection.ToString();
        }
    }



}
