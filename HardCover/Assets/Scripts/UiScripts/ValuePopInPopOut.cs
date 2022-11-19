using TMPro;
using UnityEngine;

public class ValuePopInPopOut : MonoBehaviour
{
    public TextMeshProUGUI ValueText;
    public float FadeInTime = 0.1f;
    public float FadeOutTime = 0.5f;

    public static Vector3 sDown = Vector3.down;

    private float mInternalTimer = 0.0f;
    private Vector3 mStartingPosition;

    private bool mIsUpdated = false;

    private void Start()
    {
        mStartingPosition = transform.position;
    }

    private void Update()
    {
        mInternalTimer += Time.deltaTime;

        float percentage;
        if (mInternalTimer < FadeInTime && !mIsUpdated)
        {
            percentage = mInternalTimer / FadeInTime;

            transform.position = Vector3.Lerp(mStartingPosition, mStartingPosition + (-sDown), percentage);
            ValueText.color = new Color(ValueText.color.r,
                ValueText.color.g,
                ValueText.color.b,
                255.0f * percentage);
        }
        else if (mInternalTimer < FadeInTime + FadeOutTime)
        {
            if (!mIsUpdated)
            {
                mIsUpdated = true;
                mStartingPosition = transform.position;
            }

            percentage = (mInternalTimer - FadeInTime) / FadeOutTime;

            transform.position = Vector3.Lerp(mStartingPosition, mStartingPosition + (-sDown), percentage);
            ValueText.color = new Color(ValueText.color.r,
                ValueText.color.g,
                ValueText.color.b,
                255.0f * (1.0f - percentage));
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetText(string t)
    {
        ValueText.SetText(t);
    }

    public void SetColor(Color c)
    {
        ValueText.color = c;
    }
}