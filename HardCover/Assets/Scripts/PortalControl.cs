using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is on the player
public class PortalControl : MonoBehaviour
{
    [HideInInspector]
    public List<PortalTrigger> activePortals;

    [SerializeField] KeyCode portalKey;

    private void Start()
    {
        activePortals = new List<PortalTrigger>();
    }

    private void Update()
    {
        if (activePortals.Count > 0 && activePortals[0] != null)
        {
            // Show "Press E" thingy on UI
            if (Input.GetKeyDown(portalKey))
            {
                activePortals[0].StartInteraction();
            }
        }
    }
}