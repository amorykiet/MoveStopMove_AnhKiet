using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponType type;

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

    public void Fire(float scale, float speed)
    {
        HBPool.Spawn<Bullet>(bullet.poolType, bulletSpawnPos.position, bulletSpawnPos.rotation).OnInit(scale, owner, speed);
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
