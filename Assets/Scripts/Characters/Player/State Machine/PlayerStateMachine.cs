using System.Collections.Generic;
using Characters.Base.State_Machine;
using State_Machine;

namespace Characters.Player.State_Machine
{
    public class PlayerStateMachine : AgentStateMachine<PlayerStates, Player, PlayerStateMachine>
    {
        public override void Init(Player player)
        {
            base.Init(player);
            var playerGameObject = gameObject;
            States = new Dictionary<PlayerStates, BaseState<PlayerStates>>
            {
                { PlayerStates.Idle, new PlayerIdleState(PlayerStates.Idle, playerGameObject, this) },
                { PlayerStates.GoToTarget, new PlayerGoToTargetState(PlayerStates.GoToTarget, playerGameObject, this) },
                { PlayerStates.Attack, new PlayerAttackState(PlayerStates.Attack, playerGameObject, this) },
                { PlayerStates.Flee, new PlayerFleeState(PlayerStates.Flee, playerGameObject, this) }
            };

            CurrentState = States[PlayerStates.Idle];
        }
    }

    public enum PlayerStates
    {
        Idle,
        GoToTarget,
        Attack,
        Flee
    }
}