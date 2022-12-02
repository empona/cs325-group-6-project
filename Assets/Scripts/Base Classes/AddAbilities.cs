using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddAbilities : MonoBehaviour
{
    void Start(){

        //Cost Amount Range Name Description

        int spellRange = 5;
        int meleeRange = 1;
    
        //Warrior Abilities
        int firstWarriorAbilityCost = 1;
        int firstWarriorAbilityDamage = 20;
        string firstWarriorAbilityName = "Single Slash";
        string firstWarriorAbilityDescription = $"Slash the target a single time, producing {firstWarriorAbilityDamage} damage to the target.";
        
        int secondWarriorAbilityCost = 3;
        int secondWarriorAbilityDamage = 100;
        string secondWarriorAbilityName = "Strong Slash";
        string secondWarriorAbilityDescription = $"Viciously slash the target, yielding {secondWarriorAbilityDamage} damage to the target.";
        
        int thirdWarriorAbilityCost = 7;
        int thirdWarriorAbilityDamage = 50;
        string thirdWarriorAbilityName = "Whirlwind";
        string thirdWarriorAbilityDescription = $"Slash in a spinning motion, damaging all nearby enemies for {thirdWarriorAbilityDamage} damage.";

        new Abilities(firstWarriorAbilityCost, firstWarriorAbilityDamage, meleeRange, firstWarriorAbilityName, firstWarriorAbilityDescription);
        new Abilities(secondWarriorAbilityCost,secondWarriorAbilityDamage,meleeRange, secondWarriorAbilityName,secondWarriorAbilityDescription);
        new Abilities(thirdWarriorAbilityCost,thirdWarriorAbilityDamage,meleeRange * 3, thirdWarriorAbilityName,thirdWarriorAbilityDescription);


        //Ranger Abilities
        int firstRangerAbilityCost = 1;
        int firstRangerAbilityDamage = 20;
        string firstRangerAbilityName = "Fire Shot";
        string firstRangerAbilityDescription = $"Fire your bow a once, causing {firstRangerAbilityDamage} damage to the target.";
        
        int secondRangerAbilityCost = 6;
        int secondRangerAbilityDamage = 100;
        string secondRangerAbilityName = "True Shot";
        string secondRangerAbilityDescription = $"Precisely fire by lining up the shot, deals {secondRangerAbilityDamage} damage to the target.";
        
        int thirdRangerAbilityCost = 9;
        int thirdRangerAbilityDamage = 50;
        string thirdRangerAbilityName = "Rapid Fire";
        string thirdRangerAbilityDescription = $"Rapidly fire in all directions, firing between 6-12 shots where one shot damages an enemy for {thirdRangerAbilityDamage} damage.";

        new Abilities(firstRangerAbilityCost,firstRangerAbilityDamage,spellRange,firstRangerAbilityName,firstRangerAbilityDescription);
        new Abilities(secondRangerAbilityCost,secondRangerAbilityDamage,spellRange, secondRangerAbilityName,secondRangerAbilityDescription);
        new Abilities(thirdRangerAbilityCost,thirdRangerAbilityDamage,spellRange, thirdRangerAbilityName,thirdRangerAbilityDescription);

        //Healer Abilities
        int firstHealerAbilityCost = 2;
        int firstHealerAbilityHealing = 20;
        string firstHealerAbilityName = "Restore Life";
        string firstHealerAbilityDescription = $"Restore life to your friendly ally, the ally gains {firstHealerAbilityHealing} health. ";
        
        int secondHealerAbilityCost = 6;
        int secondHealerAbilityHealing = 30;
        string secondHealerAbilityName = "Grand Light";
        string secondHealerAbilityDescription = $"Heal all friendly allies in range for {secondHealerAbilityHealing} health.";
        
        int thirdHealerAbilityCost = 10;
        int thirdHealerAbilityHealing = 80;
        string thirdHealerAbilityName = "Greater Aegis";
        string thirdHealerAbilityDescription = $"Give your friendly ally a protective bubble with {thirdHealerAbilityHealing} points. This bubble can not go above 120 points of health.";

        new Abilities(firstHealerAbilityCost,firstHealerAbilityHealing,spellRange,firstHealerAbilityName,firstHealerAbilityDescription);
        new Abilities(secondHealerAbilityCost,secondHealerAbilityHealing,spellRange, secondHealerAbilityName,secondHealerAbilityDescription);
        new Abilities(thirdHealerAbilityCost,thirdHealerAbilityHealing,spellRange, thirdHealerAbilityName,thirdHealerAbilityDescription);

        //Mage Abilities
        int firstMageAbilityCost = 2;
        int firstMageAbilityDamage = 50;
        string firstMageAbilityName = "Cast Doubt";
        string firstMageAbilityDescription = $"Cast doubt on your enemies and dealing {firstMageAbilityDamage} health to them.";
        
        int secondMageAbilityCost = 8;
        int secondMageAbilityDamage = 250;
        string secondMageAbilityName = "Shadowbolt";
        string secondMageAbilityDescription = $"Cast a bolt of shadow to your enemy dealing {secondMageAbilityDamage} health.";
        
        int thirdMageAbilityCost = 10;
        int thirdMageAbilityDamage = 150;
        string thirdMageAbilityName = "Drain Life";
        string thirdMageAbilityDescription = $"Drain life out of all your nearby enemies, dealing {thirdMageAbilityDamage} to each enemy.";

        new Abilities(firstMageAbilityCost,firstMageAbilityDamage,spellRange,firstMageAbilityName,firstMageAbilityDescription);
        new Abilities(secondMageAbilityCost,secondMageAbilityDamage,spellRange, secondMageAbilityName,secondMageAbilityDescription);
        new Abilities(thirdMageAbilityCost,thirdMageAbilityDamage,spellRange, thirdMageAbilityName,thirdMageAbilityDescription);

        //Tank Abilities
        
        int firstTankAbilityCost = 2;
        int firstTankAbilityDamage = 50;
        string firstTankAbilityName = "Distract";
        string firstTankAbilityDescription = $"Distract the enemy, making them more likely to attack you instead. Additionally, damage them for {firstTankAbilityDamage} health.";
        
        int secondTankAbilityCost = 3;
        int secondTankAbilityDamage = 0;
        string secondTankAbilityName = "Shield Block";
        string secondTankAbilityDescription = $"Ready your shield for an attack, reducing the amount of damage you take by 20%, stackable.";
        
        int thirdTankAbilityCost = 10;
        int thirdTankAbilityDamage = 100;
        string thirdTankAbilityName = "Taunt";
        string thirdTankAbilityDescription = $"Taunt the enemy causing them to attack you on their next turn and deal {thirdTankAbilityDamage} damage to them.";

        new Abilities(firstTankAbilityCost,firstTankAbilityDamage,meleeRange,firstTankAbilityName,firstTankAbilityDescription);
        new Abilities(secondTankAbilityCost,secondTankAbilityDamage,meleeRange, secondTankAbilityName,secondTankAbilityDescription);
        new Abilities(thirdTankAbilityCost,thirdTankAbilityDamage,spellRange, thirdTankAbilityName,thirdTankAbilityDescription);


        //Null
        new Abilities(0,0,0,"Null","Null"); //To be used to null.
    }
}
