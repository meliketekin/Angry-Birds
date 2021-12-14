using UnityEngine;
using UnityEngine.SceneManagement;
 
public class Control : MonoBehaviour
{
    public void NextScene()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void Exit(){
        Application.Quit();
    }

    public void Restart(){
        SceneManager.LoadScene("Main Menu");
    }
}