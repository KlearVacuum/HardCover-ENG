using TMPro;
using UnityEngine;

public class University : MonoBehaviour, IInteractable
{
    public GameObject NotificationGameObject;
    public float TransitionTimer = 1.0f;

    public Color TheRed, TheGreen;

    // mKnowledge == 100 && mEnergy > 60 && mCash >= 200;
    public TextMeshProUGUI KnowledgeText, EnergyText, CashText;

    private float mTimer = 0.0f;
    private bool mShrink = false, mGrow = false;

    private void Start()
    {
        KnowledgeText.text = "100%";
        EnergyText.text = "60%";
        CashText.text = "$200";
    }

    private void Update()
    {
        Vector3 vec = new Vector3(0.001f, 0.001f, 0.001f);

        if (mShrink)
        {
            mTimer += Time.deltaTime;
            NotificationGameObject.transform.localScale = Vector3.Lerp(Vector3.one * 0.25f, vec, mTimer / TransitionTimer);

            if (mTimer > TransitionTimer)
            {
                mTimer = 0;
                mShrink = false;
            }
        }

        if (mGrow)
        {
            mTimer += Time.deltaTime;
            NotificationGameObject.transform.localScale = Vector3.Lerp(vec, Vector3.one * 0.25f, mTimer / TransitionTimer);

            if (mTimer > TransitionTimer)
            {
                mTimer = 0;
                mGrow = false;
            }
        }
    }


    public void StartInteraction(GameObject interactor)
    {
        if (GlobalGameData.playerStats.CanPassUni())
        {
            GlobalGameData.blackScreenOverlay.GoToScene("End_UniSuccess");
        }
    }

    public InteractionPriority GetPriority()
    {
        return InteractionPriority.Default;
    }

    public GameObject GetObject()
    {
        return gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            mGrow = true;
            mShrink = false;

            KnowledgeText.color = GlobalGameData.playerStats.knowledge == 100 ? TheGreen : TheRed;
            EnergyText.color = GlobalGameData.playerStats.energy >= 60 ? TheGreen : TheRed;
            CashText.color = GlobalGameData.playerStats.cash >= 200 ? TheGreen : TheRed;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            mShrink = true;
            mGrow = false;
        }
    }
}