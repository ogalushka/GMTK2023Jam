using Assets.Code;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public PathBehaviour lane;
    public Unit unitPrefab;
    public GameObject unitsContainer;

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
        var unit = Instantiate(unitPrefab, unitsContainer.transform);
        unit.SetPath(path);
    }
}

