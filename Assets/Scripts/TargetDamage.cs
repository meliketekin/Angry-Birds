using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDamage : MonoBehaviour
{
    public int hitPoints = 2;
    public float damageImpactSpeed;
    public Sprite damagedSprite;

    private SpriteRenderer spriteRenderer;
    private int currentHitPoints;
    private float damageImpactSpeedSqr;

    void Start(){
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHitPoints = hitPoints;
        damageImpactSpeedSqr = damageImpactSpeed*damageImpactSpeed;
    }

    void OnCollisionEnter2D(Collision2D collision){
        if(collision.collider.tag != "Plank")
            return;


        if(collision.relativeVelocity.sqrMagnitude < damageImpactSpeedSqr){
            Debug.Log(collision.relativeVelocity.sqrMagnitude + " " + damageImpactSpeedSqr);
            spriteRenderer.sprite = damagedSprite;
            return;
        }

        if(spriteRenderer){
            spriteRenderer.sprite = damagedSprite;
            currentHitPoints--;
            if(currentHitPoints<=0)
                Kill();
        }
        
    }

    void Kill(){
        spriteRenderer.enabled = false;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().isKinematic = true;
        Destroy(this.gameObject, 0.5f);
        GetComponent<ParticleSystem>().Play();
    }
}
