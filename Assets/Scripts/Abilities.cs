using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities{
    public static Hashtable allAbilities = new Hashtable();
    public castable ability;
    public struct castable{
        public int cost; //Action point cost
        public int amount; // The amount of healer / damage the ability does
        public int range; // The range the ability can be casted from
        public string name; // Name of the ability to be used for GUI
        public string description; // Description of the ability to be used for GUI
        public castable(int cost, int amount, int range, string name, string description){
            this.cost = cost;
            this.amount = amount;
            this.range = range;
            this.name = name;
            this.description = description;
        }
    }
    
    public Abilities(int cost, int amount, int range, string name, string description){
        ability = new castable(cost,amount,range,name,description);
        allAbilities.Add(name,ability);
    }
    
    public bool abilityUsable(Entity target, Entity caster){
        if(target != null){
            if(Entity.isEntityInRange(target,caster.gameObject.transform.position,ability.range)){
                if(caster.entityAction.actionUsable(ability.cost)){
                    return true;
                }
                else{
                    Debug.Log("Not Enough Action Points");
                }
            }
            else{
                Debug.Log("Entity not in range");
            }
        }
        else{
            Debug.Log("Invalid Target");
        }
        return false;
    }
    

    public void castDamageSpell(Entity target, Entity caster){
        if(abilityUsable(target,caster)){
            target.entityHealth.reduceHealth(ability.amount);
            caster.entityAction.actionPointReduction(ability.cost);
            target.enemyThreat.generateThreat((int) caster.characterClass,ability.amount);
        }
    }

    public void castDamageAOESpell(Entity target, Entity caster){
        if(abilityUsable(target,caster)){
            foreach(Entity item in Entity.entitiesNearEntity(target,Select.enemyList,ability.range)){
                item.entityHealth.reduceHealth(ability.amount);
                item.enemyThreat.generateThreat((int) caster.characterClass,ability.amount);
            }
            caster.entityAction.actionPointReduction(ability.cost);
        }
    }

    public void castHealingSpell(Entity target, Entity caster){
        if(abilityUsable(target,caster)){
            target.entityHealth.addHealth(ability.amount,0);
            caster.entityAction.actionPointReduction(ability.cost);
            foreach(Entity item in Entity.entitiesNearEntity(caster,Select.enemyList,ability.range)){
                item.enemyThreat.generateThreat((int) caster.characterClass,ability.amount);
            }
        }
    }

    public void castOverhealingSpell(Entity target, Entity caster){
        if(abilityUsable(target,caster)){
            target.entityHealth.addHealth(0,ability.amount);
            caster.entityAction.actionPointReduction(ability.cost);
            foreach(Entity item in Entity.entitiesNearEntity(caster,Select.enemyList,ability.range)){
                item.enemyThreat.generateThreat((int) caster.characterClass,ability.amount);
            }
        }
    }

    public void castHealingAOESpell(Entity target, Entity caster){
        if(abilityUsable(target,caster)){
            foreach(Entity item in Entity.entitiesNearEntity(target,Select.allyList,ability.range)){
                item.entityHealth.addHealth(ability.amount,0);
            }
            foreach(Entity item in Entity.entitiesNearEntity(caster,Select.enemyList,ability.range)){
                item.enemyThreat.generateThreat((int) caster.characterClass,ability.amount);
            }
            caster.entityAction.actionPointReduction(ability.cost);
        }
    }
    
    public void castTaunt(Entity target, Entity caster){
        if(abilityUsable(target,caster)){
            target.enemyThreat.placeAtTop((int) caster.characterClass);
        }
    }
    
}