using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private UIFade blackScreenOverlay;

    private void Awake()
    {
        blackScreenOverlay = FindObjectOfType<UIFade>();
    }
    private void Start()
    {
        blackScreenOverlay.FadeOut();
    }
    void Update()
    {
        if (GlobalGameData.portalCooldown > 0)
        {
            GlobalGameData.portalCooldown -= Time.deltaTime;
        }
    }
}
