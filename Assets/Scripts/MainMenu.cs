using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public string startScene;

    [SerializeField]
    public GameObject notify;

    [SerializeField]
    public GameObject startButton;

    [SerializeField]
    public GameObject quitButton;

    [SerializeField]
    public GameObject settingButton;

    public void StartGame()
    {
        AudioManager.instance.PlaySFX(4);
        AudioManager.instance.BossMusic();
        SceneManager.LoadScene(startScene);
    }

    public void OnClickQuitButton()
    {
        AudioManager.instance.PlaySFX(4);
        startButton.SetActive(false);   
        quitButton.SetActive(false);
        settingButton.SetActive(false);
        notify.SetActive(true);        
    }

    public void QuitGame()
    {
        AudioManager.instance.PlaySFX(4);
        Application.Quit();
        Debug.Log("Game is exiting");
    }

    public void NotQuitGame()
    {
        AudioManager.instance.PlaySFX(4);
        startButton.SetActive(true);
        quitButton.SetActive(true);
        settingButton.SetActive(true);
        notify.SetActive(false);
    }

    
}
