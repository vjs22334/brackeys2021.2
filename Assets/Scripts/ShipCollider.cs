using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCollider : MonoBehaviour
{
    public ShipType shipType;

    public bool CollisionProcessed = false;

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
        if(other.CompareTag("LandZone")&&shipType!=ShipType.ENEMY){
            _ship.LandingProcess(other.transform);
        }

        if(other.CompareTag("Ship")){
            ShipCollider otherShipCollider = other.GetComponent<ShipCollider>();
            ShipType otherShipType = otherShipCollider.shipType;

            if(shipType == ShipType.SHIP){
                //destroy ship 
               _ship.DestroyShip();

                //call Gamemanager to lose a life.
                //ship will only call for life loss if the other type is also a ship.
                if(otherShipType == ShipType.SHIP && !CollisionProcessed){
                    //do life stuff here
                    otherShipCollider.CollisionProcessed = true;// this will prevent double counting
                }
            }
            else if(shipType == ShipType.ENEMY){
                
                if(otherShipType == ShipType.DEFENDER){
                    //destroy ship
                   _ship.DestroyShip();
                }
                else if(otherShipType == ShipType.ENEMY){
                    //destroy ship
                   _ship.DestroyShip();
                }
                else if(otherShipType == ShipType.SHIP){
                    //call gamemanager to lose a life

                    //Do shoot animation if any.
                }
            }
            else if(shipType == ShipType.DEFENDER){
                if(otherShipType == ShipType.DEFENDER && !CollisionProcessed){
                    //destroy ship
                   _ship.DestroyShip();

                    //do life stuff here
                    otherShipCollider.CollisionProcessed = true;// this will prevent double counting

                }
                else if(otherShipType == ShipType.ENEMY){
                    //do score stuff here
                }
                else if(otherShipType == ShipType.SHIP){
                   //destroy ship
                  _ship.DestroyShip();
                    
                    //call gamemanager to lose a life

                    
                }
            }
        }
    }
}

public enum ShipType{
    ENEMY,
    SHIP,
    DEFENDER
}
