using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "BulletConfig", menuName = "ScriptableObjects/BulletConfig", order = 1)]
public class BulletConfig : ScriptableObject
{
    public float rotSpeed = 180;
    public float timeToExit = 1.0f;
    public Quaternion rotation = Quaternion.Euler(90, 0, 0);
}
