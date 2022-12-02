using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public int sceneID;
    public bool loadSceneAdditive;

    public void sceneChange(){
        if(!loadSceneAdditive){
            SceneManager.LoadScene(this.sceneID);
        }
        else{
            SceneManager.LoadScene(this.sceneID,LoadSceneMode.Additive);
        }
    }

    public void quit(){
        Application.Quit();
    }
    
}
