using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(LineRenderer))]
public class PathDrawer : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public float pointSpacing = 0.5f;

    public LayerMask MouseRayCastLayerMask;
    public LayerMask LandZoneLayerMask;
    List<Vector3> positions;
    Vector3 currMousePosition;

    bool drawing = false;
    ship Ship = null;

    Camera camera;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        camera = Camera.main;
        positions = new List<Vector3>();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        Vector3 currMousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
        currMousePosition.z = 0;

        if (Input.GetButtonDown("Fire1"))
        {

            Collider2D shipCollider = Physics2D.OverlapPoint(currMousePosition, MouseRayCastLayerMask);
            if (shipCollider != null)
            {
                drawing = true;
                Ship = shipCollider.GetComponent<ship>();
            }
        }

        if (Input.GetButton("Fire1") && drawing)
        {

            if (DistanceFromLastpoint(currMousePosition) > pointSpacing)
            {
                positions.Add(currMousePosition);
                lineRenderer.positionCount = positions.Count;
                lineRenderer.SetPositions(positions.ToArray());
                Ship.SetNewPath(positions.ToArray());
            }

            Collider2D LandZoneCollider = Physics2D.OverlapPoint(currMousePosition, LandZoneLayerMask);
            if (LandZoneCollider != null)
            {
                LandZone landZone = GetComponent<LandingPylon>().landZone;
                if (landZone == Ship.landZone)
                {
                    positions.Add(LandZoneCollider.transform.position);
                    lineRenderer.positionCount = positions.Count;
                    lineRenderer.SetPositions(positions.ToArray());
                    drawing = false;
                    Ship.HeadingToLand();
                }

            }
        }

        if (Input.GetButtonUp("Fire1"))
        {
            positions.Clear();
            if (drawing)
            {
                drawing = false;
                lineRenderer.positionCount = positions.Count;
            }

        }
    }

    float DistanceFromLastpoint(Vector3 point)
    {
        if (positions.Count == 0)
        {
            return float.MaxValue;
        }
        return (point - positions[positions.Count - 1]).magnitude;
    }
}
