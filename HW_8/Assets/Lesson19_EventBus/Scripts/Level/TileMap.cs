using UnityEngine;
using UnityEngine.Tilemaps;

namespace Lessons.Level
{
    public sealed class TileMap : MonoBehaviour
    {
        private readonly Vector3 _positionOffset = Vector2.one * 0.5f;
        
        [SerializeField]
        private Tilemap tilemap;

        public bool IsWalkable(Vector2Int coordinates)
        {
            return tilemap.HasTile((Vector3Int)coordinates);
        }

        public Vector2Int PositionToCoordinates(Vector3 position)
        {
            return (Vector2Int)tilemap.WorldToCell(position);
        }

        public Vector3 CoordinatesToPosition(Vector2Int coordinates)
        {
            return tilemap.CellToWorld((Vector3Int)coordinates) + _positionOffset;
        }
    }
}
