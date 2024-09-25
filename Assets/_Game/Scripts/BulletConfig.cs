using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/BulletConfig", order = 1)]
public class BulletConfig : ScriptableObject
{
    public float speed = 2;
    public float rotSpeed = 180;
    public float timeToExit = 2.0f;

}
