using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField]
    GameObject projectilePrefab;

    [SerializeField]
    GameObject[] levelPrefabs;

    public GameObject currentLevel;
    public int numProjectiles;
    public int targetsRemaining;
    public bool gameOver;

    private Transform start;
    private GameObject projectile;

    void Start(){
        gameOver = false;
        start = GameObject.Find("Game").transform.Find("Start");
        projectile = GameObject.FindGameObjectWithTag("Projectile");
    }

    void Update(){
        UpdateGameOver();
        UpdateProjectile();
    }

    void UpdateGameOver(){
        targetsRemaining = GameObject.FindGameObjectsWithTag("Target").Length;
        if(targetsRemaining==0 && currentLevel && !gameOver && !projectile){
            if(CheckMovementStopped())
            {
                StartCoroutine(WaitToDestroy(0.8f));
            }
        }
    }

    public IEnumerator WaitToDestroy(float t){
        yield return new WaitForSeconds(t);
        if(CheckMovementStopped()){
            gameOver = true;
            Debug.Log("No more target");
            Destroy(projectile.gameObject);
            Destroy(currentLevel.gameObject);
        } else{
            Debug.Log("Targets remain");
        }
    }

    public void InitLevel(int level, int numprojectiles){
        if(currentLevel == null && GameObject.FindGameObjectWithTag("Level") == null){
            if(!gameOver){
                numProjectiles = numprojectiles;
                currentLevel = Instantiate(levelPrefabs[level]) as GameObject;
                currentLevel.transform.SetParent(transform);
            }   
        }
    }

    void UpdateProjectile(){
        
        
            
        if(projectile == null && GameObject.FindGameObjectWithTag("Projectile")==null){
            if(!gameOver && targetsRemaining>0 &&currentLevel && numProjectiles>0){
            numProjectiles--;
            projectile = Instantiate(projectilePrefab,start.position,start.rotation) as GameObject;
        }

        }
        else if(numProjectiles == 0){
            StartCoroutine(WaitToDestroy(0));
        }
    }

    public bool CheckMovementStopped(){
        Rigidbody2D[] bodies = FindObjectsOfType<Rigidbody2D>();
        List<Rigidbody2D> checkBodies = new List<Rigidbody2D>();

        foreach(Rigidbody2D body in bodies){
            if(body.position.y > -8 && body.gameObject.tag == "Plank"){
                checkBodies.Add(body);
            }
        }
        int count = 0;
        int compare = checkBodies.Count;

        foreach(Rigidbody2D body in checkBodies){
            if(body.velocity.magnitude<= 0.025f){
                count++;
            }
        }
        if(count == compare)
            return true;

        return false;    
    }
}
