using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
[RequireComponent(typeof(LineRenderer))]
public class ship : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float turnspeed = 100f;

    public Transform spriteTransform;

    public LandZone landZone;

    LineRenderer lineRenderer;
    List<Vector3> pathPoints;

    int currPathIndex = 1;

    Vector3 currDirection;

    bool Landed = false;


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
        if(Landed){
            return;
        }

        if (pathPoints.Count > 1)
        {
            if (currPathIndex > pathPoints.Count - 1)
            {
                currPathIndex = 1;
                clearpath();
            }

            currDirection = (pathPoints[currPathIndex] - transform.position).normalized;
            currDirection.z = 0;
            currDirection = currDirection.normalized;
        }
        else
        {
            currPathIndex = 1;
            clearpath();
        }

        transform.position += currDirection * moveSpeed * Time.deltaTime;
        float angleDiff = Vector2.SignedAngle(spriteTransform.up, currDirection);
        float zRotation = Mathf.Sign(angleDiff) * turnspeed * Time.deltaTime;
        // Debug.Log("zRotation before:"+zRotation);
        zRotation = Mathf.Clamp(zRotation, -1 * Mathf.Abs(angleDiff), Mathf.Abs(angleDiff));
        // Debug.Log("AngleDiff:"+angleDiff);
        // Debug.Log("zRotation:"+zRotation);
        // Debug.DrawRay(transform.position,currDirection*3,Color.green);
        // Debug.Log("oldZ: "+ spriteTransform.localRotation.eulerAngles.z);
        float newZ = spriteTransform.localRotation.eulerAngles.z + zRotation;
        // Debug.Log("newZ: "+ newZ);
        spriteTransform.localRotation = Quaternion.Euler(0, 0, newZ);
        // Debug.DrawRay(transform.position,spriteTransform.up*3,Color.red);
        if ((currPathIndex < pathPoints.Count) && (pathPoints[currPathIndex] - transform.position).magnitude < 0.01f)
            currPathIndex++;

    }

    public void LandingProcess(Vector2 landDirection, Vector3 landPosition){
        if(Landed){
            return;
        }
        Landed = true;
        clearpath();
        transform.DOScale(new Vector3(0.6f,0.6f,0.6f),1f);
        float angleDiff = Vector2.SignedAngle(spriteTransform.up, landDirection);
        Debug.Log(angleDiff);
        float newZ = transform.localRotation.eulerAngles.z + angleDiff;
        spriteTransform.DOLocalRotate(new Vector3(0,0,newZ),1f);
        transform.DOMove(landPosition,1f).onComplete += ()=>{
            //do runway animation. for now I'm making it dissapear.
            DestroyShip();
        };
    }

    public void DestroyShip(){
        //Do vfx here.
        Destroy(gameObject);
    }

    //color change to indicate going to land
    public void HeadingToLand(){

    }

    //collision indicator
    public void CollisionAlert(){

    }


    public void ResetHeadingToLand(){

    }

    public void ResetCollisionAlert(){

    }


}
