using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationState : MonoBehaviour
{

    public Animator ani;

    public enum CharacterState { idle, walk, run, jump, morphBall, morphHumanoid }
    public CharacterState state;
   
    void Start()
    {
        state = CharacterState.idle;
    }

    void Update()
    {
        TrackState();   
    }

    void TrackState()
    {
        switch (state)
        {
            case CharacterState.idle:
                Idle();
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
            case CharacterState.morphBall:
                MorphBall();
                break;
            case CharacterState.morphHumanoid:
                MorphHumanoid();
                break;
        }
    }

    void Idle()
    {
        ani.SetBool("Idle", true);
        ani.SetBool("Walk", false);
        //ani.SetBool("Run", false);
        //ani.SetBool("Jump", false);
        ani.SetBool("MorphBall", false);
        ani.SetBool("MorphHumanoid", false);
    }

    void Walk()
    {
        ani.SetBool("Idle", false);
        ani.SetBool("Walk", true);
        //ani.SetBool("Run", false);
        //ani.SetBool("Jump", false);
        ani.SetBool("MorphBall", false);
        ani.SetBool("MorphHumanoid", false);
    }

    void Run()
    {
        ani.SetBool("Idle", false);
        ani.SetBool("Walk", false);
        //ani.SetBool("Run", true);
        //ani.SetBool("Jump", false);
        ani.SetBool("MorphBall", false);
        ani.SetBool("MorphHumanoid", false);
    }

    void Jump()
    {
        ani.SetBool("Idle", false);
        ani.SetBool("Walk", false);
        //ani.SetBool("Run", false);
        //ani.SetBool("Jump", true);
        ani.SetBool("MorphBall", false);
        ani.SetBool("MorphHumanoid", false);
    }

    void MorphBall() // check which state of the ball/humanoid
    {
        ani.SetBool("Idle", false);
        ani.SetBool("Walk", false);
        //ani.SetBool("Run", false);
        //ani.SetBool("Jump", false);     
        ani.SetBool("MorphBall", true);
        ani.SetBool("MorphHumanoid", false);
    }

    void MorphHumanoid()
    {
        ani.SetBool("Idle", false);
        ani.SetBool("Walk", false);
        //ani.SetBool("Run", false);
        //ani.SetBool("Jump", false);
        ani.SetBool("MorphBall", false);
        ani.SetBool("MorphHumanoid", true);
    }
}
