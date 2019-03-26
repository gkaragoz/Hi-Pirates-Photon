using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Referenced from https://en.wikipedia.org/wiki/Trajectory_of_a_projectile
/// </summary>

[RequireComponent(typeof(LineRenderer))]
public class LaunchArcRenderer : MonoBehaviour {

    public enum Direction {
        Right,
        Left
    }

    public float testVelocity;
    public Direction testDirection;
    public float angle = 45f;
    public int resolution = 10;

    private float _gravity; // Force of gravity on Y axis.
    private float _radianAngle;

    private LineRenderer _lr;

    private void Awake() {
        _lr = GetComponent<LineRenderer>();

        _gravity = Mathf.Abs(Physics.gravity.y);
    }

    private void OnValidate() {
        // Check that _lr is not null and that the game is playing.
        if (_lr != null && Application.isPlaying) {
            RenderArc(testVelocity, testDirection);
        }
    }

    // Populating the lineRenderer with the appropriate settings.
    public void RenderArc(float velocity, Direction direction) {
        _lr.positionCount = resolution + 1;
        _lr.SetPositions(CalculateArcArray(velocity, direction));
    }

    // Create an array of Vector3 positions for arc.
    private Vector3[] CalculateArcArray(float velocity, Direction direction) {
        Vector3[] arcArray = new Vector3[resolution + 1];

        switch (direction) {
            case Direction.Right:
                _radianAngle = Mathf.Deg2Rad * angle;
                break;
            case Direction.Left:
                _radianAngle = Mathf.Deg2Rad * (180 - angle);
                break;
            default:
                _radianAngle = Mathf.Deg2Rad * angle;
                break;
        }

        float maxDistance = (velocity * velocity * Mathf.Sin(2 * _radianAngle)) / _gravity;

        for (int ii = 0; ii <= resolution; ii++) {
            float t = (float)ii / (float)resolution;
            arcArray[ii] = CalculateArcPoint(t, maxDistance, velocity);
        }

        return arcArray;
    }

    // Calculate height and distance of each vertex.
    private Vector3 CalculateArcPoint(float t, float maxDistance, float velocity) {
        float x = t * maxDistance;
        float y = x * Mathf.Tan(_radianAngle) - ((_gravity * x * x) / (2 * velocity * velocity * Mathf.Cos(_radianAngle) * Mathf.Cos(_radianAngle)));

        return new Vector3(x, y);
    }

}
