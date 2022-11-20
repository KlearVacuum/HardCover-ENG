using UnityEngine;
using UnityEngine.UI;

public class ShowActionableOrInteractable : MonoBehaviour
{
    public Image EImage, SpaceImage;
    private InteractionController ic = null;

    private void Start()
    {
        ic = GlobalGameData.playerStats.GetComponent<InteractionController>();
    }

    private void Update()
    {
        EImage.color = ic.CanInteract() ? Color.white : Color.grey;
        SpaceImage.color = ic.CanAction() ? Color.white : Color.grey;
    }
}