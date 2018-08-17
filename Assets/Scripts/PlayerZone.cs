using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{

    public void OnPointerEnter(PointerEventData eventData)
    {
        // If nothing is being dragged, don't do anything.
        if (eventData.pointerDrag == null)
            return;

        // Grab the thing being dragged on here.
        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
        // If it's not null, check how many children are on here
        if (d != null)
        {
            // If there's no children in the zone object, set the parent here.
            //if (this.transform.childCount == 0)
                //d.placeholderParent = this.transform;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // If nothing is being dragged, don't do anything.
        if (eventData.pointerDrag == null)
            return;

        // Grab the thing being dropped on here.
        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
        // If it's not null, set the placeholder's parent to the old one.
        if (d != null && d.placeholderParent == this.transform)
        {
            d.placeholderParent = d.parentToSnapTo;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        // Grab the thing being dropped on here.
        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
        // If it's not null, set it's parent to this.
        if (d != null)
        {
            // If it's not null, check how many children are on here
            if ((this.transform.childCount) < 1)
                d.parentToSnapTo = this.transform;
                
        }
    }
}