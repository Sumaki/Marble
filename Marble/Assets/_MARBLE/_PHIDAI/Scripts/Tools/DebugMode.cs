using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugMode : MonoBehaviour
{
    public GameObject[] respawn_points;
    public GameObject player;

    private void Update()
    {
        Inputs();
        Respawn(); 
    }

    void Inputs()
    {
        Reloader();
    }

    void Reloader()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void Respawn()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            player.transform.position = respawn_points[0].transform.position;
        if (Input.GetKeyDown(KeyCode.Alpha2))
            player.transform.position = respawn_points[1].transform.position;
        if (Input.GetKeyDown(KeyCode.Alpha3))
            player.transform.position = respawn_points[2].transform.position;
        if (Input.GetKeyDown(KeyCode.Alpha4))
            player.transform.position = respawn_points[3].transform.position;
    }
}
