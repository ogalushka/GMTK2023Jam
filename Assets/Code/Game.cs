using Assets.Code;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public PathBehaviour lane;
    public Unit unitPrefab;

    public List<Unit> units;

    public float spawnInterval;

    private float currentSpawnInterval;

    void Update()
    {
        if (currentSpawnInterval < 0)
        {
            SpawnUnits(lane);
            currentSpawnInterval = spawnInterval;
        }
        else
        {
            currentSpawnInterval -= Time.deltaTime;
        }
    }


    private void SpawnUnits(PathBehaviour path)
    {
        var startNode = path.First();
        var unit = Instantiate(unitPrefab, startNode.position, startNode.rotation);
        unit.SetPath(path);
    }
}

