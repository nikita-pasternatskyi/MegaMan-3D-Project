using Core.Levels;
using UnityEngine;

namespace Core.Player.UI
{
    public class MenuControl : MonoBehaviour
    {
        public GameObject _uiPauseMenu;
        [SerializeField] private PauseMenuState _currentPauseMenuState;

        private void OnPauseGame()
        {
            TogglePauseMenu();
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
            Debug.Log(_uiPauseMenu);
            LevelSettings.Instance.PauseGame();
            _currentPauseMenuState = PauseMenuState.Opened;
            _uiPauseMenu?.SetActive(true); 
        }

        private void Close()
        {
            LevelSettings.Instance.ResumeGame();
            _currentPauseMenuState = PauseMenuState.Closed;
            _uiPauseMenu?.SetActive(false);
        }
    }
}
