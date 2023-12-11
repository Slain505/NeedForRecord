using UnityEngine;
using UnityEngine.EventSystems;

namespace Core
{
    public class JoystickController : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
    {
        private RectTransform joystickBackground;
        private RectTransform joystickHandle;
        
        public static JoystickController Instance { get; private set; }
        
        private void Start()
        {
            Instance = this;
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

        
        public Vector2 GetInputDirection()
        {
            return joystickHandle.anchoredPosition / (joystickBackground.sizeDelta / 2);
        }
    }
}