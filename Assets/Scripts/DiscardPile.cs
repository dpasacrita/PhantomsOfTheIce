using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscardPile : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public GameObject FindTopCard()
    {
        // Declare the object we're looking for
        GameObject bottomChild;

        // Triple check the pile has a child before we go getting one
        if (transform.childCount > 0)
        {
            int childCount = transform.childCount;
            bottomChild = transform.GetChild(childCount - 1).gameObject;
            return bottomChild;

        }
        // If there are no children, do nothing.
        else {
            return null;
        }
    }

    public void SetOnlyTopChildVisible()
    {
        // Loop through all children and make all of them invisible
        foreach (Transform child in this.transform)
        {
            child.GetComponent<CanvasGroup>().alpha = 0f; //this makes everything transparent
            child.GetComponent<CanvasGroup>().blocksRaycasts = false; //this prevents the UI element to receive input events
        }

        // Now make the top card visible
        // Double check the pile has a child before we go getting one
        if (transform.childCount > 0)
        {
            GameObject topChild = FindTopCard();
            // Make the card visible and NOT clickable
            topChild.GetComponent<CanvasGroup>().alpha = 1f;
            topChild.GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
    }
}
