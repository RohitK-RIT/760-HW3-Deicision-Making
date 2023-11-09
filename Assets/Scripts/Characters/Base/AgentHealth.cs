using System;
using UnityEngine;

namespace Characters.Base
{
    public class AgentHealth : MonoBehaviour
    {
        [SerializeField] private float startingHealth;
        [SerializeField] private LayerMask opponentLayerMask;
        [SerializeField] private RectTransform fill;

        protected float Hp
        {
            get => _hp;
            set
            {
                _hp = Mathf.Clamp(value, 0f, startingHealth);
                fill.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Normalized * 10);

                if (Hp == 0)
                    Destroy(gameObject);
            }
        }

        private float _hp;

        public float Normalized => Hp / startingHealth;

        private bool _isPlayer;

        private void Start()
        {
            _isPlayer = GetComponent<Player.Player>();
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

        private void CollisionCheck(Collision2D other)
        {
            if (_isPlayer)
            {
                if (other.gameObject.GetComponent<Enemy.Enemy>() is { } enemy)
                    Hp -= enemy.Damage;
            }
            else
            {
                if (other.gameObject.GetComponent<Player.Player>() is { } player)
                    Hp -= player.Damage;
            }
        }
    }
}