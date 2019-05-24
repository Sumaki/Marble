using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationState : MonoBehaviour
{

    public Animator ani;

    public enum CharacterState { idle, idle_2, walk, run, jump, fall, push, morphBall, morphHumanoid, grab_element }
    public CharacterState state;

    public float walkSpeedAnim;

    public bool idle_2_startTimer = true;

    void Start()
    {
        state = CharacterState.idle;
    }

    void Update()
    {
       // Debug.Log("Character Animation State: " + state);
        TrackState();

        if (state == CharacterState.idle)
        {
            StartCoroutine(StartTimerIdle_2());
        }
             
    }

    void TrackState()
    {
        switch (state)
        {
            case CharacterState.idle:
                Idle();
                break;
            case CharacterState.idle_2:
                Idle2();
                break;
            case CharacterState.walk:
                Walk();
                break;
            case CharacterState.run:
                Run();
                break;
            case CharacterState.jump:
                Jump();
                break;
            case CharacterState.fall:
                Fall();
                break;
            case CharacterState.push:
                Push();
                break;
            case CharacterState.morphBall:
                MorphBall();
                break;
            case CharacterState.morphHumanoid:
                MorphHumanoid();
                break;
            case CharacterState.grab_element:
                GrabElement();
                break;
        }
    }

    void Idle()
    {
        ani.SetBool("Idle", true);
        ani.SetBool("Idle_2", false);
        ani.SetBool("Walk", false);
        //ani.SetBool("Run", false);
        ani.SetBool("Jump", false);
        ani.SetBool("Fall", false);
        ani.SetBool("Push", false);
        ani.SetBool("MorphBall", false);
        ani.SetBool("MorphHumanoid", false);
        ani.SetBool("Grab_Element", false);
    }

    void Idle2()
    {
        ani.SetBool("Idle", false);
        ani.SetBool("Idle_2", false);
        ani.SetBool("Walk", false);
        //ani.SetBool("Run", false);
        ani.SetBool("Jump", false);
        ani.SetBool("Fall", false);
        ani.SetBool("Push", false);
        ani.SetBool("MorphBall", false);
        ani.SetBool("MorphHumanoid", false);
        ani.SetBool("Grab_Element", false);
    }

    void Walk()
    {
        ani.SetBool("Idle", false);
        ani.SetBool("Idle_2", false);
        ani.SetBool("Walk", true);
        //ani.SetBool("Run", false);
        ani.SetBool("Jump", false);
        ani.SetBool("Fall", false);
        ani.SetBool("Push", false);
        ani.SetBool("MorphBall", false);
        ani.SetBool("MorphHumanoid", false);
        ani.SetBool("Grab_Element", false);
        ani.SetFloat("Speed", walkSpeedAnim,.1f,Time.deltaTime);
    }

    void Run()
    {
        ani.SetBool("Idle", false);
        ani.SetBool("Idle_2", false);
        ani.SetBool("Walk", false);
        //ani.SetBool("Run", true);
        ani.SetBool("Jump", false);
        ani.SetBool("Fall", false);
        ani.SetBool("Push", false);
        ani.SetBool("MorphBall", false);
        ani.SetBool("MorphHumanoid", false);
        ani.SetBool("Grab_Element", false);
    }

    void Jump()
    {
        ani.SetBool("Idle", false);
        ani.SetBool("Idle_2", false);
        ani.SetBool("Walk", false);
        //ani.SetBool("Run", false);
        ani.SetBool("Jump", true);
        ani.SetBool("Fall", false);
        ani.SetBool("Push", false);
        ani.SetBool("MorphBall", false);
        ani.SetBool("MorphHumanoid", false);
        ani.SetBool("Grab_Element", false);
    }

    void Fall()
    {
        ani.SetBool("Idle", false);
        ani.SetBool("Idle_2", false);
        ani.SetBool("Walk", false);
        //ani.SetBool("Run", false);
        ani.SetBool("Jump", false);
        ani.SetBool("Fall", true);
        ani.SetBool("Push", false);
        ani.SetBool("MorphBall", false);
        ani.SetBool("MorphHumanoid", false);
        ani.SetBool("Grab_Element", false);
    }

    void Push()
    {
        ani.SetBool("Idle", false);
        ani.SetBool("Idle_2", false);
        ani.SetBool("Walk", false);
        //ani.SetBool("Run", false);
        ani.SetBool("Jump", false);
        ani.SetBool("Fall", false);
        ani.SetBool("Push", true);
        ani.SetBool("MorphBall", false);
        ani.SetBool("MorphHumanoid", false);
        ani.SetBool("Grab_Element", false);
    }

    void MorphBall() // check which state of the ball/humanoid
    {
        ani.SetBool("Idle", false);
        ani.SetBool("Idle_2", false);
        ani.SetBool("Walk", false);
        //ani.SetBool("Run", false);
        ani.SetBool("Jump", false);
        ani.SetBool("Push", false);
        ani.SetBool("MorphBall", true);
        ani.SetBool("MorphHumanoid", false);
        ani.SetBool("Grab_Element", false);
    }

    void MorphHumanoid()
    {
        ani.SetBool("Idle", false);
        ani.SetBool("Idle_2", false);
        ani.SetBool("Walk", false);
        //ani.SetBool("Run", false);
        ani.SetBool("Jump", false);
        ani.SetBool("Fall", false);
        ani.SetBool("Push", false);
        ani.SetBool("MorphBall", false);
        ani.SetBool("MorphHumanoid", true);
        ani.SetBool("Grab_Element", false);
    }

    void GrabElement()
    {
        ani.SetBool("Idle", false);
        ani.SetBool("Idle_2", false);
        ani.SetBool("Walk", false);
        //ani.SetBool("Run", false);
        ani.SetBool("Jump", false);
        ani.SetBool("Fall", false);
        ani.SetBool("Push", false);
        ani.SetBool("MorphBall", false);
        ani.SetBool("MorphHumanoid", false);
        ani.SetBool("Grab_Element", true);
    }

    IEnumerator StartTimerIdle_2()
    {
        yield return new WaitForSeconds(3f);
        state = CharacterState.idle_2;
    }
}
