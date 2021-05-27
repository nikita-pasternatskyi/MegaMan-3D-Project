using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Items;
using Core.General;

namespace Core.Levels
{
    public class LevelSettings : MonoBehaviour
    {
        [SerializeField] private GameObject _winScreen;

        public static LevelSettings Instance;
        public bool IsPaused = false;

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
                    _enemyToDefeat.ActorKilled += FinishLevel;
                    break;
                case LevelType.DestroyObject:
                    break;
            }
        }

        private void Awake()
        {
            IsPaused = false;
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
            FinishedLevel?.Invoke();
            _winScreen.SetActive(true);
        }

        public void PauseGame()
        {
            Instance.IsPaused = true;
            Time.timeScale = 0;
        }

        public void ResumeGame()
        {
            Instance.IsPaused = false;
            Time.timeScale = 1;
        }
    }
}
