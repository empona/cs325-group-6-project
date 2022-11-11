using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Warrior : MonoBehaviour
{
    bool warriorActive = CharacterSelect.classArray[0];
    public TMP_Text warriorText;

    public Button singleSlashButton;
    int singleSlashCost = 1; 
    int singleSlashDamage = 20;


    public Button strongSlashButton;
    int strongSlashCost = 3;
    int strongSlashDamage = 100;
    

    int spellRange = 1;
   
    
    Entity.Health warriorHealth = new Entity.Health(100);
    Entity.Action warriorAction = new Entity.Action();
    Entity.Movement warriorMovement = new Entity.Movement(-1);
    
    
    public void updatewarriorText(){
        warriorText.text=$"warrior Health: {warriorHealth.getHealth()}\r\nwarrior Action Points: {warriorAction.getActionPoints()}\n";
    }

    public void warriorButtonStates(){
        warriorAction.ButtonState(singleSlashCost, singleSlashButton);
        warriorAction.ButtonState(strongSlashCost,strongSlashButton);
   ;
      
    }

    public void castsingleSlash(){
        if(warriorAction.actionUsable(singleSlashCost)){
            if(Entity.Combat.selectedEnemy != null){
                if(Entity.Combat.isEnemyInRange(Entity.Combat.selectedEnemy,this.gameObject.transform.position.x,this.gameObject.transform.position.y, spellRange)){
                    Entity.Combat.selectedEnemy.enemyHealth.reduceHealth(singleSlashDamage);
                    Entity.Combat.selectedEnemy.enemyTarget.generateThreat(typeof(Warrior),singleSlashDamage);
                    warriorAction.actionPointReduction(singleSlashCost);

                }
                else{
                    Debug.Log("Enemy not close enough");
                }
            }
            else{
                //Display a error message to center screen so people know what they are doing wrong?
                Debug.Log("Unknown Enemy; Click an enemy to set as current enemy");
            }
        }
    }

    public void caststrongSlash(){
        if(warriorAction.actionUsable(strongSlashCost)){
            if(Entity.Combat.selectedEnemy != null){
                if(Entity.Combat.isEnemyInRange(Entity.Combat.selectedEnemy,this.gameObject.transform.position.x,this.gameObject.transform.position.y, spellRange)){
                    Entity.Combat.selectedEnemy.enemyHealth.reduceHealth(strongSlashDamage);
                    Entity.Combat.selectedEnemy.enemyTarget.generateThreat(typeof(Warrior),strongSlashDamage);
                    warriorAction.actionPointReduction(strongSlashCost);
        
                    
                }
                else{
                    Debug.Log("Enemy not close enough");
                }
            }
            else{
                //Display a error message to center screen so people know what they are doing wrong?
                Debug.Log("Unknown Enemy; Click an enemy to set as current enemy");
            }
        }
    }

 
    

   
    
    void Start(){
        Entity.setEntityActive(warriorActive,this.gameObject);
        if(warriorActive == true){
            for (int i = 0; i < Entity.Combat.allyList.Length; i++){
                if(Entity.Combat.allyList[i] == null){
                    Entity.Combat.allyList[i] = this;
                    break;
                }
            }
        }
    }

    void Update(){
        warriorButtonStates();
        updatewarriorText();    
    }
}



