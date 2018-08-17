using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : Draggable
{

    public string cardName = "Default Name";
    public enum Slot { Forward, Defenseman, Goalie };
    public Slot cardType = Slot.Forward;
    public int power = 0;
    GameObject nameField;
    GameObject powerField;

    // Use this for initialization
    void Start()
    {

        // Set the Forward's Name
        nameField = this.transform.Find("Card Name").gameObject;
        nameField.GetComponent<Text>().text = cardName;

        // Now set it's Power
        powerField = this.transform.Find("Power").gameObject;
        powerField.GetComponent<Text>().text = power.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        // We need to hide the card if it's currently inside a deck.
        GameObject cardParent = this.transform.parent.gameObject;
        if (cardParent.GetComponent("Deck") != null)
        {
            this.GetComponent<CanvasGroup>().alpha = 0f; //this makes everything transparent
            this.GetComponent<CanvasGroup>().blocksRaycasts = false; //this prevents the UI element to receive input events
        }
    }
}
