using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxMovement : MonoBehaviour
{
    public List<ParallaxData> parallaxDataList;
    public float moveAmount;
    private Vector2 camStartPos;

    private void Awake()
    {
        for (int i = 0; i < parallaxDataList.Count; i++)
        {
            parallaxDataList[i].startPos = parallaxDataList[i].go.transform.position;
        }

        camStartPos = (Vector2)Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        camStartPos.y = 0;
    }

    private void Update()
    {
        // if player is at screen ctr, all sprites are at startPos
        // if player has negative x, everthing goes opposite direction
        // higher sorting order moves faster?

        Vector3 camPos = (Vector2)Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        camPos.y = 0;
        camPos.z = 0;

        foreach (ParallaxData data in parallaxDataList)
        {
            data.go.transform.position = data.startPos + (camPos - (Vector3)camStartPos) * moveAmount * data.movementLayer;
        }
    }
}

[System.Serializable]
public class ParallaxData
{
    public GameObject go;
    [HideInInspector] public Vector3 startPos;
    [Tooltip("positive follows target, negative moves away from target")]
    public float movementLayer;
}
