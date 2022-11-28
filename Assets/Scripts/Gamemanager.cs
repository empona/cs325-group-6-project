using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
   
        public float levelStartDelay = 2f;                        
        public float turnDelay = 0.1f;                            
        public int playerFoodPoints = 100;                        
        public static GameManager instance = null;                
        [HideInInspector] public bool playersTurn = true;     

        public void GameOver()
        {

        }

        public void changeTurn()
        {
            if(playersTurn == true)
            {
                playersTurn = false;
            }

            else
            {
                playersTurn = true;
            }
        }


    



}
