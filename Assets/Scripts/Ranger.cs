using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Ranger : MonoBehaviour{

    bool rangerActive = CharacterSelect.classArray[1];
    public TMP_Text rangerText;

    public Button simpleShotButton;
    int simpleShotCost = 1; 
    int simpleShotAmmoCost = 1;
    int simpleShotDamage = 50;


    public Button trueShotButton;
    int trueShotCost = 6;
    int trueShotAmmoCost = 4;
    int trueShotDamage = 250;

    public Button rapidFireButton;
    int rapidFireCost = 9;

    int rapidFireAmmoCost = 5; //Damage corresponds to shots fired, this is the minimum ammo cost. The ability will shoot 5<=x<=12 shots.

    int rapidFireDamagePerBullet = 60;

    public Button reloadButton;
    
    int spellRange = 5000; 
    
    //Note that Ranger doesn't have any AOE, the idea is that the mage specializes in AOE (will fix up better spells later), Ranger focuses on a single target, and melee is a mix of both.

    public class Ammo{ //Goal is to have the player reload every 3 rounds?
        int maxAmmoCapacity;
        int currentAmmoCapacity;

        public Ammo(){
            maxAmmoCapacity = 30;
            currentAmmoCapacity = 30;
        }

        public int getCurrentAmmo(){
            return currentAmmoCapacity;
        }

        public int reduceAmmo(int reduction){
            currentAmmoCapacity-=reduction;
            return currentAmmoCapacity;
        }

        public void reload(){
            currentAmmoCapacity = maxAmmoCapacity;
        }

        public bool shotsAvailable(int quantity){
            return (currentAmmoCapacity>=quantity);
        }
    }
    public static Entity.Health health = new Entity.Health(100);
    public Entity.Action action = new Entity.Action();
    public Entity.Movement movement = new Entity.Movement(-1);
    Ammo rangerAmmo = new Ammo();
    
    public void updateRangerText(){
        rangerText.text=$"Ranger Health: {health.getHealth()+health.getOverHealth()}\r\nRanger Action Points: {action.getActionPoints()}\nRanger's Ammo: {rangerAmmo.getCurrentAmmo()}";
    }

    public void rangerButtonStates(){
        action.ButtonState(simpleShotCost, simpleShotButton, simpleShotAmmoCost, rangerAmmo.getCurrentAmmo());
        action.ButtonState(trueShotCost,trueShotButton, trueShotAmmoCost, rangerAmmo.getCurrentAmmo());
        action.ButtonState(rapidFireCost, rapidFireButton, rapidFireAmmoCost, rangerAmmo.getCurrentAmmo());
        action.ButtonState(10,reloadButton);
    }

    public void castSimpleShot(){
        if(action.actionUsable(simpleShotCost) && rangerAmmo.shotsAvailable(simpleShotAmmoCost)){
            if(Entity.Combat.selectedEnemy != null){
                if(Entity.Combat.isEnemyInRange(Entity.Combat.selectedEnemy,this.gameObject.transform.position.x,this.gameObject.transform.position.y, spellRange)){
                    Entity.Combat.selectedEnemy.enemyHealth.reduceHealth(simpleShotDamage);
                    Entity.Combat.selectedEnemy.enemyTarget.generateThreat(typeof(Ranger),simpleShotDamage);
                    action.actionPointReduction(simpleShotCost);
                    rangerAmmo.reduceAmmo(simpleShotAmmoCost);
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

    public void castTrueShot(){
        if(action.actionUsable(trueShotCost) && rangerAmmo.shotsAvailable(trueShotAmmoCost)){
            if(Entity.Combat.selectedEnemy != null){
                if(Entity.Combat.isEnemyInRange(Entity.Combat.selectedEnemy,this.gameObject.transform.position.x,this.gameObject.transform.position.y, spellRange)){
                    Entity.Combat.selectedEnemy.enemyHealth.reduceHealth(trueShotDamage);
                    Entity.Combat.selectedEnemy.enemyTarget.generateThreat(typeof(Ranger),trueShotDamage);
                    action.actionPointReduction(trueShotAmmoCost);
                    rangerAmmo.reduceAmmo(trueShotAmmoCost);
                    
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

    public void castRapidFire(){
        if(action.actionUsable(rapidFireCost) && rangerAmmo.shotsAvailable(rapidFireAmmoCost)){
            if(Entity.Combat.selectedEnemy != null){
                if(Entity.Combat.isEnemyInRange(Entity.Combat.selectedEnemy,this.gameObject.transform.position.x,this.gameObject.transform.position.y, spellRange)){
                    int maxBullets = (rangerAmmo.getCurrentAmmo() >12? 12 : rangerAmmo.getCurrentAmmo());
                    int bulletsUsed = Entity.randomGenerator.Next(rapidFireAmmoCost,maxBullets);
                    Entity.Combat.selectedEnemy.enemyHealth.reduceHealth(rapidFireDamagePerBullet*bulletsUsed);
                    Entity.Combat.selectedEnemy.enemyTarget.generateThreat(typeof(Ranger),rapidFireDamagePerBullet*bulletsUsed);
                    action.actionPointReduction(rapidFireCost);
                    rangerAmmo.reduceAmmo(bulletsUsed);
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

    public void reload(){
        if(action.actionUsable(10) ){
            rangerAmmo.reload();
            action.actionPointReduction(10);
        }
        else{
            //Display a error message to center screen so people know what they are doing wrong?
            Debug.Log("Reloading requires a full turn.");
        }
    }
    void Start(){
        Entity.setEntityActive(rangerActive,this.gameObject);
    }

    void Update(){
        rangerButtonStates();
        updateRangerText();    
    }

    public void selectThisAlly(){
        Entity.Combat.selectedAlly = typeof(Ranger);
    }
}
