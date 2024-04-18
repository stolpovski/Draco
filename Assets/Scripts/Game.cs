using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    Controls controls;
    private void Awake()
    {
        controls = new Controls();
        controls.Game.Enable();
        controls.Game.Restart.performed += context => SceneManager.LoadScene(0);
    }
}
