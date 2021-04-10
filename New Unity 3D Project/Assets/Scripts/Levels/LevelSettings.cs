using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Items;
using Assets.Scripts.General;
using TMPro;

namespace Assets.Scripts.Levels
{
    class LevelSettings : MonoBehaviour
    {
        public static LevelSettings Instance;

        public GameObject TMP;
        public static bool IsPaused = false;

        public delegate void OnFinishedLevel();
        public static event OnFinishedLevel FinishedLevel;

        [SerializeField] private LevelType _levelType;

        [Header("Level Type: Defeat enemy")]
        [SerializeField] private Health _enemyToDefeat;

        [Header("Level Type: Collect an item")]
        [SerializeField] private Item _itemToCollect;

        private void OnEnable()
        {
            switch (_levelType)
            {
                case LevelType.CollectItem:
                    _itemToCollect.PickedUp += FinishLevel;
                    break;
                case LevelType.DefeatEnemy:
                    _enemyToDefeat.Killed += FinishLevel;
                    break;
                case LevelType.DestroyObject:
                    break;
            }
        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if(Instance == this) 
            {
                Destroy(gameObject);
            }
        }

        protected virtual void FinishLevel()
        {
            Debug.Log("hio");
            FinishedLevel?.Invoke();
            TMP.SetActive(true);
        }

        public static void PauseGame()
        {
            Time.timeScale = 0;
        }

        public static void ResumeGame()
        {
            Time.timeScale = 1;
        }
    }
}
