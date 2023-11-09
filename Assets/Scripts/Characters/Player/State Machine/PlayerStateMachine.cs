using System.Collections.Generic;
using Characters.Base.State_Machine;
using State_Machine;

namespace Characters.Player.State_Machine
{
    /// <summary>
    /// Player's state machine component.
    /// </summary>
    public class PlayerStateMachine : AgentStateMachine<PlayerStates, Player, PlayerStateMachine>
    {
        /// <summary>
        /// Initialization function for the player state machine component.
        /// </summary>
        /// <param name="player">player for which the state machine is</param>
        public override void Init(Player player)
        {
            // Initialize the base component.
            base.Init(player);
            var playerGameObject = gameObject;
            // Create the states for the player.
            States = new Dictionary<PlayerStates, BaseState<PlayerStates>>
            {
                { PlayerStates.Idle, new PlayerIdleState(PlayerStates.Idle, playerGameObject, this) },
                { PlayerStates.GoToTarget, new PlayerGoToTargetState(PlayerStates.GoToTarget, playerGameObject, this) },
                { PlayerStates.Attack, new PlayerAttackState(PlayerStates.Attack, playerGameObject, this) },
                { PlayerStates.Flee, new PlayerFleeState(PlayerStates.Flee, playerGameObject, this) }
            };

            // Set current state as idle.
            CurrentState = States[PlayerStates.Idle];
        }
    }

    /// <summary>
    /// Enum for the player's behaviour states.
    /// </summary>
    public enum PlayerStates
    {
        /// <summary>
        /// Stand still at a point and do nothing.
        /// </summary>
        Idle,

        /// <summary>
        /// Try to achieve the target.
        /// </summary>
        GoToTarget,

        /// <summary>
        /// Attack an enemy.
        /// </summary>
        Attack,

        // Flee from a point.
        Flee
    }
}