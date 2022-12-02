using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tooltip : MonoBehaviour
{
    public TMP_Text abilityName;
    public TMP_Text abilityRange;
    public TMP_Text abilityCost;
    public TMP_Text abilityDescription;

    Camera screen;

    static Vector3 mouseScreenPosition;
    static Vector3 mouseCursorPosition;

    // Start is called before the first frame update
    void Start()
    {
      this.gameObject.SetActive(false);
      screen = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        mouseCursorPosition = Input.mousePosition;
        
        mouseScreenPosition = screen.ScreenToWorldPoint(new Vector3(mouseCursorPosition.x,mouseCursorPosition.y,1));
        if(mouseCursorPosition.x < Screen.width/2.0f){
            this.gameObject.transform.position = new Vector3(mouseScreenPosition.x+250,mouseScreenPosition.y+125,1);
        }
        else{
            this.gameObject.transform.position = new Vector3(mouseScreenPosition.x-250,mouseScreenPosition.y+125,1);
        }
        if(!MouseOverAbility.currentAbility.Equals(Abilities.allAbilities["Null"] ) ) {
            abilityName.text = MouseOverAbility.currentAbility.name;
            abilityRange.text = $"Range: {MouseOverAbility.currentAbility.range} tiles";
            abilityCost.text = $"{MouseOverAbility.currentAbility.cost} Action Points";
            abilityDescription.text = MouseOverAbility.currentAbility.description;
        }


    }
}
