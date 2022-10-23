using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalControl : MonoBehaviour
{
    [SerializeField] KeyCode portalKey;
    [HideInInspector]
    public List<PortalTrigger> activePortals;

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
                activePortals[0].Teleport(gameObject);
            }
        }
    }
}
