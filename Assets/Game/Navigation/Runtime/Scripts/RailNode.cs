using UnityEngine;

namespace Game.Navigation
{
    public class RailNode : MonoBehaviour
    {
        [SerializeField] private Transform self;
        [SerializeField] private RailNode mainRoute;
        [SerializeField] private RailNode alternateRoute;
        [field: SerializeField, Min(0.1f)] public float SpeedMultiplier { get; private set; } = 1;

        private bool _switched = false;

        public Vector3 Position => self.position;
        public RailNode Next => _switched ? alternateRoute : mainRoute;
        
        internal RailNode AlternateRoute => alternateRoute;

        void Reset()
        {
            self = transform;
        }

        public void SwitchRoute()
        {
            _switched = alternateRoute != null;
        }

        private void OnDrawGizmos()
        {
            DrawGizmos(0.5f);
        }

        private void OnDrawGizmosSelected()
        {
            DrawGizmos(0);
        }

        private void DrawGizmos(float transparency)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(Position, 0.25f * (1 - transparency));

            Gizmos.color = Color.cyan - Color.black * transparency;
            if (mainRoute != null)
            {
                Gizmos.DrawLine(Position, mainRoute.Position);
            }

            if (alternateRoute != null)
            {
                Gizmos.color = Color.yellow - Color.black * transparency;
                Gizmos.DrawLine(Position, mainRoute.Position);
            }
        }
    }
}