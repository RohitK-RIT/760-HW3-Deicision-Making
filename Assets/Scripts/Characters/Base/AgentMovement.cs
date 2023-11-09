using System;
using System.Collections.Generic;
using Environment;
using UnityEngine;

namespace Characters.Base
{
    /// <summary>
    /// Abstract class to handle character movement. 
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class AgentMovement : MonoBehaviour
    {
        /// <summary>
        /// Time taken to traverse between nodes.
        /// </summary>
        [SerializeField] private float timeBetweenNodes = 0.2f;

        /// <summary>
        /// Flag to check if the agent is the player.
        /// </summary>
        [SerializeField] private bool isPlayer;

        /// <summary>
        /// Variable to store the target node.
        /// </summary>
        private Node _targetNode;

        /// <summary>
        /// Target's position, duh!!
        /// </summary>
        public Node TargetNode
        {
            get => _targetNode;
            private set
            {
                // Check if the value is same as the current target node, if it is then return. 
                if (_targetNode != null && _targetNode.Equals(value))
                    return;

                if (_targetNode != null)
                    _targetChanged = true;

                //Set the value as target node.
                _targetNode = value;
            }
        }

        public bool HasTarget => TargetNode != null;

        /// <summary>
        /// Stack to store the current path.
        /// </summary>
        private Stack<Node> _currentPath;
        /// <summary>
        /// Property Stack to store the current path of the character.
        /// </summary>
        private Stack<Node> CurrentPath
        {
            get => _currentPath;

            set
            {
                _currentPath = value;
#if UNITY_EDITOR
                _currentPathArray = value?.ToArray();
#endif
            }
        }

#if UNITY_EDITOR
        /// <summary>
        /// Path Array to draw gizmos.
        /// </summary>
        private Node[] _currentPathArray;
#endif

        /// <summary>
        /// Current Node the character is at.
        /// </summary>
        private Node _currentNode;

        /// <summary>
        /// Reference of the next node in the path.
        /// </summary>
        private Node _nextNode;

        /// <summary>
        /// Flag to check if the target node has been changed.
        /// </summary>
        private bool _targetChanged;

        /// <summary>
        /// Time elapsed while traveling from _currentNode to _nextNode.
        /// </summary>
        private float _deltaTime;

        private void Start()
        {
            // Get the current node near which the character is standing.
            _currentNode = GroundSystem.Instance.GetNearestNode(transform.position);
            transform.position = _currentNode.WorldPos;
        }

        public void SetTarget(Vector2 targetPosition)
        {
            TargetNode = GroundSystem.Instance.GetNearestNode(targetPosition);
        }

        private void Update()
        {
            if (TargetNode == null) return;
            if (_currentNode.Equals(TargetNode))
            {
                TargetNode = null;
                return;
            }

            CurrentPath ??= GroundSystem.Instance.GetPath(_currentNode, TargetNode);
            if (_nextNode == null)
            {
                if (!CurrentPath.TryPop(out _nextNode))
                {
                    CurrentPath = null;
                    return;
                }

                LookAtTarget(_nextNode.WorldPos);
            }

            transform.position = Vector2.Lerp(_currentNode.WorldPos, _nextNode.WorldPos, _deltaTime / timeBetweenNodes);
            _deltaTime += Time.deltaTime;

            if (Vector2.Distance(transform.position, _nextNode.WorldPos) > 0.01f) return;

            transform.position = _nextNode.WorldPos;
            _currentNode = _nextNode;
            _deltaTime = 0f;
            _nextNode = null;

            if (!_targetChanged) return;

            CurrentPath = null;
            _targetChanged = false;
        }

        /// <summary>
        /// Function to make the character look at the target.
        /// </summary>
        private void LookAtTarget(Vector2 point)
        {
            // Get the normalized direction of the character.
            var direction = (point - (Vector2)transform.position).normalized;
            // Apply the rotation of the object.
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg));
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if(isPlayer)
                DrawGizmos();
        }

        private void OnDrawGizmosSelected()
        {
            DrawGizmos();
        }

        private void DrawGizmos()
        {
            if(!Application.isPlaying) return;

            // Check if the path is valid.
            if (_currentPathArray is { Length: > 0 })
            {
                // Give black color to the path.
                Gizmos.color = Color.black;
                for (var i = 0; i < _currentPathArray.Length - 1; i++)
                    Gizmos.DrawLine(_currentPathArray[i].WorldPos, _currentPathArray[i + 1].WorldPos); // Draw the line between nodes.
            }
        }
#endif
    }
}