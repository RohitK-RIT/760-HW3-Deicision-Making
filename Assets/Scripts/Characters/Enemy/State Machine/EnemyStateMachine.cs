using System.Collections.Generic;
using Characters.Base;
using Characters.Base.State_Machine;
using State_Machine;

namespace Characters.Enemy.State_Machine
{
    public class EnemyStateMachine : AgentStateMachine<EnemyStates, Enemy, EnemyStateMachine>
    {
        public override void Init(Enemy enemy)
        {
            base.Init(enemy);
            var playerGameObject = gameObject;
            States = new Dictionary<EnemyStates, BaseState<EnemyStates>>
            {
                { EnemyStates.Wander, new EnemyWanderState(EnemyStates.Wander, playerGameObject, this) },
                { EnemyStates.Attack, new EnemyAttackState(EnemyStates.Attack, playerGameObject, this) },
            };

            CurrentState = States[EnemyStates.Wander];
        }
    }

    public enum EnemyStates
    {
        Wander,
        Attack
    }
}