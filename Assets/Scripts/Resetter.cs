using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resetter : MonoBehaviour
{
    public Rigidbody2D projectile;
    private SpringJoint2D spring;
    public float resetSpeed= 0.025f;
    private float resetSpeedSqr;

    void Start(){
        resetSpeedSqr = resetSpeed*resetSpeed;
    }

    void Update(){
        if(projectile){
            if(!spring)
                spring = projectile.GetComponent<SpringJoint2D>();
            CheckForReset();
        }else{
            TryFindProjectile();
        }
    }

    void TryFindProjectile(){
        if(GameObject.FindGameObjectWithTag("Projectile")){
            projectile = GameObject.FindGameObjectWithTag("Projectile").GetComponent<Rigidbody2D>();
            spring = projectile.GetComponent<SpringJoint2D>();
        }
    }

    void CheckForReset(){
        if(Input.GetKeyDown("r"))
            Reset();

        if(projectile.velocity.sqrMagnitude < resetSpeedSqr && spring == null){
            Reset();
        }
    
    }
    void OnTriggerExit2D(Collider2D other){
        
        if(projectile && other.GetComponent<Rigidbody2D>() == projectile){
            Reset();
        }
    }

    void Reset(){
        Destroy(projectile.gameObject);
    }
}
