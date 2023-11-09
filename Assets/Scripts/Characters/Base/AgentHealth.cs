using UnityEngine;

namespace Characters.Base
{
    /// <summary>
    /// Component for Agent's Health
    /// </summary>
    public class AgentHealth : MonoBehaviour
    {
        /// <summary>
        /// Field for agent's starting health points.
        /// </summary>
        [SerializeField] private float startingHealth;
        /// <summary>
        /// Reference of the fill rect transform to display the health.
        /// </summary>
        [SerializeField] private RectTransform fill;

        /// <summary>
        /// Property to access the health points for the agent. 
        /// </summary>
        protected float Hp
        {
            get => _hp;
            set
            {
                // Clamp the input value between 0 and starting health.
                _hp = Mathf.Clamp(value, 0f, startingHealth);
                // Change the display bar.
                fill.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Normalized * 10);

                // If health is 0, then destroy the gameObject.
                if (Hp == 0)
                    Destroy(gameObject);
            }
        }

        /// <summary>
        /// Variable to store the health points.
        /// </summary>
        private float _hp;

        /// <summary>
        /// Normalized value of health points.
        /// </summary>
        public float Normalized => Hp / startingHealth;

        /// <summary>
        /// Flag to check if this component is attached to a player agent type.
        /// </summary>
        protected virtual bool IsPlayer => false;

        protected virtual void Start()
        {
            // Set the health points to starting value.
            Hp = startingHealth;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            CollisionCheck(other);
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            CollisionCheck(other);
        }

        /// <summary>
        /// Function to check collision.
        /// </summary>
        /// <param name="other">collision data</param>
        private void CollisionCheck(Collision2D other)
        {
            // Check if is player
            if (IsPlayer)
            {
                // Get the enemy component and deduct the damage dealt.
                if (other.gameObject.GetComponent<Enemy.Enemy>() is { } enemy)
                    Hp -= enemy.Damage;
            }
            else
            {
                // Get the player component and deduct the damage dealt.
                if (other.gameObject.GetComponent<Player.Player>() is { } player)
                    Hp -= player.Damage;
            }
        }
    }
}