using Characters.Base;
using UnityEngine;

namespace Characters.Player
{
    /// <summary>
    /// Player's health component
    /// </summary>
    public class PlayerHealth : AgentHealth
    {
        /// <summary>
        /// Field to store the player health rejuvenation per second.
        /// </summary>
        [SerializeField, Header("Player attributes")]
        private float rejuvenationPerSecond = 0.1f;

        /// <summary>
        /// Property to check if the component is player.
        /// </summary>
        protected override bool IsPlayer => true;
        
        private void Update()
        {
            // Rejuvenate the player health over time.
            Hp += rejuvenationPerSecond * Time.deltaTime;
        }
    }
}