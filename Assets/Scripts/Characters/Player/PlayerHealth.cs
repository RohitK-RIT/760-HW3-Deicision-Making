using Characters.Base;
using UnityEngine;

namespace Characters.Player
{
    public class PlayerHealth : AgentHealth
    {
        [SerializeField, Header("Player attributes")]
        private float rejuvenationPerSecond = 0.1f;

        private void Update()
        {
            Hp += rejuvenationPerSecond * Time.deltaTime;
        }
    }
}