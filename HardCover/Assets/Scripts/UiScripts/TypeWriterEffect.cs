using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TypeWriterEffect : MonoBehaviour
{
    public float delayBeforeConvo = 0.05f;
    public float delayBetweenCharacter = 0.05f;

    private TextMeshProUGUI mLocalText;
    private string mToPrint;

    // Start is called before the first frame update
    void Awake()
    {
        mLocalText = GetComponent<TextMeshProUGUI>();
    }

    public void StartEffect(string text)
    {
        mToPrint = text;
        mLocalText.text = "";

        StartCoroutine(TypeWriter());
    }

    private IEnumerator TypeWriter()
    {
        yield return new WaitForSeconds(delayBeforeConvo);

        foreach (char c in mToPrint)
        {
            mLocalText.text += c;
            yield return new WaitForSeconds(delayBetweenCharacter);
        }
    }
}