using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
                         
        public static GameManager instance = null;                
        [HideInInspector] public bool playersTurn = true;   
        private int level = 1;  
        public List<Enemy> enemies;

        public void GameOver()
        {

        }

         void Awake()
        {
            
            if (instance == null)
            {
                
                instance = this;
            }
          
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
