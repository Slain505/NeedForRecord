using System;

namespace Game.Player
{
    public class PlayerModel
    {
        public event Action<PlayerModel> OnCoinCollect = delegate { };
        public event Action<PlayerModel> OnBusterColide = delegate { };
        //public event Action<PlayerModel, float> damageTaken = delegate { };
		
        public float Acceleration { get; }
        public float TurnSpeed { get; }
        public int Score { get; private set; }
        public int Health { get; private set; }
    
        public PlayerModel(PlayerConfig config)
        {
            Acceleration = config.Acceleration;
            Health = config.Health;
            TurnSpeed = config.TurnSpeed;
        }
    }
}