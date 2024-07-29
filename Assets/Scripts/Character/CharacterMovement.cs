using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    private Rigidbody2D _rb2d;

    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    public void MoveCharacter(Vector2 movementVector)
    {
        _rb2d.velocity = movementVector * _movementSpeed;
    }

    public void StopCharacter()
    {
        _rb2d.velocity = Vector2.zero;
    }

    public void TurnRBOff()
    {
        _rb2d.isKinematic = true;
    }

    public void TurnRBOn()
    {
        _rb2d.isKinematic = false;
    }
}
