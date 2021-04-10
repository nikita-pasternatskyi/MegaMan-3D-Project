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
            Input.PauseButtonPressed += Open;
        }

        private void Open()
        {
            if (_currentPauseMenuState == PauseMenuState.Opened)
            {
                Close();
            }
            else if ( _currentPauseMenuState == PauseMenuState.Closed)
            {
                LevelSettings.PauseGame();
                _uiPauseMenu.SetActive(true);
            }
        }

        private void Close()
        {
            LevelSettings.ResumeGame();
            _uiPauseMenu.SetActive(false);
        }

        private void OnDisable()
        {
            Input.PauseButtonPressed -= Open;
        }
    }
}
