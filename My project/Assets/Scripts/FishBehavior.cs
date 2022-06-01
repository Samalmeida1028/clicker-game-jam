using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBehavior : MonoBehaviour
{
    public int interpolationFramesCount = 2; // Number of frames to completely interpolate between the 2 positions
    private int elapsedFrames = 0;

    private Vector3 targetPos;
    private Vector3 beingPulledTowards;
    private Vector3 beingPulledFrom;

    private float speed = 13.0F;

    // Time when the movement started.
    private float startTime;

    // Total distance between the markers.
    private float journeyLength;
    
    [SerializeField]
    public FishScriptableObject fishType;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.GetComponent<FlockAgent>().isClicked) { return; }
        if (this.beingPulledTowards == new Vector3(0, 0, 0) || this.beingPulledFrom == new Vector3(0, 0, 0)) { return; }

        // Distance moved equals elapsed time times speed..
        float distCovered = (Time.time - startTime) * speed;

        // Fraction of journey completed equals current distance divided by total distance.
        float fractionOfJourney = distCovered / journeyLength;

        // Set our position as a fraction of the distance between the markers.
        this.transform.position = Vector3.Lerp(beingPulledFrom, beingPulledTowards, fractionOfJourney);
        
        float dist = (this.transform.position - this.targetPos).magnitude;
        if (dist <= 1) {
            this.Catch();
        }
    }

    public void Pulled(Vector3 pulledTowardsPos, float amount) {
        this.targetPos = pulledTowardsPos;
        Vector3 a = this.transform.position;
        Vector3 b = (pulledTowardsPos - a).normalized;

        Vector3 pulledTowards = a + (amount * b);

        this.beingPulledTowards = pulledTowards;
        this.beingPulledFrom = a;

        // Keep a note of the time the movement started.
        this.startTime = Time.time;

        // Calculate the journey length.
        this.journeyLength = Vector3.Distance(this.beingPulledFrom, this.beingPulledTowards);
    }   


    public void Catch() {
        Debug.Log("Caught fish!");
        Destroy(gameObject);
    }
}
