using Characters.Base;
using Characters.Enemy.State_Machine;
using UnityEngine;

namespace Characters.Enemy
{
    public class Enemy : Agent<EnemyStates, EnemyStateMachine, Enemy>
    {
        /// <summary>
        /// Function to check if player is in range.
        /// </summary>
        /// <returns>if player is in range</returns>
        public override bool HasEnemyInRange()
        {
            return GameManager.Instance.Player &&
                   Vector2.Distance(GameManager.Instance.Player.transform.position, transform.position) <= GameManager.Instance.EnemySearchRadius;
        }

        private void OnDrawGizmosSelected()
        {
            DrawGizmos();
        }
    }
}