using UnityEngine;

public class ShipCollider : MonoBehaviour
{
    public ShipType shipType;

    public bool CollisionProcessed = false;

    public bool FirstTimeWallTrigger = false;



    ship _ship;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        _ship = GetComponentInParent<ship>();
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("LandZone") && shipType != ShipType.ENEMY && _ship.isLanding)
        {
            _ship.LandingProcess(other.transform, other.GetComponent<LandingPylon>());
        }

        if (other.CompareTag("Wall")&&shipType!=ShipType.ENEMY)
        {
            if (!FirstTimeWallTrigger)
                FirstTimeWallTrigger = true;
            else
            {
                _ship.currDirection.x *= other.GetComponent<WallBounce>().bounceVector.x;
                _ship.currDirection.y *= other.GetComponent<WallBounce>().bounceVector.y;
            }
            Debug.Log("Wall hit " + _ship.currDirection);
        }

        if (other.CompareTag("Ship") && !_ship.Landed && FirstTimeWallTrigger)
        {
            ShipCollider otherShipCollider = other.GetComponent<ShipCollider>();
            ShipType otherShipType = otherShipCollider.shipType;

            if (shipType == ShipType.SHIP)
            {
                //destroy ship 
                _ship.DestroyShip();


                //ship will only call for life loss if the other type is also a ship.
                if (otherShipType == ShipType.SHIP && !CollisionProcessed)
                {
                    GameManager.Instance.RemoveLife();
                    otherShipCollider.CollisionProcessed = true;// this will prevent double counting
                }
            }
            else if (shipType == ShipType.ENEMY)
            {

                if (otherShipType == ShipType.DEFENDER)
                {
                    //destroy ship
                    _ship.DestroyShip();
                }
                else if (otherShipType == ShipType.ENEMY)
                {
                    //destroy ship
                    _ship.DestroyShip();
                }
                else if (otherShipType == ShipType.SHIP)
                {
                    GameManager.Instance.RemoveLife();

                    //Do shoot animation if any.
                }
            }
            else if (shipType == ShipType.DEFENDER)
            {
                Defender defender = GetComponentInParent<Defender>();
                if (otherShipType == ShipType.DEFENDER && !CollisionProcessed)
                {
                    //destroy ship
                    _ship.DestroyShip();

                    GameManager.Instance.RemoveLife();
                    otherShipCollider.CollisionProcessed = true;// this will prevent double counting

                }
                else if (otherShipType == ShipType.ENEMY)
                {
                    if (defender.ammoCount > 0)
                    {
                        defender.ammoCount--;
                    }
                    else
                    {
                        _ship.DestroyShip();
                    }
                    GameManager.Instance.AddScore(false);
                }
                else if (otherShipType == ShipType.SHIP)
                {
                    //destroy ship
                    _ship.DestroyShip();

                    GameManager.Instance.RemoveLife();


                }
            }

        }

    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Wall")&&shipType==ShipType.ENEMY)
        {
            if (!FirstTimeWallTrigger)
                FirstTimeWallTrigger = true;
            else
            {
                GameManager.Instance.EnemyEscaped();
            }
        }
    }
}

public enum ShipType
{
    ENEMY,
    SHIP,
    DEFENDER
}
