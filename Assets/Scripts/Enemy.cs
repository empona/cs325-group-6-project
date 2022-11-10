using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int enemyStartingHealth;
    public Entity.Health enemyHealth;
    public Entity.Action enemyActionPoints = new Entity.Action();

    public Entity.Combat.threatTable enemyTarget = new Entity.Combat.threatTable();
    
    //bool inCombat = false;

    // Start is called before the first frame update
    void Start()
    {
        enemyHealth = new Entity.Health(enemyStartingHealth); //Allows for starting health/max health to be edited in the editor
        Entity.Combat.enemyList[Entity.Combat.size] = this;
        Entity.Combat.size++;
    }

    // Update is called once per frame
    void Update()
    {
        if(Entity.Combat.selectedEnemy != null){
            Debug.Log(Entity.Combat.selectedEnemy.enemyHealth.getHealth());
            Debug.Log(Entity.Combat.selectedEnemy.enemyTarget.findPrimaryTarget());
        }
        
    }

    public void selectThisEnemy(){
        Entity.Combat.selectedEnemy = this.gameObject.GetComponent<Enemy>();
    }

}
