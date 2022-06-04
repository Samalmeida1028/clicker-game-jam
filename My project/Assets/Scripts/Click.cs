using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour
{
    public Camera Camera;
    private FlockAgent currentFish;
    StatHandler stats;
    public float radius;
    private float cps = 1.0f;
    public int numClicks = 0;
    public int numFishCaught = 0;
    public int numFishHooked = 0;
    public int fishValue = 0;

    void Start(){
        stats = gameObject.GetComponent<StatHandler>();
    }

    private bool IsFish(Collider2D hitObject) {
        // Check if the object hit is a fish
        if (hitObject.transform == null) { return false; }
        if (hitObject.transform.GetComponent<FlockAgent>() == null) { return false; }

        return true;
    }

    void Update() {
        if(Input.GetMouseButtonDown(0)){
        numClicks ++;
        // Begin checking for mouse clicks on fishies
        if(!currentFish) {
            Vector3 mousePos = Camera.ScreenToWorldPoint( Input.mousePosition ); // Create a ray from the camera to the mouse pos
            Collider2D hit = Physics2D.OverlapCircle(mousePos, radius); // Look at this later but casts the ray
            
            if (hit!= null && IsFish(hit)) {
                numFishHooked++;
                // We hit a fish
                if (currentFish != null) {
                    GetComponent<FishingLineController>().target = null;
                }

                Transform fishTransform = hit.transform;

                if (currentFish == fishTransform.GetComponent<FlockAgent>()) { currentFish = null; return; }
                
                fishTransform.GetComponent<FlockAgent>().Hook();
                GetComponent<FishingLineController>().target = fishTransform;

                currentFish = fishTransform.GetComponent<FlockAgent>();
            }
        } else if(currentFish) {
            currentFish.Pulled(cps);
        }
        }
        
        if (currentFish != null) {
            if (currentFish.isCaught) {
                numFishCaught++;
                fishValue+=currentFish.value;
                stats.totalFishVal += currentFish.value;
                stats.setTotalVal(currentFish.value); //CHANGE THIS LATER DUMBY
                currentFish.Catch();
                currentFish = null;
            } else if (!currentFish.isHooked) {
                currentFish = null;
            }
        }
    }
}
