using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class Popups : MonoBehaviour
    {
        [SerializeField] private GameObject _endGamePopup;
        [SerializeField] private GameObject _pausePopup;
        [SerializeField] private Button _pauseButton;
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _mainMenuButton;
        [SerializeField] private Button _restartGameOverButton;
        [SerializeField] private Button _mainMenuGameOverButton;
        [SerializeField] private Text _yourScoreText;
        [SerializeField] private Text _highScoreText;
    
        public static Popups Instance { get; private set; }
    
        void Start()
        {
            Instance = this;
            _pauseButton.onClick.AddListener(OnPauseButtonClicked);
            _resumeButton.onClick.AddListener(OnResumeButtonClicked);
            _restartButton.onClick.AddListener(OnRestartButtonClicked);
            _mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
            _restartGameOverButton.onClick.AddListener(OnRestartButtonClicked);
            _mainMenuGameOverButton.onClick.AddListener(OnMainMenuButtonClicked);
        }

        private void OnMainMenuButtonClicked()
        {
            SceneManager.LoadScene(0);
        }

        private void OnRestartButtonClicked()
        {
            SceneManager.LoadScene(1);
        }

        private void OnPauseButtonClicked()
        {
            _pausePopup.SetActive(true);
            Level.Level.Instance.Paused();
        }

        private void OnResumeButtonClicked()
        {
            _pausePopup.SetActive(false);
            Level.Level.Instance.Resumed();
        }
    
        public void OnPlayerDied()
        {
            _endGamePopup.SetActive(true);
            _highScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
            _yourScoreText.text = PlayerPrefs.GetInt("Score").ToString();
        }
    
        private void OnDestroy()
        {
            _pauseButton.onClick.RemoveListener(OnPauseButtonClicked);
            _resumeButton.onClick.RemoveListener(OnResumeButtonClicked);
            _restartButton.onClick.RemoveListener(OnRestartButtonClicked);
            _mainMenuButton.onClick.RemoveListener(OnMainMenuButtonClicked);
            _endGamePopup.SetActive(false);
        }
    }
}
