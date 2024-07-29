using System;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;

    public Collider2D Interact(bool isSpriteFlipped)
    {
        Debug.DrawRay(transform.position, isSpriteFlipped ? Vector3.left : Vector3.right, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, isSpriteFlipped ? Vector3.left : Vector3.right, 1, _layerMask);
        return hit.collider;
    }
}
