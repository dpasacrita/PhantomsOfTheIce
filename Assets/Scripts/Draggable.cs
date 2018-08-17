using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

    // This is where the card will snap to when let go.
    public Transform parentToSnapTo = null;
    public Transform placeholderParent = null;

    // Create a new placeholder object
    // This will take the place of the object whenever it is dragged
    GameObject placeholder = null;

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Create the placeholder
        placeholder = new GameObject();
        // Set parent = the Draggable's parent
        placeholder.transform.SetParent(this.transform.parent);
        // Create a new layout element and add it to the placeholder
        LayoutElement le = placeholder.AddComponent<LayoutElement>();
        // Set the height and width = the Draggable's values.
        le.preferredWidth = this.GetComponent<LayoutElement>().preferredWidth;
        le.preferredHeight = this.GetComponent<LayoutElement>().preferredHeight;
        le.flexibleWidth = 0;
        le.flexibleHeight = 0;

        // Put the placeholder where you grabbed the Draggable from.
        placeholder.transform.SetSiblingIndex(this.transform.GetSiblingIndex());

        // Save our original parent
        parentToSnapTo = this.transform.parent;
        // Default the placeholder's parent to our current parent to snap to.
        placeholderParent = parentToSnapTo;
        // Set parent to parent's parent
        this.transform.SetParent(this.transform.parent.parent);

        // Turn off RayCast blocking
        // This will let us interact with the tabletop and hand
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Move this card to whereever the pointer is
        this.transform.position = eventData.position;

        if (placeholder.transform.parent != placeholderParent)
            placeholder.transform.SetParent(placeholderParent);

        int newSiblingIndex = placeholderParent.childCount;

        // For loop that will let us rearrange our cards
        // Loop through all the children in the parent
        for (int i = 0; i < placeholderParent.childCount; i++)
        {
            // If the card moves to the left of the child
            // The placeholder will take its place in the index.
            if (this.transform.position.x < placeholderParent.GetChild(i).position.x)
            {
                newSiblingIndex = i;

                if (placeholder.transform.GetSiblingIndex() < newSiblingIndex)
                    newSiblingIndex--;
                break;
            }
        }

        placeholder.transform.SetSiblingIndex(newSiblingIndex);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Snap to the current Parent, index to wherever the placeholder is.
        this.transform.SetParent(parentToSnapTo);
        this.transform.SetSiblingIndex(placeholder.transform.GetSiblingIndex());

        // Turn on RayCast blocking
        // This will let us touch the card again in the future
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        // Destroy the placeholder.
        Destroy(placeholder);
    }
}
