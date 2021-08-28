using System.Collections;
using UnityEngine;

public class SpawnSystem : MonoBehaviour
{
    public SpawnPoint[] spawnPoints;

    public GameObject[] ships;
    public GameObject enemyShip;


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
    public float enemySpawningTime = 5f;


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
        StartCoroutine(ShipSpawning());
        StartCoroutine(EnemySpawning());
    }

    IEnumerator ShipSpawning()
    {
        while (isGamePlaying)
        {
            SpawnShips();
            yield return new WaitForSeconds(shipSpawningTime);
        }
    }
    IEnumerator EnemySpawning()
    {
        while (isGamePlaying)
        {
            yield return new WaitForSeconds(enemySpawningTime);
            SpawnEnemies();
        }
    }

    public void DefenderSpawn()
    {
        GameObject ship = Instantiate(defenderShip, defenderSpawnPoint.position, Quaternion.identity);
        ship.GetComponentInChildren<ShipCollider>().FirstTimeWallTrigger = true;
        GameManager.Instance.defender = ship.GetComponent<Defender>();
        ship.GetComponent<Defender>().Landed = true;
        GameManager.Instance.launchBtn.interactable = true;
        ship.transform.localScale *= 0.6f;

    }

    private void GetACurrentSpawnPoint()
    {
        currentPoint = Random.Range(0, spawnPoints.Length);

    }

    private void SpawnEnemies()
    {
        GetACurrentSpawnPoint();
        GameObject ship = Instantiate(enemyShip, spawnPoints[currentPoint].transform.position, Quaternion.identity);
        Vector2 vector2 = Random.insideUnitCircle;
        Vector2 vdir = vector2 * Random.Range(10f, 15f);
        Vector3 vecDir = new Vector3(vdir.x, vdir.y, 0);
        ship.GetComponent<ship>().currDirection = (vecDir - ship.transform.position).normalized;
    }
    private void SpawnShips()
    {
        GetACurrentSpawnPoint();
        //Debug.Log("Current Point " + currentPoint);
        GameObject ship = Instantiate(ships[GetRandomShip()], spawnPoints[currentPoint].transform.position, Quaternion.identity);
        ship.GetComponent<ship>().currDirection = GetRandomSpawnPosAndRot();
        GameObject offscreenIndi = Instantiate(ship.GetComponent<ship>().OffScreenIndicator, offScreenPointerCanvas.transform);
        offscreenIndi.transform.SetParent(offScreenPointerCanvas.transform);
        offscreenIndi.GetComponent<ArrowPointer>().targetTransform = ship.transform;
        //ship.GetComponent<ship>().SetSpawnSpeedAndRot(GetRandomShipSpeed(), GetRandomShipRot());
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
