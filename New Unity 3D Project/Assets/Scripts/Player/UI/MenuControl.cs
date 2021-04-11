using Assets.Scripts.Levels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Player.UI
{
    public class MenuControl : MonoBehaviour
    {
        [SerializeField] private GameObject _uiPauseMenu;
        [SerializeField] private PauseMenuState _currentPauseMenuState;

        private void OnEnable()
        {
            Input.PauseButtonPressed += TogglePauseMenu;
        }

        private void TogglePauseMenu()
        {
            if (_currentPauseMenuState == PauseMenuState.Opened)
            {
                Close();
            }
            else if ( _currentPauseMenuState == PauseMenuState.Closed)
            {
                Open();
            }
        }

        private void Open()
        {
            LevelSettings.PauseGame();
            _currentPauseMenuState = PauseMenuState.Opened;
            _uiPauseMenu.SetActive(true);
        }

        private void Close()
        {
            LevelSettings.ResumeGame();
            _currentPauseMenuState = PauseMenuState.Closed;
            _uiPauseMenu.SetActive(false);
        }

        private void OnDisable()
        {
            Input.PauseButtonPressed -= Open;
        }
    }
}
