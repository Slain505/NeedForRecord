using UnityEngine.Serialization;

namespace Game.Player
{
    [System.Serializable]
    public class PlayerConfig
    {
        public float Acceleration;
        //public float Brake;
        public float TurnSpeed;
        public int Health;
    }
}