using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SettingManager : MonoBehaviour
{

    public GameObject[] screenObjects;

    public GameObject[] settingObject;

    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void OpenSettingBoard()
    {
        foreach (GameObject obj in screenObjects)
        {
            obj.SetActive(false);
        }

        foreach (GameObject obj in settingObject)
        {
            obj.SetActive(true);
        }
        settingObject[0].SetActive(false);
        if(Time.timeScale == 1f)
            Time.timeScale = 0f;

    }

    public void OnClickReset()
    {
        Replay();
        if (Time.timeScale == 0f)
            Time.timeScale = 1f;
    }

    public void OnClickMainMenu()
    {
        MainMenu();
        if (Time.timeScale == 0f)
            Time.timeScale = 1f;
    }

    public void OnClickReturn()
    {
        foreach (GameObject obj in screenObjects)
        {
            obj.SetActive(true);
        }

        foreach (GameObject obj in settingObject)
        {
            obj.SetActive(false);
        }
        if (Time.timeScale == 0f)
            Time.timeScale = 1f;
    }

    public void OnClickSound()
    {

    }
}
