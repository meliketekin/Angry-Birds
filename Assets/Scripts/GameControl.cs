using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    [SerializeField]
    Game gamePrefab;

    public Game currentGame;

    void Start(){
        StartGame(0);
    }

    void Update(){
        if(currentGame!=null)
            CheckForCurrentGameOver();
    }

    void CheckForCurrentGameOver(){
        if(currentGame.currentLevel == null && currentGame.gameOver){
            SceneManager.LoadScene("Main Menu 1");
        }
    }

    public void StartGame(int level){
        currentGame = Instantiate(gamePrefab) as Game;
        currentGame.gameObject.name = "Game";
        currentGame.InitLevel(level, 3);
    }
}
