using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOverAbility : MonoBehaviour
{
    public string abilityName;
    public GameObject toolTip;
    public static Abilities.castable currentAbility;

    void Start(){
        currentAbility = (Abilities.castable) Abilities.allAbilities["Null"];
    }
    
    
    public void turnOffToolTip(){
        currentAbility = (Abilities.castable) Abilities.allAbilities["Null"];
        toolTip.SetActive(false);
    }

    public void turnOnToolTip(){
        currentAbility = (Abilities.castable) Abilities.allAbilities[abilityName];
        toolTip.SetActive(true);
    }
}
