using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndController : MonoBehaviour
{
    public GameObject menu;
    private void Awake()
    {
        GlobalGameData.Reset();
        GlobalGameData.blackScreenOverlay = FindObjectOfType<UIFade>();
    }
    private void Start()
    {
        GlobalGameData.blackScreenOverlay.FadeOut(0.5f);
    }

    public void GoToMainMenu()
    {
        GlobalGameData.blackScreenOverlay.GoToScene("MainMenu");
    }

    public void GoToGame()
    {
        GlobalGameData.blackScreenOverlay.GoToScene("Main");
    }
    public void OpenMyMenu()
    {
        menu.SetActive(true);
    }
}