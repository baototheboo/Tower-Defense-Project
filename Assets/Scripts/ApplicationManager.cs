using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationManager : MonoBehaviour
{
    public void QuitGame()
    {
        AudioManager.instance.PlaySFX(4);
        Application.Quit();
        Debug.Log("Game is exiting");
    }
}
