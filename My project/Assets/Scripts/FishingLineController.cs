using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingLineController : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public List<LineSegment> lineSegments = new List<LineSegment>();
    public float lineSegmentLength = 0.25f;
    public int lineLength = 35;
    public float lineWidth = 0.1f;
    public Transform origin;
    public Transform target;
    public Vector2 gravity = new Vector2(0f, -1f);

    public struct LineSegment {
        public Vector2 posNow;
        public Vector2 posOld;

        public LineSegment(Vector2 pos) {
            this.posNow = pos;
            this.posOld = pos;
        }
    }

    void Start() {
        this.lineRenderer = this.GetComponent<LineRenderer>();
        Vector3 lineStartPoint = origin.position;

        for (int i = 0; i < lineLength; i++) {
            this.lineSegments.Add(new LineSegment(lineStartPoint));
            lineStartPoint.y -= lineSegmentLength;
        }
    }

    private void DrawLine() {
        float lineWidth = this.lineWidth;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;

        Vector3[] linePositions = new Vector3[this.lineLength];
        for (int i = 0; i < this.lineLength; i++) {
            linePositions[i] = this.lineSegments[i].posNow;
        }

        lineRenderer.positionCount = linePositions.Length;
        lineRenderer.SetPositions(linePositions);
    }

    private void Simulate() {
        for (int i = 0; i < this.lineLength; i++) {
            LineSegment firstSegment = this.lineSegments[i];

            Vector2 velocity = firstSegment.posNow - firstSegment.posOld;

            firstSegment.posOld = firstSegment.posNow;

            firstSegment.posNow += velocity;
            firstSegment.posNow += gravity * Time.fixedDeltaTime;

            this.lineSegments[i] = firstSegment;
        }

        // Constraints
        for (int i = 0; i < 50; i++) {
            this.ApplyConstraint();
        }
    }

    private void ApplyConstraint() {
        LineSegment firstSegment = this.lineSegments[0];
        firstSegment.posNow = origin.position;
        this.lineSegments[0] = firstSegment;

        for (int i = 0; i < this.lineLength - 1; i++) {
            LineSegment firstSeg = this.lineSegments[i];
            LineSegment secondSeg = this.lineSegments[i + 1];

            float dist = (firstSeg.posNow - secondSeg.posNow).magnitude;
            float error = dist - lineSegmentLength;
            Vector2 changeDir = (firstSeg.posNow - secondSeg.posNow).normalized;
            Vector2 changeAmount = changeDir * error;

            if (i != 0) {
                firstSeg.posNow -= changeAmount * 0.5f;
                this.lineSegments[i] = firstSeg;

                secondSeg.posNow += changeAmount * 0.5f;
                this.lineSegments[i + 1] = secondSeg;
            } else {
                secondSeg.posNow += changeAmount;
                this.lineSegments[i + 1] = secondSeg;
            }
        }

        LineSegment lastSegment = this.lineSegments[lineLength - 1];
        lastSegment.posNow = target.position;
        this.lineSegments[lineLength - 1] = lastSegment;

    }

    void Update() {
       this.DrawLine();
    }

    void FixedUpdate() {
        this.Simulate();
    }
}
