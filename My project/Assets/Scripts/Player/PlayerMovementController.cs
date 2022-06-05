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

    void Start(){
        RB = gameObject.GetComponent<Rigidbody2D>();
        RB.freezeRotation = true;
    }
    
    void FixedUpdate(){
        if (canMove)
        {
            if ((Input.GetKey(KeyCode.W)) || (Input.GetKey(KeyCode.S)))
            {
                Debug.Log("Step A");
                MoveDirectionY = Input.GetKey(KeyCode.W) ? 1 : -1;
                // Gobly_Animation.SetBool("isntStill", true);
            }
            else
            {
                MoveDirectionY = 0;
            }

            if ((Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.D)))
            {
                MoveDirectionX = Input.GetKey(KeyCode.D) ? 1 : -1;
                // Gobly_Animation.SetFloat("X Direction", MoveDirectionX);
                // Gobly_Animation.SetBool("isntStill", true);
            }
            else
            {
                MoveDirectionX = 0;
            }
            
            // Apply Movement
            float speed = PlayerSpeed;
            if (!(MoveDirectionX != 0 && MoveDirectionY != 0))
            {
                RB.AddForce(new Vector2((MoveDirectionX) * speed, (MoveDirectionY) * speed));
            }
            else
            {
                float DiagPlayerSpeed = Mathf.Sqrt(2 * (Mathf.Pow(speed, 2.0f)));
                RB.AddForce(new Vector2((MoveDirectionX) * DiagPlayerSpeed / 1.9f, (MoveDirectionY) * DiagPlayerSpeed / 1.9f));
            }
        }
    }
    
}
