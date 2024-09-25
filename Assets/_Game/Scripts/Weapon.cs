using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Bullet bullet;
    [SerializeField] private GameObject model;

    private Character owner;
    private Transform bulletSpawnPos;

    public void SetOwner(Character owner)
    {
        this.owner = owner;
    }

    public void SetBulletSpawnPos(Transform spawnPos)
    {
        bulletSpawnPos = spawnPos;
    }

    public void FireOnScale(float scale)
    {
        Instantiate(bullet, bulletSpawnPos.position, bulletSpawnPos.rotation).OnInit(scale, owner);
    }


    public void Show()
    {
        model.SetActive(true);
    }

    public void Hide()
    {
        model.SetActive(false);
    }
}
