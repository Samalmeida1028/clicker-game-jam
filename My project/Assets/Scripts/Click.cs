using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour
{
    public Camera Camera;
    private Transform currentFish;
    public float radius;
    private float cps = 1.0f;

    private bool IsFish(Collider2D hitObject) {
        // Check if the object hit is a fish
        if (hitObject.transform == null) { return false; }
        if (hitObject.transform.GetComponent<FlockAgent>() == null) { return false; }

        return true;
    }

    void Update() {   
        // Begin checking for mouse clicks on fishies
        if(Input.GetMouseButtonDown(0) && !currentFish) {
            Vector3 mousePos = Camera.ScreenToWorldPoint( Input.mousePosition ); // Create a ray from the camera to the mouse pos
            Collider2D hit = Physics2D.OverlapCircle(mousePos, radius); // Look at this later but casts the ray
            
            if (hit!= null && IsFish(hit)) {
                // We hit a fish
                if (currentFish != null) {
                    this.GetComponent<FishingLineController>().target = null;
                }

                Transform fish = hit.transform;

                if (currentFish == fish) { currentFish = null; return; }
                
                fish.GetComponent<FlockAgent>().Hook();

                this.GetComponent<FishingLineController>().target = fish;

                currentFish = fish;
            }
        } else if(Input.GetMouseButtonDown(0) && currentFish) {
            FlockAgent fish = currentFish.GetComponent<FlockAgent>();

            fish.Pulled(this.GetComponent<FishingLineController>().origin.position, cps);
        }
        
        if (currentFish != null) {
            if (currentFish.GetComponent<FlockAgent>().isHooked == false) {
                currentFish = null;
            }
        }
    }
}
