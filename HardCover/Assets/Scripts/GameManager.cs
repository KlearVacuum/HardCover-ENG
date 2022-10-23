using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Update()
    {
        if (GlobalGameData.portalCooldown > 0)
        {
            GlobalGameData.portalCooldown -= Time.deltaTime;
        }
    }
}
