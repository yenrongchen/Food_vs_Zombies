using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    [SerializeField] GameObject helpScreen;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && helpScreen.activeSelf)
        {
            closeHelp();
        }
    }
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
