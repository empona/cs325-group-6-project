using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public int sceneID;
    public int loadSceneMode;

    private void sceneChange(){
        if(loadSceneMode == 0){
            SceneManager.LoadScene(this.sceneID);
        }
        else{
            SceneManager.LoadScene(this.sceneID,LoadSceneMode.Additive);
        }
    }

    private void quit(){
        Application.Quit();
    }
    
}
