using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckerWithOverlap : MonoBehaviour
{
    #region Show in Inspector


    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private Color _gizmosColor;
    [SerializeField] Vector3 _halfExtents;
    [SerializeField] Transform _center;

    #endregion


    #region Main Methods

    public bool TestCollision(Vector3 center, Vector3 halfExtents, Quaternion orientation, LayerMask layerMask)
    {
        return Physics.OverlapBoxNonAlloc(center, halfExtents, _buffer, orientation, layerMask) > 0;
    }

    public bool TestCollision()
    {
        return TestCollision(_center.position, _halfExtents, Quaternion.identity, _layerMask);
    }
    #endregion

    #region Debug

    private void OnDrawGizmos()
    {

        Gizmos.color = _gizmosColor;
        Gizmos.DrawWireCube(_center.position, _halfExtents * 2);

    }
    #endregion


    #region Private

    private Collider[] _buffer = new Collider[1];


    #endregion
}
