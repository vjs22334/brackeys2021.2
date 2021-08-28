using UnityEngine;

public class Enemy : ship
{

    //public float MaxDirectionChangeTime;

    //public float MinDirectionChangeTime;


    protected override void Start()
    {
        //currDirection = transform.up;
        //StartCoroutine(ChangeDirection());
    }

    protected override void Update()
    {
        transform.position += currDirection * moveSpeed * Time.deltaTime;
        float anglediff = Vector2.SignedAngle(spriteTransform.up, currDirection);
        if (Mathf.Abs(anglediff) > 1f)
        {
            float zrotation = Mathf.Sign(anglediff) * turnspeed * Time.deltaTime;
            zrotation = Mathf.Clamp(zrotation, -1 * Mathf.Abs(anglediff), Mathf.Abs(anglediff));
            float newz = spriteTransform.localRotation.eulerAngles.z + zrotation;
            spriteTransform.localRotation = Quaternion.Euler(0, 0, newz);
        }
    }

    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    //void OnDestroy()
    //{
    //    StopCoroutine(ChangeDirection());
    //}

    //IEnumerator ChangeDirection()
    //{
    //    while (true)
    //    {
    //        float waitTime = Random.Range(MinDirectionChangeTime, MaxDirectionChangeTime);
    //        yield return new WaitForSeconds(waitTime);

    //        Vector2 newDirection = Random.insideUnitCircle;
    //        currDirection = new Vector3(newDirection.x, newDirection.y, 0);
    //    }
    //}
}
