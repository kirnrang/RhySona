using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalTileAction : MonoBehaviour, ITileAction
{
    public bool OnPlayerEnter(PlayerControl player)
    {
        Debug.Log(" Goal tile has reached!");
        return true;
    }
}

