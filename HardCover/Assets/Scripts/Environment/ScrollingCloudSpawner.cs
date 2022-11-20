using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingCloudSpawner : MonoBehaviour
{
    public List<Sprite> cloudType;
    public GameObject cloudPrefab;
    public int spawnQty;
    public float minX;
    public float maxX;

    private void Start()
    {
        if (spawnQty <= 0) return;
        float step = (maxX - minX) / spawnQty;

        for (int i = 0; i < spawnQty; i++)
        {
            GameObject newCloud = Instantiate(cloudPrefab, new Vector3(minX + step * (i + 1), transform.position.y, transform.position.z + (0.01f * i)), Quaternion.Euler(new Vector3(45,0,0)));
            newCloud.GetComponent<SpriteRenderer>().sprite = cloudType[Random.Range(0, cloudType.Count)];
            ScrollingCloud scrollingCloud = newCloud.GetComponent<ScrollingCloud>();
            scrollingCloud.startLoopPoint = minX;
            scrollingCloud.endLoopPoint = maxX;
        }
    }
}