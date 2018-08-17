using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Match : MonoBehaviour {

    // Match Data
    public int homeScore = 0;
    public int awayScore = 0;
    GameObject HomeZone;
    GameObject AwayZone;

    // Card Data
    GameObject HomeCard;
    GameObject AwayCard;
    int homePower = 0;
    int awayPower = 0;
    Card.Slot homeType;
    Card.Slot awayType;


    // Use this for initialization
    void Start () {
        
        // Find both Zone objects
        HomeZone = GameObject.Find("Home Zone");
        AwayZone = GameObject.Find("Away Zone");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RetrieveCards()
    {
        // Get the card in each zone, if applicable
        try
        {
            HomeCard = HomeZone.transform.GetChild(0).gameObject;
        }
        catch (UnityException e)
        {
            Debug.Log("ERROR: Cannot set Home Card!");
            Debug.Log("ERROR: " + e);
            return;
        }
        try
        {
            AwayCard = AwayZone.transform.GetChild(0).gameObject;
        }
        catch (UnityException e)
        {
            Debug.Log("ERROR: Cannot set Away Card!");
            Debug.Log("ERROR: " + e);
            return;
        }

        // Get each card's power and Card Type
        homePower = HomeCard.GetComponent<Card>().power;
        awayPower = AwayCard.GetComponent<Card>().power;
        homeType = HomeCard.GetComponent<Card>().cardType;
        awayType = AwayCard.GetComponent<Card>().cardType;
    }

    public void DetermineScore()
    {
        // First get the card data from the field
        RetrieveCards();
        
        // Determine the winner of the current player matchup
        // First dermine - Are either cards goalies?
        if (homeType == Card.Slot.Goalie || awayType == Card.Slot.Goalie)
        {
            // Now figure out which team has the goalie.
            // Are both players goalies?
            if (homeType == Card.Slot.Goalie && awayType == Card.Slot.Goalie)
            {
                // Do nothing, we'll check the score normally down below
            }
            else
            {
                // No Score - return
                Debug.Log("MATCH: Blocked Shot! No score.");
                Debug.Log("MATCH: Home: " + homeScore + " Away: " + awayScore);
                return;
            }

        }
        // Neither card is a goalie.
        // Are both powers the same?
        if (homePower == awayPower)
        {
            // No Score - return
            Debug.Log("MATCH: Cards equal in power. No score.");
            Debug.Log("MATCH: Home: " + homeScore + " Away: " + awayScore);
            return;
        }
        // If Home is stronger than away, home scores
        if (homePower > awayPower)
        {
            homeScore++;
            Debug.Log("MATCH: Home: " + homeScore + " Away: " + awayScore);
            return;
        }
        // If Away is stronger, away scores.
        else if (awayPower > homePower)
        {
            awayScore++;
            Debug.Log("MATCH: Home: " + homeScore + " Away: " + awayScore);
            return;
        }
        // If something else happened, error.
        else
        {
            Debug.Log("ERROR: Something is very wrong with the card power. Check.");
            return;
        }

    }

    public void DetermineWinner()
    {
        
        // Determine which team is the winner, or if there's a tie.
        if (homeScore > awayScore)
        {
            Debug.Log("Home Team has won! Resetting Score.");
            homeScore = 0;
            awayScore = 0;
        }
        else if (awayScore > homeScore)
        {
            Debug.Log("Away Team has won! Resetting Score.");
            homeScore = 0;
            awayScore = 0;
        }
        else
        {
            Debug.Log("Game is a tie. Resetting score and beginning sudden death.");
            homeScore = 0;
            awayScore = 0;
            Debug.Log("Sudden death rules TBD.");

        }
    }
}
