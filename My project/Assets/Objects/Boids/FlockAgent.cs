using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class FlockAgent : MonoBehaviour
{
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

    void Start()
    {
        agentCollider = GetComponent<Collider2D>();
        passed = false;
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
        gameObject.transform.localScale *=(float)(value)/(max);
    }


}
