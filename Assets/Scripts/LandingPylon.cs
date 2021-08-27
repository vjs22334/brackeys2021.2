using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingPylon : MonoBehaviour
{
    public LandZone landZone;

    [Tooltip("true for pylon, false for docking port")]
    public bool IsPylon  = true;

}

public enum LandZone
{
    LANDZONE_1,
    LANDZONE_2,
    LANDZONE_3
}
