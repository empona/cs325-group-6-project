using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tank : MonoBehaviour
{
    bool tankActive = CharacterSelect.classArray[4];

    public TMP_Text tankText;


    public Button distractButton;
    int distractCost = 3; 
    int distractDamage = 20; //Using variables for balancing easier/I dont know what values will be set on. maybe we will use random values such as in between (x,y)


    public Button blockButton;
    int blockCost = 4;


    public Button tauntButton;
    int tauntCost = 7;

    int spellRange = 5000;

    public static Entity.Health health = new Entity.Health(200);
    public Entity.Action action = new Entity.Action();
    public Entity.Movement movement = new Entity.Movement(-1);

    public void updateTankText(){
        tankText.text=$"Tank Health: {(health.getHealth()+health.getOverHealth())}\r\nTank Action Points: {action.getActionPoints()}"; 
        // fun fact: \r is carriage return which is used in Windows as '\r\n' for EOL other operating systems use a mixture of \r or \n.
        // Right here its useless, it does nothing, im just bored.
    }


    public void castDistact(){
        if(action.actionUsable(distractCost)){
            if(Entity.Combat.selectedEnemy != null){
                if(Entity.Combat.isEnemyInRange(Entity.Combat.selectedEnemy,this.gameObject.transform.position.x,this.gameObject.transform.position.y, spellRange)){
                    Entity.Combat.selectedEnemy.enemyHealth.reduceHealth(distractDamage);
                    Entity.Combat.selectedEnemy.enemyTarget.generateThreat(typeof(Tank),distractDamage);
                    action.actionPointReduction(distractCost);
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

    public void castBlock(){
        //To do, idea is 10% dmg taken reduction per cast, lasts till tank's next round.
        //How to implement: int variable for damage taken in 1 round, heal for x% so that it was as if that % was blocked.
        Debug.Log("To be implemented");
    }

    public void castTaunt(){
        if(action.actionUsable(tauntCost)){
            if(Entity.Combat.selectedEnemy != null){
                if(Entity.Combat.isEnemyInRange(Entity.Combat.selectedEnemy,this.gameObject.transform.position.x,this.gameObject.transform.position.y, spellRange)){
                    Entity.Combat.selectedEnemy.enemyTarget.placeAtTop(typeof(Tank));
                    action.actionPointReduction(tauntCost);
                }
                else{
                    Debug.Log("Enemy not close enough");
                }
            }
            else{
                Debug.Log("Unknown Enemy; Click an enemy to set as current enemy");
            }
        }
    }

    
    public void tankButtonStates(){
        action.ButtonState(distractCost, distractButton);
        action.ButtonState(blockCost,blockButton);
        action.ButtonState(tauntCost, tauntButton);
    }
    void Start(){
        Entity.setEntityActive(tankActive,this.gameObject);
    }

    void Update(){
        tankButtonStates();
        updateTankText(); 
    }

    public void selectThisAlly(){
        Entity.Combat.selectedAlly = typeof(Tank);
    }
}
