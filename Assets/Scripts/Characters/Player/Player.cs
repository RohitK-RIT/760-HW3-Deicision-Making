using System;
using System.Linq;
using Characters.Base;
using Characters.Player.State_Machine;
using UnityEngine;

namespace Characters.Player
{
    public class Player : Agent<PlayerStates, PlayerStateMachine, Player>
    {
        public event Action OnTargetAchieved;
        public override bool HasEnemyInRange()
        {
            return GameManager.Instance.Enemies.Where(enemy => enemy)
                .Any(enemy => Vector2.Distance(enemy.transform.position, transform.position) <= GameManager.Instance.EnemySearchRadius);
        }

        public void TargetAchieved()
        {
            OnTargetAchieved?.Invoke();
        }

        private void OnDrawGizmos()
        {
            DrawGizmos();
        }
    }
}