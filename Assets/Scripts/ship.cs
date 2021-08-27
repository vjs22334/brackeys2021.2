using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(LineRenderer))]
public class ship : MonoBehaviour
{

    public event Action<Vector3> deletePoint = delegate{};
    public float moveSpeed = 5f;
    public float turnspeed = 100f;

    public Transform spriteTransform;

    public LandZone landZone;

    LineRenderer lineRenderer;
    List<Vector3> pathPoints;

    int currPathIndex = 1;

    public Vector3 currDirection; // -1,0,0 

    bool Landed = false;


    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        pathPoints = new List<Vector3>();
        //currDirection = transform.up;
    }

    public void SetNewPath()
    {
        pathPoints = new List<Vector3>();
        lineRenderer.positionCount = pathPoints.Count;
        lineRenderer.SetPositions(pathPoints.ToArray());
        
    }

    public void AddPathPoint(Vector3 point){
        pathPoints.Add(point);
        lineRenderer.positionCount = pathPoints.Count;
        lineRenderer.SetPositions(pathPoints.ToArray());
    }
    

    public void clearpath()
    {
        pathPoints.Clear();
        lineRenderer.positionCount = 0;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (Landed)
        {
            return;
        }

        if (pathPoints.Count > 0)
        {
            currDirection = (pathPoints[0] - transform.position).normalized;
            currDirection.z = 0;
            currDirection = currDirection.normalized;
        }
      

        transform.position += currDirection * moveSpeed * Time.deltaTime;
        float angleDiff = Vector2.SignedAngle(spriteTransform.up, currDirection);
        if(Mathf.Abs(angleDiff) > 1f){
            float zRotation = Mathf.Sign(angleDiff) * turnspeed * Time.deltaTime;
            zRotation = Mathf.Clamp(zRotation, -1 * Mathf.Abs(angleDiff), Mathf.Abs(angleDiff));
            float newZ = spriteTransform.localRotation.eulerAngles.z + zRotation;
            spriteTransform.localRotation = Quaternion.Euler(0, 0, newZ);
        }
        if (pathPoints.Count > 0 && (pathPoints[0] - transform.position).magnitude < 0.01f){
            
            deletePoint?.Invoke(pathPoints[0]);
            pathPoints.RemoveAt(0);
            lineRenderer.positionCount = pathPoints.Count;
            lineRenderer.SetPositions(pathPoints.ToArray());
        }
                
        

    }

    public void LandingProcess(Transform pylonTransform, bool pylon){
        if(Landed){
            return;
        }
        Landed = true;
        clearpath();

        //hack to reset transform rotation,
        spriteTransform.SetParent(null, true);
        transform.rotation = Quaternion.identity;
        spriteTransform.SetParent(transform, true);


        transform.DOScale(new Vector3(0.6f,0.6f,0.6f),1f);
        spriteTransform.DOLocalRotate(new Vector3(0,0,pylonTransform.localEulerAngles.z),1f);
        if(!pylon){
            spriteTransform.GetComponent<SpriteRenderer>().sortingOrder = -10;
        }
        transform.DOMove(pylonTransform.position,1f).onComplete += ()=>{
            transform.DOMove(transform.position+pylonTransform.up*2f,1f).onComplete += () => {
                DestroyShip();
                GameManager.Instance.AddScore(true);
            };
            
        };
    }

    public void DestroyShip()
    {
        //Do vfx here.
        Destroy(gameObject);
    }

    //color change to indicate going to land
    public void HeadingToLand()
    {

    }

    //collision indicator
    public void CollisionAlert()
    {

    }


    public void ResetHeadingToLand()
    {

    }

    public void ResetCollisionAlert()
    {

    }



}
