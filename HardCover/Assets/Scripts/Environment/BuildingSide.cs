using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSide : MonoBehaviour
{
    [SerializeField] float minScale = 0;
    float maxScale;
    public float maxDistance;
    public bool rightSide;
    Vector2 anchorPos;

    private void Awake()
    {
        anchorPos = transform.position;
        maxScale = Mathf.Abs(transform.localScale.x);
    }

    private void Update()
    {
        Vector2 diff = anchorPos - (Vector2)Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2, Screen.height/2,0));
        diff.y = 0;
        float distance = diff.magnitude;

        if (rightSide)
        {
            if (diff.x > 0)
            {
                transform.localScale = new Vector3(minScale, transform.localScale.y, transform.localScale.z);
                return;
            }
            float scaleAmount = Mathf.Lerp(minScale, maxScale, distance / maxDistance);
            transform.localScale = new Vector3(scaleAmount, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            if (diff.x < 0)
            {
                transform.localScale = new Vector3(-minScale, transform.localScale.y, transform.localScale.z);
                return;
            }
            float scaleAmount = Mathf.Lerp(minScale, maxScale, distance / maxDistance);
            transform.localScale = new Vector3(-scaleAmount, transform.localScale.y, transform.localScale.z);

        }
    }
}
