using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Funnel : MonoBehaviour
{
    [Header("Player Ball Object")]
    public GameObject playerBall;
    [Header("How much speed do we need to keep at? (either the same or greater)")]
    public float speedLimitToActivate;
    [Header("How long until we activate the power")]
    public float timerToActivate;

    [Header("Ability Information")]
    public float speed;
    public float width;
    public float height;
    public float launchPower;
    [Header("How long until the ability ends?")]
    public float seconds;


    float timerCounter = 0f;
    float currentTimer = 0f;
    bool activePower = false;
    bool powerOn = false;
    bool launch = false;
    bool spin;

    private void Update()
    {
        if (powerOn)
        {
            ActivateFunnelAbility();
        }

        if (launch)
        {
            BurstUp();
        }

        if (spin)
        {
            StartCoroutine(SpinDuration());
            spin = false;
        }

        //Debug.Log("Spin variable: " + spin);
        //Debug.Log("Launch Variable: " + launch);

    }

    private void OnTriggerStay(Collider other)
    {
        // want it to apply to a ball only
        if (other.gameObject.tag == "PlayerBall")
        {
            // do something
            if(!powerOn)
                Debug.Log("Current Timer inside: " + currentTimer);
           // Debug.Log("State Active: " + powerOn);
            
            CheckConditions();

           
        }
    }

    void CheckConditions()
    {
        // counter to check how long the player has been in the trigger
        // track that the player is constantly moving a speed that we set a limit to

        if ( (Input.GetKey(KeyCode.Space) || Input.GetButton("Submit_X"))  && !activePower && playerBall.GetComponent<Rigidbody>().velocity.magnitude >= speedLimitToActivate)
        {
            currentTimer += 1 * Time.deltaTime;
        }

        if(  (Input.GetKeyUp(KeyCode.Space) || Input.GetButtonUp("Submit_X")) && !activePower)
        {
            currentTimer = 0;
        }

        if (currentTimer >= timerToActivate)
        {
            activePower = true;
        }

        if (activePower && !powerOn)
        {
            if (Input.GetKeyUp(KeyCode.Space) || Input.GetButtonUp("Submit_X"))
            {
                // do it
                spin = true;
                powerOn = true;
                
            }
        }
    }

    void ActivateFunnelAbility()
    {
        activePower = false;
        currentTimer = 0f;
            timerCounter += Time.deltaTime * speed;
            float x = Mathf.Cos(timerCounter) * width;
            float y = 10f;
            float z = Mathf.Sin(timerCounter) * height;

            // lerp the position or find a way to start where we are then go
            // do it for x amount of spins or timer
            // launch the player

        playerBall.transform.position = new Vector3(x, y, z);

        // do the ability, set a force to push the ball in a Vector.up motion in global space
        // maybe set the camera in a upwards view?
    }

    IEnumerator SpinDuration()
    {
        // spin = false;
        Debug.Log("In spin duration");
        yield return new WaitForSeconds(seconds);
        SpinFinish();
       // spin = true;
    
    }

    void SpinFinish()
    {

            Debug.Log("Finishing Spin");
            powerOn = false;
            timerCounter = 0;
            launch = true;
          
           
    }

    void BurstUp()
    {
        // launch the player upwards
        //playerBall.GetComponent<Rigidbody>().AddForce(Vector3.up * launchPower);
        playerBall.GetComponent<Rigidbody>().AddForceAtPosition(Vector3.up * launchPower * 10f, playerBall.transform.position);
        //playerBall.GetComponent<Rigidbody>().AddExplosionForce(launchPower, playerBall.transform.position, 1f);
        Debug.Log("Launching");
        launch = false;
    }
}