using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBehavior : MonoBehaviour
{

    private Camera camera;

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

        camera = Camera.main;
    }

    void Update() {

    }

    // Update is called once per frame
    void FixedUpdate() {
        if (!this.GetComponent<FlockAgent>().isClicked) { return; }

        // Check if fish is within camera view
        Vector3 view = this.camera.WorldToViewportPoint(this.transform.position);
        if (view.x > 1.1 || view.y > 1.1 || view.y < -0.1 || view.x < -0.1) {
           this.Escape();
        }

        // Get the direction towards the getting caught area
        Vector3 dirTowardsRod = (this.targetPos - this.transform.position).normalized;

        // Apply Velocity
        this.transform.position += this.velocity * Time.deltaTime;

        // Apply Rotation
        this.transform.up = this.velocity;

        // If the fish has made it to the rod location(Where it is caught) catch it
        if ((this.targetPos - this.transform.position).magnitude < 1) {
            this.Catch();
            return;
        }

        // If the fish is moving towards a goal position, check when they reach that position then reset their velocity to fight
        if (currentGoalPosition != new Vector3(0, 0, 0)) {
            float magnitudeTowardsGoal = (this.currentGoalPosition - this.transform.position).magnitude;

            // decelerate as we approach our goal
            if (magnitudeTowardsGoal <= 0.5) {
                this.velocity -= (4 * dirTowardsRod) * Time.deltaTime; 
            }

            // If the fish gets to its goal target begin moving away again
            if (magnitudeTowardsGoal <= 0.1) { 
                //velocity = new Vector3(0, 0, 0);
                this.currentGoalPosition = new Vector3(0, 0, 0);
                return;
            }
        } else {
            if (velocity.magnitude >= maxSpeed) { return; }
            
            this.velocity += (10 * -dirTowardsRod) * Time.deltaTime;
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

    public void Escape() {
        this.GetComponent<FlockAgent>().isClicked = false;
        GameObject FishingLine = GameObject.Find("FishingLine");
        FishingLine.GetComponent<FishingLineController>().target = null;
        Debug.Log("Fish Escaped!");
    }

    public void Catch() {
        Debug.Log("Caught fish!");
        Destroy(gameObject);
    }
}
