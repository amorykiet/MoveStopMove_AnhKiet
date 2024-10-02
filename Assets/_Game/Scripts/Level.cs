using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Level : MonoBehaviour
{
    public List<Vector3> positionSpawnList = new();

    public void OnInit()
    {
        SpawnPositions();
    }

    public void SpawnPositions()
    {
        Vector3 randomPosition;
        NavMeshHit hit;

        int coutToExit = 0;

        while (positionSpawnList.Count < 10)
        {
            coutToExit++;
            randomPosition = Random.insideUnitSphere * 30.0f;

            if (NavMesh.SamplePosition(randomPosition, out hit, 30.0f, 1))
            {
                if(IsSpawnable(hit.position))
                {
                    positionSpawnList.Add(hit.position);
                }
            }
            if (coutToExit == 100000)
            {
                break;
            }
        }
    }

    public bool IsSpawnable(Vector3 myPos)
    {
        bool isSpawnable = true;
        foreach (var position in positionSpawnList)
        {
            if (Vector3.Distance(myPos, position) < 6.0f)
            {
                isSpawnable = false;
                break;
            }
        }
        return isSpawnable;
    }

}
