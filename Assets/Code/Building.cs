using Assets.Code.Data;
using Assets.Code.UI;
using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Code
{
    [RequireComponent(typeof(Collider2D))]
    public class Building : MonoBehaviour
    {
        public int health;

        public int reward;
        public int rewardPerDamage;

        public BuildingEvent spawnedEvent;
        public BuildingEvent buildingDestroyed;
        public IntEvent rewardMoney;
        private bool mouseWasOver = false;
        public TextAnimator goldTextPrefab;
        public Transform uiParent;

        [HideInInspector]
        public Collider2D bounds;

        private void Start()
        {
            bounds = GetComponent<Collider2D>();
        }

        private void Awake()
        {
            bounds = GetComponent<Collider2D>();
        }

        public void DealDamage(int amount)
        {
            health -= amount;
            var damageReward = amount * rewardPerDamage;
            rewardMoney.Raise(damageReward);
            if (!IsAlive())
            {
                buildingDestroyed.Raise(this);
                ShowGoldGain(reward + damageReward);
                gameObject.SetActive(false);
            }
            else
            {
                ShowGoldGain(damageReward);
            }
        }

        private void ShowGoldGain(int amount)
        {
            var text = Instantiate(goldTextPrefab, uiParent);
            text.SetAmount(amount);
            text.transform.position = transform.position;
        }

        public bool IsAlive()
        {
            return health > 0;
        }

        private void Update()
        {
            // OnMouseDown for some reason stops wokring when relaodin a scene, so i'm going manualy
            var worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPos.z = 0;
            var isOver = bounds.bounds.Contains(worldPos);

            if (mouseWasOver != isOver)
            {
                if (isOver)
                {
                    HandleMouseOver();
                }
                else
                {
                    HandleMouseExit();
                }

                mouseWasOver = isOver;
            }

            if (Input.GetMouseButtonDown(0) && isOver)
            {
                HandleMouseDown();
            }
        }

        private void HandleMouseOver()
        {
            gameObject.GetComponent<Light>().intensity = 1f;
        }

        private void HandleMouseExit()
        {
            gameObject.GetComponent<Light>().intensity = 0f;
        }

        private void HandleMouseDown()
        {
            spawnedEvent.Raise(this);
        }
    }
}

