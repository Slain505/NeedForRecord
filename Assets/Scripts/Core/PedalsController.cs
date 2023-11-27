using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.Game
{
    public class PedalsController : MonoBehaviour
    {
        [SerializeField] private EventTrigger _gasButton;
        [SerializeField] private EventTrigger _brakeButton;
        public bool IsGasButtonPressed { get; private set; }
        public bool IsBrakeButtonPressed { get; private set; }

        private void Awake()
        {
            foreach (var a in _gasButton.triggers)
            {
                if(a.eventID == EventTriggerType.PointerDown)
                    a.callback.AddListener(GasButtonPressed);
                else if (a.eventID == EventTriggerType.PointerUp)
                    a.callback.AddListener(GasButtonReleased);
                
            }

            foreach (var a in _brakeButton.triggers)
            {
                if(a.eventID == EventTriggerType.PointerDown)
                    a.callback.AddListener(BrakeButtonPressed);
                else if(a.eventID == EventTriggerType.PointerUp)
                    a.callback.AddListener(BrakeButtonReleased);
            }
        }

        private void GasButtonReleased(BaseEventData arg)
        {
            IsGasButtonPressed = false;
            Debug.Log("Gas button released");
        }

        private void BrakeButtonReleased(BaseEventData arg)
        {
            IsBrakeButtonPressed = false;
            Debug.Log("Brake button released");
        }

        void GasButtonPressed(BaseEventData arg)
        {
            IsGasButtonPressed = true;
            Debug.Log("Gas button pressed");
        }
        
        void BrakeButtonPressed(BaseEventData arg)
        {
            IsBrakeButtonPressed = true;
            Debug.Log("Brake button pressed");
        }
    }
}
