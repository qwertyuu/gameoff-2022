using UnityEngine;

namespace Game.Navigation
{
    public sealed class Navigator : MonoBehaviour
    {
        [SerializeField] RailNode startingNode = null;
        [SerializeField] private float speed = 4;

        [Header("Rotation")] [SerializeField] bool faceDirection = true;
        [SerializeField] float rotationSpeed = 360;

        private Vector3 _direction;
        private float _distance;
        private RailNode _target;

        bool HasReachedTarget => _distance <= Mathf.Epsilon;

        private void Awake()
        {
            _target = startingNode;
            ProceedToNextNode();
        }

        private void Update()
        {
            if (_target != null)
            {
                if (HasReachedTarget)
                {
                    ProceedToNextNode();
                }
                else
                {
                    float coveredDistance = Mathf.Min(speed * Time.deltaTime * _target.SpeedMultiplier, _distance);
                    transform.position += _direction * coveredDistance;
                    _distance -= coveredDistance;
                    if (faceDirection)
                    {
                        Vector3 forward = Vector3.RotateTowards(transform.forward, _direction,
                            rotationSpeed * Time.deltaTime, 0);
                        transform.rotation = Quaternion.LookRotation(forward);
                    }
                }
            }
        }

        private void ProceedToNextNode()
        {
            transform.position = _target.Position;

            RailNode next = _target.Next;
            if (next == null)
            {
                enabled = false;
                return;
            }
            
            _direction = next.Position - _target.Position;
            _distance = _direction.magnitude;
            _direction /= _distance;
            _target = next;
        }

        private void OnDrawGizmos()
        {
            if (_target == null) return;
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position + Vector3.up, _target.Position + Vector3.up);
        }
    }
}