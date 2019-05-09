using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoBehaviour
{
   public enum LevelStates { Start, Temple, Boss} // will manage more in the future
   public LevelStates Which_Level;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PlayerBall" || other.gameObject.tag == "PlayerHumanoid")
        {
            ChangeScene();
        }
    }

    void ChangeScene()
    {
        switch (Which_Level)
        {
            case LevelStates.Start:
                break;
            case LevelStates.Temple:
                SceneManager.LoadScene(0); // stick with index for now, change in the future
                break;
            case LevelStates.Boss:
                break;
        }
    }
}
