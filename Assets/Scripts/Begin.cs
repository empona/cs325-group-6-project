using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Begin : MonoBehaviour
{
    [SerializeField] AudioSource clickSound;
    void Start()
    {

    }
    void Update()
    {

    }
    public void beginLevel()
    {
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        clickSound.Play();
        SceneManager.LoadScene(1);
    }
}
