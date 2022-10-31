using System.Collections.Generic;
using UnityEngine;

public interface IShowUiPopUp
{
    public List<string> TagsToInteractWith();
    public void TriggerEnter(Collider2D collision);
    public void TriggerExit(Collider2D collision);
}