using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTileAction : MonoBehaviour, ITileAction
{
    public bool OnPlayerEnter(PlayerControl player)
    {
        Debug.Log(" you reach trap tile");
        return true;
    }
}

