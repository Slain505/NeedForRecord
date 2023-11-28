using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Score : MonoBehaviour
    {
        [SerializeField] private Text _scoreText;
        [SerializeField] private Text _scoreHeaderText;
        private int _score;
        private int _highScore;

        private void Update()
        {
            _score = PlayerPrefs.GetInt("Score", 0);
            _scoreText.text = _score.ToString();
            if (CheckHighScore())
            {
                _scoreText.color = Color.cyan;
                _scoreHeaderText.color = Color.cyan;
                _scoreHeaderText.text = "New High Score!";
            }
        }

        private bool CheckHighScore()
        {
            _highScore = PlayerPrefs.GetInt("HighScore", 0);
            if (_score > _highScore)
            {
                PlayerPrefs.SetInt("HighScore", _score);
                return true;
            }
            return false;
        }
    }
}
