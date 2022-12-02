using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : Entity{
    public class ThreatTable{
        List<KeyValuePair<int,int>> table = new List<KeyValuePair<int,int>>(3); //Only 3 playable characters
        public ThreatTable(){
            for (int i = 0; i < CharacterSelect.classArray.Length; i++){ //First target is usually the last element to be added.
                if(CharacterSelect.classArray[i] == true){
                    KeyValuePair<int, int> threatPair;
                    threatPair = new KeyValuePair<int, int>(i,0);
                    table.Add(threatPair);
                }
            }
        }

        public void generateThreat(int playableCharacter, int amountOfThreat){
            if(playableCharacter == 4){ //Tank
                amountOfThreat = amountOfThreat*4;
            }
            if(playableCharacter == 2){ //Healer
                amountOfThreat = amountOfThreat *2;
            }

            for (int i = 0; i < table.Count; i++){
                KeyValuePair<int, int> item = table[i];
                if(playableCharacter == item.Key){
                    amountOfThreat = amountOfThreat + item.Value;
                    var threatPair = new KeyValuePair<int, int>(playableCharacter,amountOfThreat);
                    table.Remove(item);
                    table.Add(threatPair);
                    break;
                }
            }
        }

        public KeyValuePair<int, int> findPrimaryTarget(){
            KeyValuePair<int, int> topThreat = table[0];
            for (int i = 0; i < table.Count; i++){
                KeyValuePair<int, int> item = table[i];
                if(item.Value >= topThreat.Value){
                    topThreat = item;
                }
            }
            return topThreat;
        }

        public void placeAtTop(int playableCharacter){ //To be used by tank's taunt.
            KeyValuePair<int, int> topThreat = findPrimaryTarget();
            for (int i = 0; i < table.Count; i++){
                KeyValuePair<int, int> item = table[i];
                if(item.Key == playableCharacter){
                    table.Remove(item);
                    break;
                }
            }
            int newThreat = (topThreat.Value>200)? (int) ((double)topThreat.Value * 1.25) : topThreat.Value+250; //In the case where taunt is the first ability casted on the target.
            var threatPair = new KeyValuePair<int, int>(playableCharacter,newThreat);
            table.Add(threatPair);
        }

        
    }
}
