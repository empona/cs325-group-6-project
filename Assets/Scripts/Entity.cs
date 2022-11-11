using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//I didnt know where to start / what I will need later so I wrote in a "data strucutre" sense.
//Everything that a class has in common with each other.
public class Entity:MonoBehaviour{
    
    public static System.Random randomGenerator = new System.Random();

    public static void setEntityActive(bool shouldBeActive, GameObject GameObjectEntity){
        GameObjectEntity.SetActive(shouldBeActive);
    }

    public class Action{

        public int actionPoints= 10;
        public int maxActionPoint = 10;
        

        public int getActionPoints(){
            return actionPoints;
        }

        public bool actionUsable(int actionCost){
            return actionPoints>=actionCost;
        }

        public void actionPointReduction(int actionCost){
            actionPoints= actionPoints-actionCost;
        }
        
        public void resetPoints(){
            actionPoints = maxActionPoint; 
        }

        public void ButtonState(int actionPointCost, Button ButtonUsed, int? ammoRestriction = null, int? currentAmmo = null){
            if(ammoRestriction == null || currentAmmo == null){
                if(actionUsable(actionPointCost)){
                    ButtonUsed.interactable = true;
                }
                else{
                    ButtonUsed.interactable = false;
                }
            }
            else{
                if(actionUsable(actionPointCost) && currentAmmo >= ammoRestriction){
                    ButtonUsed.interactable = true;
                }
                else{
                    ButtonUsed.interactable = false;
                }
            }
            
        }
    }

    public class Movement{
        int movementDistance;
        public Movement(int movementDistance){ //In case we want to have different amounts of movement per class type.
            this.movementDistance = movementDistance;
        }

        //To be done later in accordance to level design. "how big is x squares?" "are we still using squares?" "Were we ever using squares?"
    }

    public class Health{
        int maxHealth;
        int currentHealth;

        int overHealth;
        bool isDead;
        public Health(int startingHealth, int maxStartingHealth=-1){
            maxHealth = (maxStartingHealth == -1) ? startingHealth : maxStartingHealth;
            currentHealth = startingHealth;
            isDead = false;
            overHealth = 0;
        }
        public int getHealth(){
            return currentHealth;
        }

        public int getOverHealth(){
            return overHealth;
        }

        public bool deadCheck(){
            isDead = (currentHealth == 0);
            return isDead;
        }

        public int reduceHealth(int reduction){
            if(overHealth > 0){
                if(reduction > overHealth){
                    int gap = reduction - overHealth;
                    overHealth = 0;
                    currentHealth = currentHealth - gap;
                }
                else{
                    overHealth = overHealth - reduction;
                }
            }
            else{
                currentHealth = currentHealth - reduction;
            }
            deadCheck();
            return currentHealth;
        }

        public int addHealth(int additionToBase, int additionToOverHealth){   
            currentHealth = currentHealth + additionToBase;
            if(currentHealth > maxHealth){
                currentHealth = maxHealth;
            }
            if(additionToOverHealth > 0 && currentHealth < maxHealth){
                int healthMissing = maxHealth - currentHealth;
                if(healthMissing >= additionToOverHealth){
                    currentHealth = currentHealth + additionToOverHealth;
                }
                else{
                    currentHealth = currentHealth + healthMissing;
                    overHealth = additionToOverHealth -healthMissing;
                }
            }
            else if(additionToOverHealth >0 && currentHealth==maxHealth){
                overHealth=overHealth+additionToOverHealth;
            }
            return currentHealth;
        }

    }
    
    public class Combat{ //A class dedicated to combat itself i.e. what enemy the player is attacking, finding what enemies are in x range for AOE, etc, whether the player unit is in range to attack.

        public static Enemy[] enemyList = new Enemy[100]; // I imagine that there would never be more than 100 enemies in the game, simply due to time concerns/scale-ability/why would you need more than 100?
        public static int size = 0; //Current size of enemylist.

        public static Enemy selectedEnemy = null;

        public static List<System.Type> allyList = new List<System.Type>(3); 
        public static System.Type selectedAlly =null;
        
        public static Enemy[] enemiesNearSelectedEnemy(Enemy selectedEnemy, Enemy[] enemyList, int size, int range){
            GameObject enemyObject;
            float xPosition;
            float yPosition;
            Enemy[] nearbyEnemies = new Enemy[10]; //I'd imagine that there would never be more than 10 enemies in given range. 
            int nearbyEnemiesSize = 0;
            float minX = (selectedEnemy.gameObject.transform.position.x)-range/2; // (minX, maxY)     (maxX maxY)
            float maxX = (selectedEnemy.gameObject.transform.position.x)+range/2; // R |------------->| R (Range x)
            float minY = (selectedEnemy.gameObject.transform.position.y)-range/2; // A |              | A
            float maxY = (selectedEnemy.gameObject.transform.position.y)+range/2; // N |              | N
            for (int i = 0; i < size; i++){                                       // G |      *       | G    (selected is meant to be center/origin of square)
                enemyObject = enemyList[i].gameObject;                            // E |  (selected)  | E
                xPosition = enemyObject.transform.position.x;                     // Y | ------------>| Y (Range X)
                yPosition = enemyObject.transform.position.y;                  //(minX, minY)      (maxX, minY)
                if(xPosition <= maxX && xPosition >= minX){
                    if(yPosition <= maxY && yPosition >= minY){
                        nearbyEnemies[nearbyEnemiesSize] = enemyList[i];
                        nearbyEnemiesSize++;
                    }
                }

            }
            return nearbyEnemies;
        }
        public static bool isEnemyInRange(UnityEngine.Component selectedEnemy, float casterX, float casterY, int range, GameObject selectedAlly=null){
            float minX;
            float maxX;
            float minY;
            float maxY;
            if(selectedAlly != null){
                minX = (selectedAlly.transform.position.x)-range; 
                maxX = (selectedAlly.transform.position.x)+range; 
                minY = (selectedAlly.transform.position.y)-range; 
                maxY = (selectedAlly.transform.position.y)+range;
            }
            else{
                minX = (selectedEnemy.gameObject.transform.position.x)-range; 
                maxX = (selectedEnemy.gameObject.transform.position.x)+range; 
                minY = (selectedEnemy.gameObject.transform.position.y)-range; 
                maxY = (selectedEnemy.gameObject.transform.position.y)+range;
            }
            
            if(casterX <= maxX && casterX >= minX){
                if(casterY <= maxY && casterY >= minY){
                    return true;
                }
            }
            return false;
        }

        public static bool isAllyInRange(System.Type selectedAlly, float casterX, float casterY, int range){
            Object test = FindObjectOfType(selectedAlly);
            if(test != null){
                string objectName = test.ToString(); //Returns ObjectName (Type) when wanting just ObjectName
                objectName = objectName.Substring(0,objectName.IndexOf('(')-1); 
                GameObject representingGameObject = GameObject.Find(objectName);
                return isEnemyInRange(null,casterX,casterY,range, representingGameObject);
            }
            return false;
        }


        public class threatTable{
            List<KeyValuePair<System.Type,int>> table = new List<KeyValuePair<System.Type,int>>(3); //Only 3 playable characters
            public threatTable(){
                for (int i = 0; i < CharacterSelect.classArray.Length; i++){ //First target is usually the last element to be added.
                    if(CharacterSelect.classArray[i] == true){
                        KeyValuePair<System.Type, int> threatPair;
                        switch (i){
                            case 0:
                                threatPair = new KeyValuePair<System.Type, int>(typeof(Warrior),0);
                                table.Add(threatPair);
                                break;
                            case 1:
                                threatPair = new KeyValuePair<System.Type, int>(typeof(Ranger),0);
                                table.Add(threatPair);
                                break;
                            case 2:
                                threatPair = new KeyValuePair<System.Type, int>(typeof(Healer),0);
                                table.Add(threatPair);
                                break;
                            case 3:
                                threatPair = new KeyValuePair<System.Type, int>(typeof(Mage),0);
                                table.Add(threatPair);
                                break;
                            case 4:
                                threatPair = new KeyValuePair<System.Type, int>(typeof(Tank),0);
                                table.Add(threatPair);
                                break;
                        }
                        
                    }
                }
            }

            public void generateThreat(System.Type playableCharacter, int amountOfThreat){
                if(playableCharacter == typeof(Tank)){
                    amountOfThreat = amountOfThreat*3;
                }

                for (int i = 0; i < table.Count; i++){
                    KeyValuePair<System.Type, int> item = table[i];
                    if(playableCharacter == item.Key){
                        amountOfThreat = amountOfThreat + item.Value;
                        var threatPair = new KeyValuePair<System.Type, int>(playableCharacter,amountOfThreat);
                        table.Remove(item);
                        table.Add(threatPair);
                        break;
                    }
                }
            }

            public KeyValuePair<System.Type, int> findPrimaryTarget(){
                KeyValuePair<System.Type, int> topThreat = table[0];
                for (int i = 0; i < table.Count; i++){
                    KeyValuePair<System.Type, int> item = table[i];
                    if(item.Value >= topThreat.Value){
                        topThreat = item;
                    }
                }
                return topThreat;
            }

            public void placeAtTop(System.Type playableCharacter){ //To be used by tank's taunt.
                KeyValuePair<System.Type, int> topThreat = findPrimaryTarget();
                for (int i = 0; i < table.Count; i++){
                    KeyValuePair<System.Type, int> item = table[i];
                    if(item.Key == playableCharacter){
                        table.Remove(item);
                        break;
                    }
                }
                int newThreat = (topThreat.Value>200)? (int) ((double)topThreat.Value * 1.25) : topThreat.Value+250; //In the case where taunt is the first ability casted on the target.
                var threatPair = new KeyValuePair<System.Type, int>(playableCharacter,newThreat);
                table.Add(threatPair);
            }

            
        }
    }
    
    void Start(){
        //Generate allyList, needed for healer (AOE heal).
        for (int i = 0; i < CharacterSelect.classArray.Length; i++){
            if(CharacterSelect.classArray[i] == true){
                switch (i){
                    case 0:
                        Combat.allyList.Add(typeof(Warrior));
                        break;
                    case 1:
                        Combat.allyList.Add(typeof(Ranger));
                        break;
                    case 2:
                        Combat.allyList.Add(typeof(Healer));
                        break;
                    case 3:
                        Combat.allyList.Add(typeof(Mage));
                        break;
                    case 4:
                        Combat.allyList.Add(typeof(Tank));
                        break;
                }
            }
        }
    }
}
