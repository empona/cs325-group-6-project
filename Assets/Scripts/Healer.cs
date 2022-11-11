using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Healer : MonoBehaviour
{
    bool healerActive = CharacterSelect.classArray[2];

    public TMP_Text healerText;


    public Button restoreLifeButton;
    int restoreLifeCost = 2; 
    int restoreLifeAmount = 20; //Using variables for balancing easier/I dont know what values will be set on. maybe we will use random values such as in between (x,y)


    public Button grandLightButton;
    int grandLightCost = 6;
    int grandLightAmount = 30;


    public Button greaterAegisButton;
    int greaterAegisCost = 10;
    int greaterAegisAmount = 80;
    int spellRange = 5000;

    public static Entity.Health health = new Entity.Health(70);
    public Entity.Action action = new Entity.Action();
    public Entity.Movement movement = new Entity.Movement(-1);

    public Entity.Health getHealthClass(System.Type ally){
        if(ally == typeof(Tank)){
            return Tank.health;
        }
        if(ally == typeof(Healer)){
            return Healer.health;
        }
        if(ally == typeof(Mage)){
            return Mage.health;
        }
        if(ally == typeof(Ranger)){
            return Ranger.health;
        }
        return null;
    }


    public void updateHealerText(){
        healerText.text=$"Healer Health: {health.getHealth()+health.getOverHealth()}\r\nHealer Action Points: {action.getActionPoints()}"; 
    }


    public void castRestoreLife(){
        if(action.actionUsable(restoreLifeCost)){
            if(Entity.Combat.selectedAlly!= null){
                if(Entity.Combat.isAllyInRange(Entity.Combat.selectedAlly,this.gameObject.transform.position.x,this.gameObject.transform.position.y, spellRange)){
                    Entity.Health respectiveHealthClass = getHealthClass(Entity.Combat.selectedAlly);
                    respectiveHealthClass.addHealth(restoreLifeAmount,0);
                    action.actionPointReduction(restoreLifeCost);
                }
                else{
                    Debug.Log("Friendly character not close enough");
                }
            }
            else{
                Debug.Log("Select a friendly character");
            }
        }
    }

    public void castGrandLight(){
        if(action.actionUsable(grandLightCost)){
            if(Entity.Combat.selectedAlly != null){
                if(Entity.Combat.isAllyInRange(Entity.Combat.selectedAlly,this.gameObject.transform.position.x,this.gameObject.transform.position.y, spellRange)){
                    foreach(var item in Entity.Combat.allyList){
                        Entity.Health respectiveHealthClass = getHealthClass(item);
                        respectiveHealthClass.addHealth(grandLightAmount,0);
                    }
                    action.actionPointReduction(grandLightCost);
                }
                else{
                    Debug.Log("Friendly character not close enough");
                }
            }
            else{
                Debug.Log("Select a friendly character");
            }
        }
    }

    public void castGreaterAegis(){
        if(action.actionUsable(greaterAegisCost)){
            if(Entity.Combat.selectedAlly != null){
                if(Entity.Combat.isAllyInRange(Entity.Combat.selectedAlly,this.gameObject.transform.position.x,this.gameObject.transform.position.y, spellRange)){
                    Entity.Health respectiveHealthClass = getHealthClass(Entity.Combat.selectedAlly);
                    respectiveHealthClass.addHealth(0,greaterAegisAmount);
                    action.actionPointReduction(greaterAegisCost);
                }
                else{
                    Debug.Log("Friendly character not close enough");
                }
            }
            else{
                Debug.Log("Select a friendly character");
            }
        }
    }



    public void healerButtonStates(){
        action.ButtonState(restoreLifeCost, restoreLifeButton);
        action.ButtonState(grandLightCost,grandLightButton);
        action.ButtonState(greaterAegisCost, greaterAegisButton);
    }
    
    public void selectThisAlly(){
        Entity.Combat.selectedAlly = typeof(Healer);
    }
    void Start(){
        Entity.setEntityActive(healerActive,this.gameObject);
    }

    void Update(){
        updateHealerText();
        healerButtonStates();
    }
}
