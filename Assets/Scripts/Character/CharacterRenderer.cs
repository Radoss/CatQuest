using System.Collections;
using UnityEngine;

public class CharacterRenderer : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _playerSprite;
    private Color _initColor;

    public bool IsSpriteFlipped { get => _playerSprite.flipX; }

    private void Start()
    {
        _initColor = _playerSprite.color;
    }

    public void RenderCharacter(Vector2 directionVector)
    {
        if (Mathf.Abs(directionVector.x) > 0.1f)
            _playerSprite.flipX = Vector3.Dot(transform.right, directionVector) < 0;
    }

    public void FlashRed()
    {
        StopAllCoroutines();
        StartCoroutine(FlashRedCoroutine());
    }

    private IEnumerator FlashRedCoroutine()
    {
        _playerSprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        _playerSprite.color = _initColor;
    }
}
