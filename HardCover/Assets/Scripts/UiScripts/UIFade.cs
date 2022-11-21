using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIFade : MonoBehaviour
{
    public string nextLevel;
    bool fadeIn;
    Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
        image.color = new Color(image.color.r, image.color.g, image.color.b, 1);
    }

    public void FadeIn(float delay = 0, bool changeScene = false)
    {
        StartCoroutine(WaitThenFadeInOverSeconds(delay, 1.5f, changeScene));
    }

    public void FadeOut(float delay = 0, UnityAction action = null)
    {
        StartCoroutine(WaitThenFadeOutOverSeconds(delay, 1.5f, action));
    }

    public void GoToScene(string sceneName)
    {
        nextLevel = sceneName;
        FadeIn(0.5f, true);
    }

    IEnumerator WaitThenFadeInOverSeconds(float waitTime, float fadeSpeed, bool changeScene = false)
    {
        yield return new WaitForSeconds(waitTime);

        while (image.color.a < 1)
        {
            float newAlpha = image.color.a + Time.deltaTime * fadeSpeed;
            image.color = new Color(image.color.r, image.color.g, image.color.b, newAlpha);
            if (newAlpha >= 1) break;
            yield return null;
        }

        if (changeScene) SceneManager.LoadScene(nextLevel);
    }

    IEnumerator WaitThenFadeOutOverSeconds(float waitTime, float fadeSpeed, UnityAction action)
    {
        yield return new WaitForSeconds(waitTime);

        while (image.color.a > 0)
        {
            float newAlpha = image.color.a - Time.deltaTime * fadeSpeed;
            image.color = new Color(image.color.r, image.color.g, image.color.b, newAlpha);
            if (newAlpha <= 0) break;
            yield return null;
        }

        action?.Invoke();
    }
}