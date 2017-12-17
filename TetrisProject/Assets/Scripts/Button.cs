using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour {
    public Transform canvas;

    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void ExitGame() {
        Application.Quit();
    }

    public void PauseGame() {
        if (canvas.gameObject.activeInHierarchy == false)
        {
            canvas.gameObject.SetActive(true);
            Time.timeScale = 0;
            Tetramino.paused = true;
        }
        else {
            canvas.gameObject.SetActive(false);
            Time.timeScale = 1;
            Tetramino.paused = false;
        }
    }
    
}
