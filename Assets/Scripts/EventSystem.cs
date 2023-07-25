using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EventSystem : MonoBehaviour
{
    public Canvas PauseScreen;
    public Canvas SoundUIScreen;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        PauseScreen.gameObject.SetActive(!PauseScreen.gameObject.activeSelf);
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("LaunchScreen");
    }

    public void ReturnHome()
    {
        SceneManager.LoadScene("LaunchScreen");
    }

    public void ToggleSound()
    {
        SoundManager.instance.ToggleSound();
    }
}
