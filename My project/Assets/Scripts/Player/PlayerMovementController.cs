using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{

    Rigidbody2D RB;
    float MoveDirectionY = 0;
    float MoveDirectionX = 0;
    public float PlayerSpeed = 5;
    public bool canMove;
    public bool isMoving;

    public Vector2 movement;
    void Start()
    {
        RB = gameObject.GetComponent<Rigidbody2D>();
        RB.freezeRotation = true;
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            RB.velocity = movement * PlayerSpeed;
        }

    }

    void Update()
    {
        if (canMove)
        {
            float xAxis = Input.GetAxisRaw("Horizontal");
            float yAxis = Input.GetAxisRaw("Vertical");

            //Check for player keyboard input
            checkInput(xAxis, yAxis);


            //Check if player is moving
            if (yAxis != 0 || xAxis != 0)
            {
                isMoving = true;
            }
            else
            {
                isMoving = false;
            }
        }



    }

    void checkInput(float xAxis, float yAxis)
    {
        // Normalized just makes the max vector length 1 so diagonal movement isnt faster than vertical or horizontal
        movement = new Vector2(xAxis, yAxis).normalized;
    }
}
