using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool _isGameOver = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("r") && _isGameOver)
        {
            SceneManager.LoadScene(1);
        }

        if (Input.GetKeyDown("escape"))
        {
            SceneManager.LoadScene(0);
            //Application.Quit();
        }
    }

    public void GameOver()
    {
        _isGameOver = true;
    }
}
