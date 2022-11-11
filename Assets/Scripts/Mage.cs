using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Mage : MonoBehaviour{

    bool mageActive = CharacterSelect.classArray[3];

    public TMP_Text mageText;


    public Button lightningZapButton;
    int lightningZapCost = 2; 
    int lightningZapDamage = 50; //Using variables for balancing, I dont know what values will be set on, maybe we will use random values such as in between (x,y)?


    public Button fireballButton;
    int fireballCost = 8;
    int fireballDamage = 250;

    public Button meteorStrikeButton;
    int meteorStrikeCost = 10;
    int meteorStrikeDamage = 200;

    int spellRange = 5000;

    int aoeRange = 3000;

    public static Entity.Health health = new Entity.Health(70);
    public Entity.Action action = new Entity.Action();
    public Entity.Movement movement = new Entity.Movement(-1);

    public void updateMageText(){
        mageText.text=$"Mage Health: {health.getHealth()+health.getOverHealth()}\r\nMage Action Points: {action.getActionPoints()}";
    }


    public void castLightningZap(){
        if(action.actionUsable(lightningZapCost)){
            if(Entity.Combat.selectedEnemy != null){
                if(Entity.Combat.isEnemyInRange(Entity.Combat.selectedEnemy,this.gameObject.transform.position.x,this.gameObject.transform.position.y, spellRange)){
                    Entity.Combat.selectedEnemy.enemyHealth.reduceHealth(lightningZapDamage);
                    Entity.Combat.selectedEnemy.enemyTarget.generateThreat(typeof(Mage),lightningZapDamage);
                    action.actionPointReduction(lightningZapCost);
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

    public void castFireball(){
        if(action.actionUsable(fireballCost)){
            if(Entity.Combat.selectedEnemy != null){
                if(Entity.Combat.isEnemyInRange(Entity.Combat.selectedEnemy,this.gameObject.transform.position.x,this.gameObject.transform.position.y, spellRange)){
                    Entity.Combat.selectedEnemy.enemyHealth.reduceHealth(fireballDamage);
                    Entity.Combat.selectedEnemy.enemyTarget.generateThreat(typeof(Mage),fireballDamage);
                    action.actionPointReduction(fireballCost);
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

    public void castMeteorStrike(){
        if(action.actionUsable(meteorStrikeCost)){
            if(Entity.Combat.selectedEnemy != null){
                if(Entity.Combat.isEnemyInRange(Entity.Combat.selectedEnemy,this.gameObject.transform.position.x,this.gameObject.transform.position.y, spellRange)){
                    Enemy[] enemiesAffected = Entity.Combat.enemiesNearSelectedEnemy(Entity.Combat.selectedEnemy,Entity.Combat.enemyList,Entity.Combat.size,aoeRange);
                    foreach (var item in enemiesAffected){
                        if(item != null){
                            item.enemyHealth.reduceHealth(meteorStrikeDamage);
                            item.enemyTarget.generateThreat(typeof(Mage),meteorStrikeDamage);
                        }
                    }
                    action.actionPointReduction(meteorStrikeCost);
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

    
    public void mageButtonStates(){
        action.ButtonState(lightningZapCost, lightningZapButton);
        action.ButtonState(fireballCost,fireballButton);
        action.ButtonState(meteorStrikeCost, meteorStrikeButton);
    }
    void Start(){
        Entity.setEntityActive(mageActive,this.gameObject);
    }

    public void selectThisAlly(){
        Entity.Combat.selectedAlly = typeof(Mage);
        Debug.Log(Entity.Combat.selectedAlly);
        var test = FindObjectOfType(Entity.Combat.selectedAlly);
        Debug.Log(test);
    }

    void Update(){
        mageButtonStates();
        updateMageText();    
    }
    
}
