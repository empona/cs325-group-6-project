using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//I realized that classes/Enemies truely are just instances of Entity.
public class Entity :MonoBehaviour{
    public enum classTypes
    {
        Warrior, Ranger, Healer, Mage, Tank, Null
    }


    public classTypes characterClass;
    public int maxHealth;
    public Health entityHealth;
    public ActionPoints entityAction = new ActionPoints();
    public Movement entityMovement = new Movement(-1);

    public Enemy.ThreatTable enemyThreat = null;
    void Start(){
        entityHealth = new Health(maxHealth);
        if(characterClass != classTypes.Null){
            this.gameObject.SetActive(CharacterSelect.classArray[(int) characterClass]);
            Select.allyList.Add(this);
        }
        else{
            Select.enemyList.Add(this);
            enemyThreat = new Enemy.ThreatTable();
        }
        
    }
    

    public static bool isEntityInRange(Entity entity, Vector3 casterPosition, int range){
        float minX = (entity.gameObject.transform.position.x)-range; 
        float maxX = (entity.gameObject.transform.position.x)+range; 
        float minY = (entity.gameObject.transform.position.y)-range; 
        float maxY = (entity.gameObject.transform.position.y)+range;
        
        if(casterPosition.x <= maxX && casterPosition.x >= minX){
            if(casterPosition.y <= maxY && casterPosition.y >= minY){
                return true;
            }
        }
        return false;
    }

    public static List<Entity> entitiesNearEntity(Entity entity, List<Entity> entities, int range){
        List<Entity> entitiesNear = new List<Entity>();
        foreach(var item in entities){
            if(isEntityInRange(item,entity.transform.position,range)){
                entitiesNear.Add(item);
            }
        }
        return entitiesNear;
    }

    public void selectEntity(){
        Select.selectedEntity = this.gameObject.GetComponent<Entity>();
    }
}