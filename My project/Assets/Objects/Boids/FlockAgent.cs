using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class FlockAgent : MonoBehaviour
{
    public bool isFlipped;
    Flock agentFlock;
    public Flock AgentFlock {get{return agentFlock;}}
    Collider2D agentCollider;
    public Collider2D AgentCollider {get{return agentCollider;}}
    public int value;
    public int size;
    public int speed;

    public bool passed;
    public int maxPasses = 2;
    public int passnum = 0;
    public bool isHooked;

    public float attackSpeed = 0.4f;

    private float lastPulled;

    Rigidbody2D rb;

    private Camera camera;

    private Vector2 polePosition;
    private Vector3 currentGoalPosition;
    private Vector2 fishVelocity;
    private Vector2 fishForce;
    private Vector2 directionTowardsPole;
    private Vector2 targetAwayFromPole;
    private Vector2 directionTowardsTarget;

    private float fishStrength = 1.0f;
    private float maxSpeed = 4.0f;

    void Start()
    {
        agentCollider = GetComponent<Collider2D>();
        passed = false;

         GameObject FishingLine = GameObject.Find("Weight");
        if (FishingLine != null) {
            polePosition = FishingLine.transform.position;
            Debug.Log("Found");
        }

        camera = Camera.main;
    }
    public void Initialize(Flock flock){
        agentFlock = flock;
    }

    public void Move(Vector2 velocity){
        if (float.IsNaN(velocity.x) || float.IsNaN(velocity.y)) velocity = Vector2.zero;
        transform.up = velocity;
        transform.position += (Vector3)velocity*Time.fixedDeltaTime;
    }
    public void addPass(){
        passnum++;
    }
    public void Desty(){
        if(gameObject.transform.GetChild(0)!=null){
        GameObject.Destroy(gameObject.transform.GetChild(0).gameObject);
        }
        GameObject.Destroy(gameObject);
    }

    public void setValue(int min, int max){
        value = Random.Range(min,max);
        speed = value/2;
        size = value;
        gameObject.transform.localScale *=Mathf.Sqrt((float)(value))/Mathf.Sqrt((max));
    }

//+++++++++++++++FISHING BEHAVIOR++++++++++++++++++++++

     // Update is called once per frame
    void FixedUpdate() {
        // If the fish isnt caught then jus reutrn
        if (!isHooked) { return; }

        // Position of fish at this frame
        Vector2 fishCurrentPosition = gameObject.transform.position;

        // Check if fish is within camera view, if it isnt it escaped
        Vector2 view = camera.WorldToViewportPoint(fishCurrentPosition);
        if (view.x > 1.1 || view.y > 1.1 || view.y < -0.1 || view.x < -0.1) {
            Debug.Log("escaping!");
            Escape();
            return;
        }

        // Actual movement stuff

        // The opposite direction of the pole (Direction opposite of getting caught zone so fish swims away)
        fishForce = -directionTowardsPole * fishStrength;
        Debug.DrawLine(fishCurrentPosition, targetAwayFromPole, Color.red);
        // Apply Velocity
        gameObject.transform.position += (Vector3)fishVelocity * Time.deltaTime;
        
        if (lastPulled == null || Time.fixedTime - lastPulled > attackSpeed) {
            // Pull
            fishVelocity += fishForce;
            
            if (fishVelocity.magnitude > maxSpeed) {
                Vector2 maxVelocityVector = fishVelocity.normalized * maxSpeed;
                fishVelocity = maxVelocityVector;
            }

            lastPulled = Time.fixedTime;
        }
    }

    public void Pulled(Vector3 pulledTowardsPos, float onClickAmount) {
        Vector2 fishCurrentPosition = gameObject.transform.position;

        // Direction from fish towards rod
        Vector2 currentDirectionTowardsRod = (polePosition - fishCurrentPosition).normalized;
        Vector2 pullForce = currentDirectionTowardsRod * onClickAmount;

        // Apply pull force to fish
        fishVelocity += pullForce;
    }  

    public void Hook() {
        Vector2 fishCurrentPosition = gameObject.transform.position;

        // Gets the direction towards the pole in a nomralized vector ex Vector3(1, 0)
        directionTowardsPole = (polePosition - fishCurrentPosition).normalized;

        // Gets target outside of camera area (hard coded magnitude in, get radius around camera view after)
        targetAwayFromPole = -directionTowardsPole * (25);

        // Direction towards target
        directionTowardsTarget = (targetAwayFromPole - fishCurrentPosition).normalized;

        isHooked = true;
    }

    public void Escape() {
        isHooked = false;

        GameObject FishingLine = GameObject.Find("FishingLine");
        FishingLine.GetComponent<FishingLineController>().target = null;

        Debug.Log("Fish Escaped!");
    }

    public void Catch() {
        Debug.Log("Caught fish!");
        AgentFlock.removeAgent(this);
        Desty();
    }
}
