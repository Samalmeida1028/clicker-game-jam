using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBehavior : MonoBehaviour
{
    private Vector3 targetPos;
    private Vector3 currentGoalPosition;
    private Vector3 velocity;
    private Vector3 lastPos;

    private float maxSpeed = 4.0f;
    

    [SerializeField]
    public FishScriptableObject fishType;

    // Start is called before the first frame update
    void Start()
    {
        GameObject FishingLine = GameObject.Find("Weight");
        if (FishingLine != null) {
            this.targetPos = FishingLine.transform.position;
            Debug.Log("Found");
        }
    }

    // Update is called once per frame
    void Update() {
        if (!this.GetComponent<FlockAgent>().isClicked) { return; }

        // Get the direction towards the getting caught area
        Vector3 dirTowardsRod = (this.targetPos - this.transform.position).normalized;
        
        //
        transform.position += velocity * Time.deltaTime;

        // If the fish has made it to the rod location(Where it is caught) catch it
        if ((this.targetPos - this.transform.position).magnitude < 1) {
            this.Catch();
            return;
        }

        // If the fish is moving towards a goal position, check when they reach that position then reset their velocity to fight
        if (currentGoalPosition != new Vector3(0, 0, 0)) {
            float magnitudeTowardsGoal = (this.currentGoalPosition - this.transform.position).magnitude;

            if (magnitudeTowardsGoal <= 0.5) {
                velocity -= (4 * dirTowardsRod) * Time.deltaTime; 
            }

            if (magnitudeTowardsGoal <= 0.1) { // If the fish gets to its goal target begin moving away again
                //velocity = new Vector3(0, 0, 0);
                currentGoalPosition = new Vector3(0, 0, 0);
                return;
            }
        } else {
            if (velocity.magnitude >= maxSpeed) { return; }
            
            velocity += (10 * -dirTowardsRod) * Time.deltaTime;
        }
    }

    public void Pulled(Vector3 pulledTowardsPos, float amount) {
        Vector3 a = this.transform.position;
        Vector3 b = (pulledTowardsPos - a).normalized;

        Vector3 pulledTowards = a + (amount * b);

        Vector3 dirTowardsGoal = (pulledTowards - this.transform.position).normalized;

        this.currentGoalPosition = pulledTowards;
        this.velocity = dirTowardsGoal * 2.0f; // Rod Pull Amount with force
        this.lastPos = this.transform.position;
    }   


    public void Catch() {
        Debug.Log("Caught fish!");
        Destroy(gameObject);
    }
}
