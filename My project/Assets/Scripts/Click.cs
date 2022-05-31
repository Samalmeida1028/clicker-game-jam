using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour
{
    public Camera Camera;
    
    private bool HasFish = false;

    private bool IsFish(RaycastHit2D hitObject) {
        // Check if the object hit is a fish
        if (hitObject.transform == null) { return false; }
        if (hitObject.transform.GetComponent<FishBehavior>() == null) { return false; }

        return true;
    }

    void Update() {   
        // Begin checking for mouse clicks on fishies
        if(Input.GetMouseButtonDown(0) && !HasFish) {
            Ray ray = Camera.ScreenPointToRay( Input.mousePosition ); // Create a ray from the camera to the mouse pos
            RaycastHit2D hit = Physics2D.Raycast( ray.origin, ray.direction, Mathf.Infinity ); // Look at this later but casts the ray
            
            if (IsFish(hit)) {
                // We hit a fish
                Transform fish = hit.transform;
                fish.GetComponent<FishBehavior>().Catch();
                fish.GetComponent<FlockAgent>().isClicked = true;
                HasFish = true;
            }
        }
    }
}
