using UnityEngine;

namespace UI
{
    public class SafeArea : MonoBehaviour
    {
        void Awake()
        {
            if (TryGetComponent<RectTransform>(out var rectTransform))
            {
                Vector2 anchorMin = Screen.safeArea.position;
                Vector2 anchorMax = Screen.safeArea.position + Screen.safeArea.size;
                anchorMin.x /= Screen.width;
                anchorMin.y /= Screen.height;
                anchorMax.x /= Screen.width;
                anchorMax.y /= Screen.height;
                rectTransform.anchorMin = anchorMin;
                rectTransform.anchorMax = anchorMax;
            }
        }
    }
}
