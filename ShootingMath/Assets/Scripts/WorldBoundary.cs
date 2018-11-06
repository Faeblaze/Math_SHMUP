using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldBoundary : MonoBehaviour {
    public Vector3 extents;
    public Vector3 offset;

    public Bounds WorldBounds { get; private set; }
    public static WorldBoundary Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        WorldBounds = new Bounds()
        {
            center = transform.position + offset,
            extents = extents
        };
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + offset, extents * 2F);
    }
}
