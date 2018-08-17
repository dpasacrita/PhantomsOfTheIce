using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Deck : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    // Deck Type Slot
    public enum Slot { Forward, Defenseman, Goalie };
    public Slot deckType = Slot.Forward;

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
            Children[ID] = Parent.GetChild(ID);
        }
        return Children;
    }

    public void OnMouseDown(PointerEventData eventData)
    {

    }

    public void AddPlayer()
    {
        // First see if there's any cards to grab
        if (this.transform.childCount == 0)
        {
            Debug.Log("No Cards left! Nothing to draw.");
            return;
        }

        // Find the Team
        GameObject team = GameObject.Find("Hand");

        // Switch statement to determine decktype (F, D, G)
        // Then we see if we're already maxed out on that kind of player.
        switch (deckType)
        {
            // Forward Deck
            case Slot.Forward:
                // Check if the team is maxed out on forwards already
                if (team.GetComponent<Team>().currentForwards >= team.GetComponent<Team>().MAX_FORWARDS)
                {
                    Debug.Log("Team already has too many forwards!");
                    return;
                }
                break;
            // Defensemen Deck
            case Slot.Defenseman:
                // Check if the team is maxed out on Defensemen already
                if (team.GetComponent<Team>().currentDefensemen >= team.GetComponent<Team>().MAX_DEFENSEMEN)
                {
                    Debug.Log("Team already has too many Defensemen!");
                    return;
                }
                break;
            // Goalie Deck
            case Slot.Goalie:
                // Check if the team is maxed out on Goalies already
                if (team.GetComponent<Team>().currentGoalies >= team.GetComponent<Team>().MAX_GOALIES)
                {
                    Debug.Log("Team already has a goalie!");
                    return;
                }
                break;
        }

        // Start by getting a list of all the children
        Transform[] cards;
        GameObject drawnCard = null;

        // Get all the cards and pick one at random
        cards = GetTopLevelChildren(this.transform);
        drawnCard = (GameObject)((Transform)cards[Random.Range(0, cards.Length)]).gameObject;


        // Set Card's Parent to the Hand
        drawnCard.transform.SetParent(team.transform);

        // Make the card visible and clickable
        drawnCard.GetComponent<CanvasGroup>().alpha = 1f;
        drawnCard.GetComponent<CanvasGroup>().blocksRaycasts = true;

        // Switch statement to determine decktype (F, D, G)
        // Then we increase the count of players on the team.
        switch (deckType)
        {
            // Forward Deck
            case Slot.Forward:
                team.GetComponent<Team>().currentForwards++;
                break;
            // Defensemen Deck
            case Slot.Defenseman:
                team.GetComponent<Team>().currentDefensemen++;
                break;
            // Goalie Deck
            case Slot.Goalie:
                team.GetComponent<Team>().currentGoalies++;
                break;
        }
    }

    public void DraftPlayer()
    {
        // First see if there's any cards to grab
        if (this.transform.childCount == 0)
        {
            Debug.Log("No Cards left! Nothing to draw.");
            return;
        }

        // Find the Team
        GameObject team = GameObject.Find("Hand");

        // Start by getting a list of all the children
        Transform[] cards;
        GameObject drawnCard = null;

        // Get all the cards and pick one at random
        cards = GetTopLevelChildren(this.transform);
        drawnCard = (GameObject)((Transform)cards[Random.Range(0, cards.Length)]).gameObject;


        // Set Card's Parent to the Hand
        drawnCard.transform.SetParent(team.transform);

        // Make the card visible and clickable
        drawnCard.GetComponent<CanvasGroup>().alpha = 1f;
        drawnCard.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // If nothing is being dragged, don't do anything.
        if (eventData.pointerDrag == null)
            return;

        

    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }

    public void OnDrop(PointerEventData eventData)
    {
        // Debug Log
        Debug.Log("Triggered drop onto " + this.transform.name);
        
        // Grab the thing being dropped on here.
        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();

        // If it's not null, Perform a Draft depending on what kind of deck this is.
        if (d != null)
        {
            // Switch statement to determine decktype (F, D, G)
            switch (deckType)
            {
                // Forward Deck
                case Slot.Forward:
                    if (d.GetComponent<Card>().cardType == Card.Slot.Forward)
                    {
                        // Add a forward to the Team, THEN put the current Forward back in the deck
                        DraftPlayer();
                        d.parentToSnapTo = this.transform;
                        break;
                    }
                    break;
                // Defensemen Deck
                case Slot.Defenseman:
                    if (d.GetComponent<Card>().cardType == Card.Slot.Defenseman)
                    {
                        // Add a forward to the Team, THEN put the current Forward back in the deck
                        DraftPlayer();
                        d.parentToSnapTo = this.transform;
                        break;
                    }
                    break;
                // Goalie Deck
                case Slot.Goalie:
                    if (d.GetComponent<Card>().cardType == Card.Slot.Goalie)
                    {
                        // Add a forward to the Team, THEN put the current Forward back in the deck
                        DraftPlayer();
                        d.parentToSnapTo = this.transform;
                        break;
                    }
                    break;
            }
        }
    }
}
