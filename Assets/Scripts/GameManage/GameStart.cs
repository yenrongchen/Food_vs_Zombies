using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    [SerializeField]
    private GameObject helpScreen;

    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void GameHelp()
    {
        helpScreen.SetActive(true);
    }

    public void closeHelp()
    {
        helpScreen.SetActive(false);
    }
}
