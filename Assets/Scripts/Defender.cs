using DG.Tweening;
using UnityEngine;

public class Defender : ship
{
    public int TotalAmmoCount;
    public float RearmTIme;
    [HideInInspector]
    public int ammoCount;
    public override void LandingProcess(Transform pylonTransform, LandingPylon pylon)
    {
        base.LandingProcess(pylonTransform, pylon);
        GameManager.Instance.ReArmDefender();

    }

    public void Launch()
    {
        ammoCount = TotalAmmoCount;
        transform.DOScale(Vector3.one, 1f);
        transform.DOLocalMoveY(2f, 1f).onComplete += () =>
        {
            Landed = false;
            currDirection = transform.up;
        };
    }

    public override void DestroyShip()
    {
        base.DestroyShip();
        GameManager.Instance.ReArmDefender();

    }
}
