﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSceneChange : MonoBehaviour
{
    void Update()
    {
        if (Input.GetButtonDown("Submit_B") || Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene(1);
        }
    }
}   