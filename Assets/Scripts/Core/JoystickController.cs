using UnityEngine;
using UnityEngine.EventSystems;

namespace Core
{
    public class JoystickController : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
    {
        private RectTransform joystickBackground;
        private RectTransform joystickHandle;

        private void Start()
        {
            // Настройте эти ссылки с помощью вашего UI
            joystickBackground = GetComponent<RectTransform>();
            joystickHandle = transform.GetChild(0).GetComponent<RectTransform>();
            joystickHandle.anchoredPosition = Vector2.zero;
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 position = RectTransformUtility.WorldToScreenPoint(Camera.main, joystickBackground.position);
            Vector2 radius = joystickBackground.sizeDelta / 2;
            Vector2 direction = (eventData.position - position) / (radius);
            direction = (direction.magnitude > 1) ? direction.normalized : direction;

            // Переместите ручку джойстика
            joystickHandle.anchoredPosition = direction * radius;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnDrag(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            joystickHandle.anchoredPosition = Vector2.zero;
        }

        // Функция для получения ввода из других скриптов
        public Vector2 GetInputDirection()
        {
            return joystickHandle.anchoredPosition / (joystickBackground.sizeDelta / 2);
        }
    }
}