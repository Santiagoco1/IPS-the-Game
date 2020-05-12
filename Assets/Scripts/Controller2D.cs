using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (BoxCollider2D))]
public class Controller2D : MonoBehaviour
{
    public bool active=true;
    struct RayCastOrigin{
        public Vector2 topRight,topLeft,bottomRight,bottomLeft;
    }

    public struct Collisions{
        public bool above,below,right,left;
        public void reset(){
            above=below=right=left=false;
        }
    }

    BoxCollider2D boxCollider2d;
    RayCastOrigin rayCastOrigin;
    public Collisions collisions;

    public float skinWidth=0.15f;
    public int verticalRayCasts=4;
    public int horizontalRayCasts=4;   

    public LayerMask obstacles;

    float verticalRaySpacing;
    float horizontalRaySpacing;
    void Start()
    {
        boxCollider2d=GetComponent<BoxCollider2D>();
        calculateRaySpacing();
    }

    public void Move(Vector2 moveVector){
        getRayCastOrigins();
        collisions.reset();

        if(moveVector.x!=0) horizontalCollision(ref moveVector);
        if(moveVector.y!=0) verticalCollision(ref moveVector);

        transform.Translate(moveVector);
    }

    void horizontalCollision(ref Vector2 moveVector){
        float directionX = Mathf.Sign(moveVector.x);
        float rayLength = Mathf.Abs(moveVector.x) + skinWidth;

        Vector2 rayOrigin = (directionX<0) ? rayCastOrigin.bottomLeft : rayCastOrigin.bottomRight;
        for(int i=0;i<horizontalRayCasts;i++){
            RaycastHit2D hit= Physics2D.Raycast(rayOrigin,Vector2.right*directionX,rayLength,obstacles);
            Debug.DrawRay(rayOrigin,Vector2.right*+2*directionX,Color.green);
            rayOrigin += Vector2.up*(horizontalRaySpacing + moveVector.x);

            if(hit){
                moveVector.x=(hit.distance-skinWidth)*directionX;
                rayLength=hit.distance;

                collisions.right = directionX > 0;
                collisions.left = directionX < 0;
            }
        }
    }

    void verticalCollision(ref Vector2 moveVector){
        float directionY = Mathf.Sign(moveVector.y);
        float rayLength = Mathf.Abs(moveVector.y) + skinWidth;

        Vector2 rayOrigin = (directionY<0) ? rayCastOrigin.bottomLeft : rayCastOrigin.topLeft;
        for(int i=0;i<verticalRayCasts;i++){
            RaycastHit2D hit= Physics2D.Raycast(rayOrigin,Vector2.up*directionY,rayLength,obstacles);
            Debug.DrawRay(rayOrigin,Vector2.up*+2*directionY,Color.red);
            rayOrigin += Vector2.right*(verticalRaySpacing + moveVector.x);

            if(hit){
                moveVector.y=(hit.distance-skinWidth)*directionY;
                rayLength=hit.distance;

                collisions.above = directionY > 0;
                collisions.below = directionY < 0;
            }
        }
    }

    void getRayCastOrigins(){
        Bounds bound=boxCollider2d.bounds;
        bound.Expand(skinWidth * -2);

        rayCastOrigin.topLeft=new Vector2(bound.min.x,bound.max.y);
        rayCastOrigin.topRight=new Vector2(bound.max.x,bound.max.y);
        rayCastOrigin.bottomLeft=new Vector2(bound.min.x,bound.min.y);   
        rayCastOrigin.bottomRight=new Vector2(bound.max.x,bound.min.y);       
    }

    void calculateRaySpacing(){
        Bounds bound=boxCollider2d.bounds;
        bound.Expand(skinWidth * -2);

        verticalRayCasts=Mathf.Clamp(verticalRayCasts,2,int.MaxValue);
        horizontalRayCasts=Mathf.Clamp(horizontalRayCasts,2,int.MaxValue);

        verticalRaySpacing=bound.size.x/ (verticalRayCasts-1);
        horizontalRaySpacing=bound.size.y/ (horizontalRayCasts-1);
    }
}
