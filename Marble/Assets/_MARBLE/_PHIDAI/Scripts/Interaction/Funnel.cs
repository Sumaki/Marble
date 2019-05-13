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




    float currentTimer = 0f;
    bool activePower = false;
    Vector3 forceToApply = Vector3.zero;

    private void OnTriggerStay(Collider other)
    {
        // want it to apply to a ball only
        if (other.gameObject.tag == "PlayerBall")
        {
            // do something
            CheckConditions();
        }
    }

    void CheckConditions()
    {
        // counter to check how long the player has been in the trigger
        // track that the player is constantly moving a speed that's we set a limit to

        if (currentTimer >= timerToActivate)
        {
            activePower = true;
        }
    }

    void ActivateFunnelAbility()
    {
        // do the ability, set a force to push the ball in a Vector.up motion in global space
        // maybe set the camera in a upwards view?
    }
}