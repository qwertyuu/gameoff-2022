using System.Collections.Generic;
using UnityEngine;

namespace Game.Navigation
{
    public class RailRenderer : MonoBehaviour
    {
        [SerializeField] private RailNode startingNode;
        [SerializeField] private LineRenderer lineRenderer;

        private void OnValidate()
        {
            if (startingNode == null || lineRenderer == null) return;
            
            List<Vector3> positions = new List<Vector3>();
            
            positions.Add(startingNode.Position);
            RailNode node = startingNode.AlternateRoute ?? startingNode.Next;

            while (node != null)
            {
                positions.Add(node.Position);
                node = node.Next;
            }

            lineRenderer.positionCount = positions.Count;
            lineRenderer.SetPositions(positions.ToArray());
        }
    }
}
