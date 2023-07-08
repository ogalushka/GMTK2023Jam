using Assets.Code;
using Assets.Code.Data;
using Assets.Code.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Game : MonoBehaviour
{
    public PathBehaviour[] paths;
    public Unit unitPrefab;
    public GameObject unitsContainer;
    public SelectedUnits selectedUnits;
    public UIBehaviour uiBehaviour;
    public int startingMoney;
    public WaveInfo cheapestUnit;
    public Building mainTargetBuilding;

    public BuildingEvent spawnEvent;
    public IntEvent rewardMoney;
    public GameEvent unitDiedEvent;
    public BuildingEvent buildingDestroyedEvent;

    private Queue<SpawnQueueItem>[] pathQueues;
    private int playerMoney;

    private void OnEnable()
    {
        spawnEvent.AddListeners(SpawnUnitsOnEvent);
        rewardMoney.AddListeners(RewardMoney);
        buildingDestroyedEvent.AddListeners(BuildingDestroyedHandler);
        unitDiedEvent.AddListeners(CheckGameOver);

        pathQueues = new Queue<SpawnQueueItem>[paths.Length];
        for (var i = 0; i < pathQueues.Length; i++)
        {
            pathQueues[i] = new Queue<SpawnQueueItem>();
        }

        SetMoney(startingMoney);
    }

    private void OnDisable()
    {
        spawnEvent.RemoveListeners(SpawnUnitsOnEvent);
        rewardMoney.RemoveListeners(RewardMoney);
        unitDiedEvent.RemoveListeners(CheckGameOver);
        buildingDestroyedEvent.RemoveListeners(BuildingDestroyedHandler);
    }

    private void Update()
    {
        foreach (var path in paths)
        {
            path.spawnCooldown -= Time.deltaTime;
            if (path.spawnCooldown <= 0 
                && path.spawnQueue != null 
                && path.spawnQueue.Count > 0)
            {
                var swanItem = path.spawnQueue.Dequeue();

                var unit = Instantiate(swanItem.unitPrefab, unitsContainer.transform);
                unit.SetPath(swanItem.path);
                path.spawnCooldown = swanItem.cooldown;
            }
        }
    }

    private void RewardMoney(int amount)
    {
        SetMoney(playerMoney + amount);
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
            return;
        }

        var waveInfo = selectedUnits.waveInfo;
        if (waveInfo.price > playerMoney)
        {
            return;
        }

        SetMoney(playerMoney - waveInfo.price);

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

    private void SetMoney(int money)
    {
        playerMoney = money;
        uiBehaviour.SetMoney(money);
    }

    private void BuildingDestroyedHandler(Building building)
    {
        SetMoney(playerMoney + building.reward);
        if (building == mainTargetBuilding)
        {
            uiBehaviour.ShowWin();
        }
    }

    private void CheckGameOver()
    {
        StartCoroutine(CheckGameOverDelayed());
    }

    private IEnumerator CheckGameOverDelayed()
    {
        yield return new WaitForFixedUpdate();
        if (unitsContainer.transform.childCount == 0 && playerMoney < cheapestUnit.price)
        {
            uiBehaviour.ShowGameOver();
        }
    }
}

