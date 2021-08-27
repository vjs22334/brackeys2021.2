using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(LineRenderer))]
public class ship : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float turnspeed = 100f;

    public Transform spriteTransform;

    LineRenderer lineRenderer;
    List<Vector3> pathPoints;

    int currPathIndex = 1;

    Vector3 currDirection;


    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        pathPoints = new List<Vector3>();
        currDirection = transform.up;
    }

    public void SetNewPath(Vector3[] newPoints)
    {
        pathPoints = new List<Vector3>(newPoints);
        lineRenderer.positionCount = pathPoints.Count;
        lineRenderer.SetPositions(pathPoints.ToArray());
    }

    void clearpath()
    {
        pathPoints.Clear();
        lineRenderer.positionCount = 0;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (pathPoints.Count > 1)
        {
            if (currPathIndex > pathPoints.Count - 1)
            {
                currPathIndex = 1;
                clearpath();
            }

            currDirection = (pathPoints[currPathIndex] - transform.position).normalized;
        }
        else
        {
            currPathIndex = 1;
            clearpath();
        }

        transform.position += currDirection * moveSpeed * Time.deltaTime;
        float angleDiff = Vector3.SignedAngle(spriteTransform.up, currDirection, Vector3.forward);
        float zRotation = Mathf.Sign(angleDiff) * turnspeed * Time.deltaTime;
        zRotation = Mathf.Clamp(zRotation, -1 * Mathf.Abs(angleDiff), Mathf.Abs(angleDiff));
        spriteTransform.localRotation = Quaternion.Euler(0, 0, spriteTransform.localRotation.z + zRotation);

        if ((currPathIndex < pathPoints.Count) && (pathPoints[currPathIndex] - transform.position).magnitude < 0.01f)
            currPathIndex++;
    }

    public void SetSpawnSpeedAndRot(float MoveSpeed, float TurnRot)
    {
        moveSpeed = MoveSpeed;
        turnspeed = TurnRot;
    }




}
