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

    Entity.Health mageHealth = new Entity.Health(70);
    Entity.Action mageAction = new Entity.Action();
    Entity.Movement mageMovement = new Entity.Movement(-1);

    public void updateMageText(){
        mageText.text=$"Mage Health: {mageHealth.getHealth()}\r\nMage Action Points: {mageAction.getActionPoints()}";
    }


    public void castLightningZap(){
        if(mageAction.actionUsable(lightningZapCost)){
            if(Entity.Combat.selectedEnemy != null){
                if(Entity.Combat.isEnemyInRange(Entity.Combat.selectedEnemy,this.gameObject.transform.position.x,this.gameObject.transform.position.y, spellRange)){
                    Entity.Combat.selectedEnemy.enemyHealth.reduceHealth(lightningZapDamage);
                    Entity.Combat.selectedEnemy.enemyTarget.generateThreat(typeof(Mage),lightningZapDamage);
                    mageAction.actionPointReduction(lightningZapCost);
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
        if(mageAction.actionUsable(fireballCost)){
            if(Entity.Combat.selectedEnemy != null){
                if(Entity.Combat.isEnemyInRange(Entity.Combat.selectedEnemy,this.gameObject.transform.position.x,this.gameObject.transform.position.y, spellRange)){
                    Entity.Combat.selectedEnemy.enemyHealth.reduceHealth(fireballDamage);
                    Entity.Combat.selectedEnemy.enemyTarget.generateThreat(typeof(Mage),fireballDamage);
                    mageAction.actionPointReduction(fireballCost);
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
        if(mageAction.actionUsable(meteorStrikeCost)){
            if(Entity.Combat.selectedEnemy != null){
                if(Entity.Combat.isEnemyInRange(Entity.Combat.selectedEnemy,this.gameObject.transform.position.x,this.gameObject.transform.position.y, spellRange)){
                    Enemy[] enemiesAffected = Entity.Combat.enemiesNearSelectedEnemy(Entity.Combat.selectedEnemy,Entity.Combat.enemyList,Entity.Combat.size,aoeRange);
                    foreach (var item in enemiesAffected){
                        if(item != null){
                            item.enemyHealth.reduceHealth(meteorStrikeDamage);
                            item.enemyTarget.generateThreat(typeof(Mage),meteorStrikeDamage);
                        }
                    }
                    mageAction.actionPointReduction(meteorStrikeCost);
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
        mageAction.ButtonState(lightningZapCost, lightningZapButton);
        mageAction.ButtonState(fireballCost,fireballButton);
        mageAction.ButtonState(meteorStrikeCost, meteorStrikeButton);
    }
    void Start(){
        Entity.setEntityActive(mageActive,this.gameObject);
        if(mageActive == true){
            for (int i = 0; i < Entity.Combat.allyList.Length; i++){
                if(Entity.Combat.allyList[i] == null){
                    Entity.Combat.allyList[i] = this;
                    break;
                }
            }
        }
    }

    void Update(){
        mageButtonStates();
        updateMageText();    
    }
    
}
