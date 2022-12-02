using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health{
    int maxHealth;
    int currentHealth;

    int overHealth;
    int maxOverHealth = 120;
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
        isDead = (currentHealth <= 0);
        return isDead;
    }

    public int reduceHealth(int reduction, int blockValue = 0){ 
        reduction = (int) ((1-( (float) blockValue/5)) * reduction); //1 Block Value = 20% off Reduction.
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
        if(overHealth > maxOverHealth){
            overHealth = maxOverHealth;
        }
        return currentHealth;
    }

    public int getTotalHealth(){
        if(overHealth > 0){
            return currentHealth+overHealth;
        }
        else{
            return currentHealth;
        }
    }
}