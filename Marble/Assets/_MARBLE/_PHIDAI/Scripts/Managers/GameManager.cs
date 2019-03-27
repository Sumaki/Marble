using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public Transform currentRespawn;
    public Transform startRespawn;
    GameObject player;

    int lives;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        GameInputs();
    }

    void GameInputs()
    {
        // If stuck
        if (Input.GetKeyDown(KeyCode.R))
        {
            player.transform.position = currentRespawn.position;
        }
    }
}
