using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public Transform currentRespawn;
    public Transform startRespawn;
    public GameObject player;
    public GameObject playerHumanoid;

    int lives;

    // Start is called before the first frame update
    void Start()
    {      
        player.transform.position = currentRespawn.position;
        playerHumanoid.transform.position = currentRespawn.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        GameInputs();
    }

    void GameInputs()
    {
        // If stuck
        if (Input.GetKeyDown(KeyCode.R))
        {
            player.GetComponent<Rigidbody>().velocity = Vector3.zero;
            player.transform.position = currentRespawn.position;
            playerHumanoid.GetComponent<CharacterController>().enabled = false;
            playerHumanoid.transform.position = currentRespawn.position;
            playerHumanoid.GetComponent<CharacterController>().enabled = true;
        }
    }
}
