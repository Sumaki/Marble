using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static Transform currentRespawn;
    public Transform startRespawn;
    GameObject player;

    int lives;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("ParentPlayer");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GameInputs()
    {
        // If stuck
        if (Input.GetKeyDown(KeyCode.R))
        {

        }
    }
}
