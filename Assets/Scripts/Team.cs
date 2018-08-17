using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Team : DropZone
{
    public int MAX_FORWARDS = 3;
    public int MAX_DEFENSEMEN = 2;
    public int MAX_GOALIES = 1;

    public int currentForwards = 0;
    public int currentDefensemen = 0;
    public int currentGoalies = 0;
}
