using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour
{
    public Camera Camera;
    
    private Transform currentFish;

    private float cps = 1.0f;

    private bool IsFish(RaycastHit2D hitObject) {
        // Check if the object hit is a fish
        if (hitObject.transform == null) { return false; }
        if (hitObject.transform.GetComponent<FishBehavior>() == null) { return false; }

        return true;
    }

    void Update() {   
        // Begin checking for mouse clicks on fishies
        if(Input.GetMouseButtonDown(0) && !currentFish) {
            Ray ray = Camera.ScreenPointToRay( Input.mousePosition ); // Create a ray from the camera to the mouse pos
            RaycastHit2D hit = Physics2D.Raycast( ray.origin, ray.direction, Mathf.Infinity ); // Look at this later but casts the ray
            
            if (IsFish(hit)) {
                // We hit a fish
                if (currentFish != null) {
                    currentFish.GetComponent<FlockAgent>().isClicked = false;
                    this.GetComponent<FishingLineController>().target = null;
                }

                Transform fish = hit.transform;

                if (currentFish == fish) { currentFish = null; return; }

                fish.GetComponent<FlockAgent>().isClicked = true;

                this.GetComponent<FishingLineController>().target = fish;

                currentFish = fish;
            }
        } else if(Input.GetMouseButtonDown(0) && currentFish) {
            FishBehavior fish = currentFish.GetComponent<FishBehavior>();

            fish.Pulled(this.GetComponent<FishingLineController>().origin.position, cps);
        }
        
        if (currentFish != null) {
            if (currentFish.GetComponent<FlockAgent>().isClicked == false) {
                currentFish = null;
            }
        }
    }
}
