using System.Collections;
using UnityEngine;

public class SpawnSystem : MonoBehaviour
{
    public SpawnPoint[] spawnPoints;

    public GameObject[] ships;
    public GameObject[] enemyShips;


    public GameObject defenderShip;
    public Transform defenderSpawnPoint;

    public GameObject offScreenPointer;
    public GameObject offScreenPointerCanvas;

    public bool isGamePlaying
    {
        get
        {
            if (GameManager.Instance == null)
            {
                return true;
            }
            else
            {
                return GameManager.Instance.isPlaying;
            }
        }
    }
    public float shipSpawningTime = 5f;


    private int currentPoint;
    private int currentShip;
    private Quaternion rot;
    private void Awake()
    {
        spawnPoints = GetComponentsInChildren<SpawnPoint>();
        Debug.Log("Total spawn points " + spawnPoints.Length);
    }

    private void Start()
    {
        StartCoroutine(Spawning());
    }

    IEnumerator Spawning()
    {
        while (isGamePlaying)
        {
            SpawnShips();
            yield return new WaitForSeconds(shipSpawningTime);
        }
    }

    public void DefenderSpawn()
    {
        GameObject ship = Instantiate(defenderShip, defenderSpawnPoint.position, Quaternion.identity);
        ship.GetComponentInChildren<ShipCollider>().FirstTimeWallTrigger = true;
        //ship.GetComponent<Defender>().currDirection = Vector3.up;
        ship.GetComponent<Defender>().Launch();
        ship.transform.localScale *= 0.6f;

    }

    private void SpawnShips()
    {
        currentPoint = Random.Range(0, spawnPoints.Length);
        Debug.Log("Current Point " + currentPoint);
        GameObject ship = Instantiate(ships[GetRandomShip()], spawnPoints[currentPoint].transform.position, Quaternion.identity);
        ship.GetComponent<ship>().currDirection = GetRandomSpawnPosAndRot();
        GameObject offscreenIndi = Instantiate(offScreenPointer, offScreenPointerCanvas.transform);
        offscreenIndi.transform.SetParent(offScreenPointerCanvas.transform);
        offscreenIndi.GetComponent<ArrowPointer>().targetTransform = ship.transform;
        //ship.GetComponent<ship>().SetSpawnSpeedAndRot(GetRandomShipSpeed(), GetRandomShipRot());
    }

    private float GetRandomShipSpeed()
    {
        float speed = Random.Range(3f, 10f);
        return speed;
    }
    private float GetRandomShipRot()
    {
        float speed = Random.Range(85f, 150f);
        return speed;
    }

    private int GetRandomShip()
    {
        return currentShip = Random.Range(0, ships.Length);
    }


    private Vector3 GetRandomSpawnPosAndRot()
    {

        Vector3 shipRotation = new Vector3(0, 0, 0);
        SpawnPoint currentSpawnPoint = spawnPoints[currentPoint];
        // Current logic is added for multiple diretion of spawnpoints
        // and also for handling differnt ships size and their rotation.

        //I have 3 if statements, this can be fixed later
        if (currentSpawnPoint.shipStartRotation.x != 0 || currentSpawnPoint.shipEndRotation.x != 0)
        {
            shipRotation.x = Random.Range(currentSpawnPoint.shipStartRotation.x, currentSpawnPoint.shipEndRotation.x);
        }
        if (currentSpawnPoint.shipStartRotation.y != 0 || currentSpawnPoint.shipEndRotation.y != 0)
        {
            shipRotation.y = Random.Range(currentSpawnPoint.shipStartRotation.y, currentSpawnPoint.shipEndRotation.y);
        }

        return shipRotation;
        //return rot = Quaternion.Euler(shipRotation.x, shipRotation.y, shipRotation.z);

    }

}
