using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        GlobalGameData.blackScreenOverlay = FindObjectOfType<UIFade>();
    }
    private void Start()
    {
        GlobalGameData.blackScreenOverlay.FadeOut(0.5f);
    }
    void Update()
    {
        if (GlobalGameData.portalCooldown > 0)
        {
            GlobalGameData.portalCooldown -= Time.deltaTime;
        }
    }
}
