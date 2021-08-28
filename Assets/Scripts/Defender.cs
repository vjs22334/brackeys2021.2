using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Defender : ship
{
    public int TotalAmmoCount;
    public float RearmTIme;
    [HideInInspector]
    public int ammoCount;
    public override void LandingProcess(Transform pylonTransform, LandingPylon pylon)
    {
        base.LandingProcess(pylonTransform, pylon);
        //Inform gameManager defender landed
    }

    public void Launch(){
        ammoCount = TotalAmmoCount;
        transform.DOScale(Vector3.one,1f);
        transform.DOLocalMoveY(2f,1f).onComplete += () =>{
            Landed = false;
            currDirection = transform.forward;
        };
    }

    public override void DestroyShip()
    {
        base.DestroyShip();
        //inform gameManger defender can be redeplyed.
    }
}
