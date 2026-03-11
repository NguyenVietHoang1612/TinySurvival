using System;
using UnityEngine;


public static class TransformExtensions
{
    public static int GetSortingOrder(this Transform transform)
    {
        return -Mathf.RoundToInt(transform.position.y * 100);
    }
}


