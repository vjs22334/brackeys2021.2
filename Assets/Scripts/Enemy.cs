using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : ship
{

    public float MaxDirectionChangeTime;

    public float MinDirectionChangeTime;
    

    protected override void Start()
    {
        //currDirection = transform.up;
        StartCoroutine(ChangeDirection());
    }

    protected override void Update()
    {
        transform.position += currDirection * moveSpeed * Time.deltaTime;
        float angleDiff = Vector2.SignedAngle(spriteTransform.up, currDirection);
        if(Mathf.Abs(angleDiff) > 1f){
            float zRotation = Mathf.Sign(angleDiff) * turnspeed * Time.deltaTime;
            zRotation = Mathf.Clamp(zRotation, -1 * Mathf.Abs(angleDiff), Mathf.Abs(angleDiff));
            float newZ = spriteTransform.localRotation.eulerAngles.z + zRotation;
            spriteTransform.localRotation = Quaternion.Euler(0, 0, newZ);
        }
    }

    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    void OnDestroy()
    {
        StopCoroutine(ChangeDirection());
    }

    IEnumerator ChangeDirection(){
        while(true)
        {
            float waitTime = Random.Range(MinDirectionChangeTime,MaxDirectionChangeTime);
            yield return new WaitForSeconds(waitTime);
            
            Vector2 newDirection = Random.insideUnitCircle;
            currDirection = new Vector3(newDirection.x,newDirection.y,0);
        }
    }
}
