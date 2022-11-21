using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool mAfterStart = true;

    private void Awake()
    {
        GlobalGameData.blackScreenOverlay = FindObjectOfType<UIFade>();
    }

    private void Update()
    {
        if (mAfterStart)
        {
            mAfterStart = false;
            GlobalGameData.dialogManager.StartChat("Amanda", "Intro");
        }

        if (GlobalGameData.portalCooldown > 0)
        {
            GlobalGameData.portalCooldown -= Time.deltaTime;
        }
    }

    public void CallFadeOut(bool b, int c)
    {
        GlobalGameData.blackScreenOverlay.FadeOut(0.5f,
            () => { GlobalGameData.dialogManager.StartChat("Amanda", "Intro2"); });
    }
}