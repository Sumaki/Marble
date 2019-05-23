﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationState : MonoBehaviour
{

    public Animator ani;

    public enum CharacterState { idle, walk, run, jump, push, morphBall, morphHumanoid }
    public CharacterState state;

    public float walkSpeedAnim;
   
    void Start()
    {
        state = CharacterState.idle;
    }

    void Update()
    {
       // Debug.Log("Character Animation State: " + state);
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
            case CharacterState.push:
                Push();
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
        ani.SetBool("Jump", false);
        ani.SetBool("Push", false);
        ani.SetBool("MorphBall", false);
        ani.SetBool("MorphHumanoid", false);
    }

    void Walk()
    {
        ani.SetBool("Idle", false);
        ani.SetBool("Walk", true);
        //ani.SetBool("Run", false);
        ani.SetBool("Jump", false);
        ani.SetBool("Push", false);
        ani.SetBool("MorphBall", false);
        ani.SetBool("MorphHumanoid", false);
        ani.SetFloat("Speed", walkSpeedAnim,.1f,Time.deltaTime);
    }

    void Run()
    {
        ani.SetBool("Idle", false);
        ani.SetBool("Walk", false);
        //ani.SetBool("Run", true);
        ani.SetBool("Jump", false);
        ani.SetBool("Push", false);
        ani.SetBool("MorphBall", false);
        ani.SetBool("MorphHumanoid", false);
    }

    void Jump()
    {
        ani.SetBool("Idle", false);
        ani.SetBool("Walk", false);
        //ani.SetBool("Run", false);
        ani.SetBool("Jump", true);
        ani.SetBool("Push", false);
        ani.SetBool("MorphBall", false);
        ani.SetBool("MorphHumanoid", false);
    }

    void Fall()
    {
        ani.SetBool("Idle", false);
        ani.SetBool("Walk", false);
        //ani.SetBool("Run", false);
        ani.SetBool("Jump", false);
        ani.SetBool("Push", false);
        ani.SetBool("MorphBall", false);
        ani.SetBool("MorphHumanoid", false);
    }

    void Push()
    {
        ani.SetBool("Idle", false);
        ani.SetBool("Walk", false);
        //ani.SetBool("Run", false);
        ani.SetBool("Jump", false);
        ani.SetBool("Push", true);
        ani.SetBool("MorphBall", false);
        ani.SetBool("MorphHumanoid", false);
    }

    void MorphBall() // check which state of the ball/humanoid
    {
        ani.SetBool("Idle", false);
        ani.SetBool("Walk", false);
        //ani.SetBool("Run", false);
        ani.SetBool("Jump", false);
        ani.SetBool("Push", false);
        ani.SetBool("MorphBall", true);
        ani.SetBool("MorphHumanoid", false);
    }

    void MorphHumanoid()
    {
        ani.SetBool("Idle", false);
        ani.SetBool("Walk", false);
        //ani.SetBool("Run", false);
        ani.SetBool("Jump", false);
        ani.SetBool("Push", false);
        ani.SetBool("MorphBall", false);
        ani.SetBool("MorphHumanoid", true);
    }
}
