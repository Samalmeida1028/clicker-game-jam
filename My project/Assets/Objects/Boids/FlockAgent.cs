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
    public bool isClicked;

    public float attackSpeed = 0.4f;

    private float lastPulled;

    Rigidbody2D rb;

    private Camera camera;

    private Vector3 targetPos;
    private Vector3 currentGoalPosition;
    private Vector3 velocity;
    private Vector3 lastPos;

    private float maxSpeed = 4.0f;

    void Start()
    {
        agentCollider = GetComponent<Collider2D>();
        passed = false;

         GameObject FishingLine = GameObject.Find("Weight");
        if (FishingLine != null) {
            targetPos = FishingLine.transform.position;
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
        Vector3 pos = gameObject.transform.position;

        if (!isClicked) { return; }

        // Check if fish is within camera view
        Vector3 view = camera.WorldToViewportPoint(pos);

        // Pull force minus fish force add to fish velocity
        // velocity += 1;

        if (view.x > 1.1 || view.y > 1.1 || view.y < -0.1 || view.x < -0.1) {
            Debug.Log("escaping!");
            Escape();
        }

        // Get the direction towards the getting caught area
        Vector3 dirTowardsRod = (targetPos - pos).normalized;

        // Apply Velocity
        gameObject.transform.position += velocity * Time.deltaTime;

        // Apply Rotation
        transform.up = velocity;

        if (Time.fixedTime - lastPulled > attackSpeed) {
            velocity += -dirTowardsRod * 2.0f;
            lastPulled = Time.fixedTime;
        }
    }

    public void Pulled(Vector3 pulledTowardsPos, float amount) {
        Vector3 a = gameObject.transform.position;
        Vector3 b = (pulledTowardsPos - a).normalized;

        Vector3 pulledTowards = a + (amount * b);


        Vector3 dirTowardsGoal = (pulledTowards - transform.position).normalized;

        Vector3 dirTowardsRod = (targetPos - transform.position).normalized;

        currentGoalPosition = pulledTowards;

        velocity += (dirTowardsGoal * 1.6f);
  
        lastPos = transform.position;
    }  

    public void Caught() {
        lastPulled = Time.fixedTime;
    }

    public void Escape() {
        isClicked = false;

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
