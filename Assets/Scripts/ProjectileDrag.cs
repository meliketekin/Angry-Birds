using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProjectileDrag : MonoBehaviour
{
    private bool isClicked;

    public LineRenderer catapultLineFront;
    public LineRenderer catapultLineBack;

    private SpringJoint2D spring;
    private Vector2 prevVelocity;

    private Ray leftCatapultToProjectile;
    private float circleRadius;
    private Transform catapult;
    private Ray rayToMouse;

    void Start(){
        Init();
    }

    void Init(){
        spring = GetComponent<SpringJoint2D>();
        catapult = GameObject.Find("Game").transform.Find("Catapult_Back");
        catapultLineBack = catapult.GetComponent<LineRenderer>();
        catapultLineFront = catapult.GetChild(0).GetComponent<LineRenderer>();
        spring.connectedBody = catapult.GetComponent<Rigidbody2D>();
        LineRendererSetup();
        rayToMouse = new Ray(catapult.position, Vector3.zero);
        leftCatapultToProjectile = new Ray(catapultLineFront.transform.position,Vector3.zero);
        CircleCollider2D circle = GetComponent<CircleCollider2D>();
        circleRadius = circle.radius;
    }

   void OnMouseDown(){
       spring.enabled = false;
       isClicked = true;
    }  

    void OnMouseUp(){
        spring.enabled = true;
        GetComponent<Rigidbody2D>().isKinematic = false;
        isClicked = false;
    }

    void Update(){
        UpdateProjectile();
    }

    void UpdateProjectile(){
        

        if(isClicked){
            Dragging();
        }

        if(spring!=null){
            if(!GetComponent<Rigidbody2D>().isKinematic && 
        prevVelocity.sqrMagnitude > GetComponent<Rigidbody2D>().velocity.sqrMagnitude){
            Destroy(spring);
            GetComponent<Rigidbody2D>().velocity = prevVelocity;
        }
        if(!isClicked)
            prevVelocity = GetComponent<Rigidbody2D>().velocity;

        LineRendererUpdate();

        }else{
            catapultLineFront.enabled = false;
            catapultLineBack.enabled = false;
        }
     

        

        
    }

    void Dragging(){
        Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 catapultToMouse = mouseWorldPoint-catapult.position;
        if(catapultToMouse.sqrMagnitude>4.0f){
            rayToMouse.direction= catapultToMouse;
            mouseWorldPoint = rayToMouse.GetPoint(2.0f);
        }
        mouseWorldPoint.z = 0f;
        transform.position = mouseWorldPoint;
    }

    void LineRendererSetup(){
        catapultLineFront.SetPosition(0, catapultLineFront.transform.position);
        catapultLineBack.SetPosition(0, catapultLineBack.transform.position);

        catapultLineFront.sortingLayerName = "Foreground";
        catapultLineBack.sortingLayerName = "Foreground";

        catapultLineBack.enabled = true;
        catapultLineFront.enabled = true;

        catapultLineFront.sortingOrder = 3;
        catapultLineBack.sortingOrder = 1;
    }

    void LineRendererUpdate(){
        Vector2 catapultToProjectile = transform.position-catapultLineFront.transform.position;
        leftCatapultToProjectile.direction = catapultToProjectile;
        Vector3 holdPoint = leftCatapultToProjectile.GetPoint(catapultToProjectile.magnitude+ circleRadius);
        catapultLineFront.SetPosition(1, holdPoint);
        catapultLineBack.SetPosition(1, holdPoint);
    }

}
