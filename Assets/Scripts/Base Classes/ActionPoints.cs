using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPoints{

    public int actionPoints;
    public int maxActionPoint;
    
    public ActionPoints(int maxActionPoint = 10){
        actionPoints = maxActionPoint;
        this.maxActionPoint = maxActionPoint;
    }
    
    public int getActionPoints(){
        return actionPoints;
    }

    public bool actionUsable(int actionCost){
        return actionPoints>=actionCost;
    }

    public void actionPointReduction(int actionCost){
        actionPoints= actionPoints-actionCost;
    }
    
    public void resetPoints(){
        actionPoints = maxActionPoint; 
    }
}
