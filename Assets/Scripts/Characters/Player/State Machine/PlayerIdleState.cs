using UnityEngine;

namespace Characters.Player.State_Machine
{
    /// <summary>
    /// Player's idle state
    /// </summary>
    public class PlayerIdleState : PlayerBaseState
    {
        /// <summary>
        /// Constructor for the player's idle state.
        /// </summary>
        /// <param name="key">key of the state</param>
        /// <param name="gameObject">gameObject to which the owner state machine is attached</param>
        /// <param name="stateMachine">owner state machine</param>
        public PlayerIdleState(PlayerStates key, GameObject gameObject, PlayerStateMachine stateMachine) : base(key, gameObject, stateMachine)
        {
        }
        
        public override PlayerStates GetNextState()
        {
            // If player has a target, then try to achieve the target.
            if (GameManager.Instance.Target)
                return PlayerStates.GoToTarget;
            
            // If player has enemies in range then according to health attack or flee.
            if (Agent.HasEnemyInRange())
                return Agent.Health.Normalized < 0.3 ? PlayerStates.Flee : PlayerStates.Attack;

            // Else stay in idle state.
            return StateKey;
        }
    }
}