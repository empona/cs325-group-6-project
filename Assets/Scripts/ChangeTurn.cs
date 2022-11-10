using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTurn : MonoBehaviour
{
    //0 for player, 1 for AI
    public int turn = 0;
    // Start is called before the first frame update
    public void EndTurn()
    {
        turn = 1;
    }
    
    public void PlayerTurn()
    {
        turn = 0;
    }
}
