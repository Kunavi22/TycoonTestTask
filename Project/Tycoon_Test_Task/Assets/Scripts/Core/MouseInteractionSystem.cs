using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInteractionSystem : MonoBehaviour
{

    Clickable currentlyHovered;

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );

        //Hover and click logic through interface
        if (Physics.Raycast(ray, out RaycastHit hitInfo, float.PositiveInfinity, 1<<6))
        {
            Clickable clickableHit = hitInfo.collider.GetComponent<Clickable>();

            if(currentlyHovered != clickableHit)
            {
                if(currentlyHovered != null) currentlyHovered.OnUnHover();
                currentlyHovered = clickableHit;
                currentlyHovered.OnHover();
            }

            if (Input.GetMouseButtonDown(0)) currentlyHovered.OnClick();

        }
        else if (currentlyHovered != null)
        {
            currentlyHovered.OnUnHover();
            currentlyHovered = null;
        }
    }
}
