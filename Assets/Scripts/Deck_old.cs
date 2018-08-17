using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Deck_old : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    // Start by getting a list of all the children
    Transform[] cards = null;
    GameObject drawnCard = null;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public static Transform[] GetTopLevelChildren(Transform Parent)
    {
        Transform[] Children = new Transform[Parent.childCount];
        for (int ID = 0; ID < Parent.childCount; ID++)
        {
            if (Parent.GetChild(ID).name != "Deck Image")
                Children[ID] = Parent.GetChild(ID);
        }
        return Children;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Get all the cards and pick one at random
        cards = GetTopLevelChildren(this.transform);
        drawnCard = (GameObject)((Transform)cards[Random.Range(0, cards.Length)]).gameObject;

        // Set Card's Parent to Deck's Parent
        drawnCard.transform.SetParent(this.transform.parent.parent);

        // Make the card visible
        drawnCard.GetComponent<CanvasGroup>().alpha = 1f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Move this card to whereever the pointer is
        drawnCard.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {

    }
}
