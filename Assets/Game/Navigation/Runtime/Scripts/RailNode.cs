using UnityEngine;

namespace Game.Navigation
{
    public class RailNode : MonoBehaviour
    {
        [SerializeField] private Transform self;
        [SerializeField] private RailNode mainRoute;
        [SerializeField] private RailNode alternateRoute;
        
        public float SpeedMultiplier { get; private set; } = 1;
        
        private bool _switched = false;

        public Vector3 Position => self.position;
        public RailNode Next => _switched ? alternateRoute : mainRoute;
        
        internal RailNode AlternateRoute => alternateRoute;

        void Reset()
        {
            self = transform;
        }

        void Start()
        {
            CalculateSpeedModifier();
        }

        public void SwitchRoute()
        {
            _switched = alternateRoute != null;
            CalculateSpeedModifier();
        }
        
        private void CalculateSpeedModifier()
        {
            if (Next == null) return;
            
            Vector3 vector = Next.Position - Position;
            Vector3 topDown = vector;
            topDown.y = 0;

            float angle = Vector3.SignedAngle(topDown, vector, Quaternion.AngleAxis(90, Vector3.up) * vector);
            float baseMultiplier = angle / 60;
            if (baseMultiplier > 0) baseMultiplier *= 2;
            SpeedMultiplier = 1 + baseMultiplier;
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
            Gizmos.color = _switched ? Color.red : Color.green;
            Gizmos.DrawSphere(Position, 0.25f * (1 - transparency));

            Gizmos.color = Color.cyan - Color.black * transparency;
            if (mainRoute != null)
            {
                Gizmos.DrawLine(Position, mainRoute.Position);
            }

            if (alternateRoute != null)
            {
                Gizmos.color = Color.yellow - Color.black * transparency;
                Gizmos.DrawLine(Position, alternateRoute.Position);
            }
        }
    }
}