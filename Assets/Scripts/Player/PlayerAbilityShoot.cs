using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityShoot : PlayerAbilityBase
{
    public GunBase gunBase;
    public Transform gunPosition;

    private GunBase _currentGun;

    public FlashColor flashColor;
    protected override void Init()
    {
        base.Init();
        CreateGun();

        inputs.Gameplay.Shoot.performed += ctx => StartShoot();
        inputs.Gameplay.Shoot.canceled += ctx => CancelShoot();
        inputs.Gameplay.Gun.performed += ctx => ChangeGun();
    }

    private void CreateGun()
    {
        _currentGun = Instantiate(gunBase, gunPosition);

        _currentGun.transform.localPosition = _currentGun.transform.localEulerAngles = Vector3.zero;
    }
    private void StartShoot()
    {
        _currentGun.StartShoot();
        flashColor?.Flash();
        Debug.Log("Start Shoot");
    }

    private void CancelShoot()
    {
        _currentGun.StopShoot();
        Debug.Log("Cancel Shoot");
    }

    private void ChangeGun()
    {
        _currentGun.ChangeGun();
        Debug.Log("Change Gun");
    }
}
