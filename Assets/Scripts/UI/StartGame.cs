using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class StartGame : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Text _text;
        void Awake()
        {
            _playButton.onClick.AddListener(OnPlayButtonClicked);
            _text.DOColor(new Color(0, 0, 0, 0), 0.5f).SetLoops(-1, LoopType.Yoyo);
            _text.transform.DOMoveY(transform.position.y + 0.1f, 0.5f)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.InOutQuad);
        }

        private void OnPlayButtonClicked()
        {
            SceneManager.LoadScene(1);
        }
    }
}
