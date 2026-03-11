using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameRPG
{
    public class GridData
    {
        private Dictionary<Vector2Int, ObjectData> _gridCells = new();

        public void AddObjectToGrid(Vector2Int gridPos, Vector2 objectSize, int iD)
        {
            List<Vector2Int> occupiedCells = CalulateOccupiedCells(gridPos, objectSize);
            ObjectData newData = new ObjectData(iD, occupiedCells);

            foreach (var cell in occupiedCells)
            {
                if (_gridCells.ContainsKey(gridPos))
                {
                    throw new Exception($"Vung nay da co thu dat roi: {cell}");
                }
            }

            foreach (var cell in occupiedCells)
            {
                _gridCells[cell] = newData;
            }

        }

        public List<Vector2Int> CalulateOccupiedCells(Vector2Int gridPos, Vector2 objectSize)
        {
            List<Vector2Int> occupiedCells = new();
            for (int x = 0; x < objectSize.x; x++)
            {
                for (int y = 0; y < objectSize.y; y++)
                {
                    occupiedCells.Add(new Vector2Int(gridPos.x + x, gridPos.y + y));
                }
            }
            return occupiedCells;
        }

        public bool CanPlaceObject(Vector2Int gridPos, Vector2 objectSize)
        {
            List<Vector2Int> occupiedCells = CalulateOccupiedCells(gridPos, objectSize);
            foreach (var cell in occupiedCells)
            {
                if (_gridCells.ContainsKey(cell))
                {
                    return false;
                }
            }
            return true;
        }

        public void RemoveObjectFromGrid(Vector2Int gridPos)
        {
            foreach (var cell in _gridCells[gridPos].occupiedPosition)
            {
                _gridCells.Remove(cell);
            }
        }
    }

    public class ObjectData
    {
        public List<Vector2Int> occupiedPosition;
        public int ID { get; private set; }
        public ObjectData(int id, List<Vector2Int> occupiedPos)
        {
            ID = id;
            occupiedPosition = occupiedPos;
        }
    }
}
