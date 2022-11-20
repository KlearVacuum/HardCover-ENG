using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingCloud : MonoBehaviour
{
    public float scrollSpeed;
    [HideInInspector] public float endLoopPoint;
    [HideInInspector] public float startLoopPoint;

    void Update()
    {
        transform.position += new Vector3(scrollSpeed * Time.deltaTime, 0, 0);
        if (transform.position.x > endLoopPoint) transform.position = new Vector3(startLoopPoint, transform.position.y, transform.position.z);
    }
}
