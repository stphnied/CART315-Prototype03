using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public void Menu() {  
        SceneManager.LoadScene(0);  
    }  
    public void Gameplay() {  
        SceneManager.LoadScene(1);  
    }  

    public void Ending1() {  
        SceneManager.LoadScene(2);  
    }  

    public void Ending2() {  
        SceneManager.LoadScene(3);  
    }  

    public void Ending3() {  
        SceneManager.LoadScene(4);  
    }  
}
