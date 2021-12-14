using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFollow : MonoBehaviour
{
    public GameControl control;
    public Transform projectile;
    public Transform farLeft;
    public Transform farRight;

    void Start(){
        TryFindProjectile();
        control = GetComponent<GameControl>();
    }

    void Update(){
        if(control.currentGame !=null){
            if(projectile)
                UpdateCamera();
            else if (control.currentGame.CheckMovementStopped()){
                if(ResetCamera()){
                    TryFindProjectile();
                }
            }
        }
    }

    void UpdateCamera(){
        Vector3 newPosition = transform.position;
        newPosition.x = projectile.position.x;
        newPosition.x = Mathf.Clamp(newPosition.x,farLeft.position.x,farRight.position.x);
        transform.position = newPosition;
    }

    void TryFindProjectile(){
        if(GameObject.FindGameObjectWithTag("Projectile")){
            projectile = GameObject.FindGameObjectWithTag("Projectile").transform;
        }
    }

    bool ResetCamera(){

        Vector3 newPosition = farLeft.position;
        if(transform.position.x!=farLeft.position.x){
            newPosition.x -= Time.deltaTime;
            newPosition.z = -10f;
            newPosition.y = 0;
            newPosition.x = Mathf.Clamp(newPosition.x,farLeft.position.x,farRight.position.x);
            transform.position = newPosition;
            return false;
        }

        return true;
    }
}
