using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour{
  
    Button continueButton;
    public int classChangeIndex;
    public static bool[] classArray = {false,true,true,true,true};
    //Warrior Range Healer Mage Tank to be inferred by index.


    public void changeClass(){
        classArray[classChangeIndex] = !(classArray[classChangeIndex]);
    }

    public int numberOfSelectedClasses(){
        int selected = 0;
        foreach (var item in classArray){
            if(item == true){
                selected++;
            }
        }
        return selected;
    }

    public void buttonState(){
        int numberSelected = numberOfSelectedClasses();
        if(numberSelected == 3){
            continueButton.interactable = true;
        }
        else{
            continueButton.interactable = false;
        }
    }

    void Start(){
        continueButton = GameObject.Find("ContinueButton").GetComponent<Button>();
    }

    void Update(){
        buttonState();
    }

}