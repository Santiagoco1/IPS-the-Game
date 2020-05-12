using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Controller2D))]
public class CubitoBehaviour : MonoBehaviour
{
    public float speed;
    public Transform player;
    Controller2D controller;
    Vector2 moveVector;
    float gravity=-20f;
    float direction;
    float dampAcceleration=.2f;
    float xDisplacementSmoothing;

    void Start()
    {
        controller=GetComponent<Controller2D>();
    }
    void Update()
    {
        direction=Mathf.Sign(player.position.x-transform.position.x);
        float xDisplacement= direction * speed;

        if(controller.collisions.above || controller.collisions.below){
            moveVector.y=0;
        }

        moveVector.x = Mathf.SmoothDamp(moveVector.x,xDisplacement,ref xDisplacementSmoothing,dampAcceleration);
        moveVector.y+=gravity*Time.deltaTime;
        controller.Move(moveVector);
    }
}
