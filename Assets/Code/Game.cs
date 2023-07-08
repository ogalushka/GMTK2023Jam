using Assets.Code;
using Assets.Code.Data;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Game : MonoBehaviour
{
    public PathBehaviour[] paths;
    public Unit unitPrefab;
    public GameObject unitsContainer;
    public SpawnEvent spawnEvent;
    public SelectedUnits selectedUnits;

    public List<Unit> units;

    private Queue<SpawnQueueItem>[] pathQueues;

    private void OnEnable()
    {
        spawnEvent.AddListeners(SpawnUnitsOnEvent);

        pathQueues = new Queue<SpawnQueueItem>[paths.Length];
        for (var i = 0; i < pathQueues.Length; i++)
        {
            pathQueues[i] = new Queue<SpawnQueueItem>();
        }
    }

    private void OnDisable()
    {
        spawnEvent.RemoveListeners(SpawnUnitsOnEvent);
    }

    private void Update()
    {
        foreach (var path in paths)
        {
            path.spawnCooldown -= Time.deltaTime;
            if (path.spawnCooldown <= 0 && path.spawnQueue.Count > 0)
            {
                var swanItem = path.spawnQueue.Dequeue();

                var unit = Instantiate(swanItem.unitPrefab, unitsContainer.transform);
                unit.SetPath(swanItem.path);
                path.spawnCooldown = swanItem.cooldown;
            }
        }
    }

    private void SpawnUnitsOnEvent(Building target)
    {
        var path = paths.Where(p => p.nodes.Any(n => n.building == target)).FirstOrDefault();
        if (path == null)
        {
            Debug.LogError($"No path to building");
            return;
        }

        if (selectedUnits.waveInfo == null)
        {
            Debug.LogError($"waveInfo in Selected Units tracker needs to be set by ui buttons");
        }

        var waveInfo = selectedUnits.waveInfo;

        for (var i = 0; i < waveInfo.unitCount; i++)
        {
            path.spawnQueue.Enqueue(new SpawnQueueItem
            {
                path = path,
                cooldown = waveInfo.spawnInterval,
                unitPrefab = waveInfo.unitPrefab
            });
        }
    }
}

