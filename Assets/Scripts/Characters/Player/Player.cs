using System;
using System.Linq;
using Characters.Base;
using Characters.Player.State_Machine;
using UnityEngine;

namespace Characters.Player
{
    /// <summary>
    /// Player agent
    /// </summary>
    public class Player : Agent<PlayerStates, PlayerStateMachine, Player>
    {
        /// <summary>
        /// Event for target achieve by the player.
        /// </summary>
        public event Action OnTargetAchieved;

        /// <summary>
        /// Function to check if player has enemies in range
        /// </summary>
        /// <returns></returns>
        public override bool HasEnemyInRange()
        {
            return GameManager.Instance.Enemies.Where(enemy => enemy)
                .Any(enemy => Vector2.Distance(enemy.transform.position, transform.position) <= GameManager.Instance.EnemySearchRadius);
        }

        /// <summary>
        /// Function called when target is achieved
        /// </summary>
        internal void TargetAchieved()
        {
            // Invoke the on target achieved event.
            OnTargetAchieved?.Invoke();
        }

        private void OnDrawGizmos()
        {
            DrawGizmos();
        }
    }
}