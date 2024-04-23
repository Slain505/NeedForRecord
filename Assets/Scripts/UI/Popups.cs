using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class Popups : MonoBehaviour
    {
        [SerializeField] private GameObject _endGamePopup;
        [SerializeField] private GameObject _pausePopup;
        [SerializeField] private GameObject _optionPopup;
        [SerializeField] private Button _pauseButton;
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _optionButton;
        [SerializeField] private Button _optionCloseButton;
        [SerializeField] private Button _mainMenuButton;
        [SerializeField] private Button _restartGameOverButton;
        [SerializeField] private Button _mainMenuGameOverButton;
        [SerializeField] private Button _resetPlayerPrefsButton;
        [SerializeField] private Text _yourScoreText;
        [SerializeField] private Text _highScoreText;
        public event Action GamePaused;
        public event Action GameResumed;
        public event Action GameOver;
        
        public static Popups Instance { get; private set; }
    
        void Start()
        {
            Instance = this;
            _pauseButton.onClick.AddListener(OnPauseButtonClicked);
            _resumeButton.onClick.AddListener(OnResumeButtonClicked);
            _restartButton.onClick.AddListener(OnRestartButtonClicked);
            _optionButton.onClick.AddListener(OnOptionsButtonClicked);
            _optionCloseButton.onClick.AddListener(OnOptionsCloseButtonClicked);
            _mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
            _restartGameOverButton.onClick.AddListener(OnRestartButtonClicked);
            _mainMenuGameOverButton.onClick.AddListener(OnMainMenuButtonClicked);
            _resetPlayerPrefsButton.onClick.AddListener(ResetPlayerPrefs);
        }

        private void OnMainMenuButtonClicked()
        {
            Player.Player.Instance.UpdateCoinBalance();
            SceneManager.LoadScene(0);
        }

        private void OnRestartButtonClicked()
        {
            Level.Level.Instance.OnGameResumed();
            SceneManager.LoadScene(1);
        }

        private void OnPauseButtonClicked()
        {
            GamePaused?.Invoke();
            if (_pausePopup.activeSelf)
            {
                _pausePopup.SetActive(false);
                Level.Level.Instance.OnGameResumed();
            }
            else if (_optionPopup.activeSelf)
            {
                _optionPopup.SetActive(false);
                _pausePopup.SetActive(true);
            }
            else if (_endGamePopup.activeSelf)
            {
                _pauseButton.onClick.RemoveListener(OnPauseButtonClicked);
            }
            else
            {
                _pausePopup.SetActive(true);
                Level.Level.Instance.OnGamePaused();
            }
        }

        private void OnResumeButtonClicked()
        {
            GameResumed?.Invoke();
            _pausePopup.SetActive(false);
            Level.Level.Instance.OnGameResumed();
        }

        private void OnOptionsButtonClicked()
        {
            _optionPopup.SetActive(true);
            _pausePopup.SetActive(false); 
        }
        
        private void OnOptionsCloseButtonClicked()
        {
            _optionPopup.SetActive(false);
            _pausePopup.SetActive(true);
        }

        public void OnPlayerDied()
        {
            GameOver?.Invoke();
            _endGamePopup.SetActive(true);
            _highScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
            _yourScoreText.text = PlayerPrefs.GetInt("Score").ToString();
        }
        
        private void ResetPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
            Debug.Log("PlayerPrefs reset.");
        }
    
        private void OnDestroy()
        {
            _pauseButton.onClick.RemoveListener(OnPauseButtonClicked);
            _resumeButton.onClick.RemoveListener(OnResumeButtonClicked);
            _restartButton.onClick.RemoveListener(OnRestartButtonClicked);
            _optionButton.onClick.RemoveListener(OnOptionsButtonClicked);
            _mainMenuButton.onClick.RemoveListener(OnMainMenuButtonClicked);
            _endGamePopup.SetActive(false);
        }
    }
}
