using UnityEngine;

public class CharacterAnimations : MonoBehaviour
{
    public const string DIETRIGGER = "Die", 
                        ATTACKTRIGGER = "Attack",
                        REVIVE = "Revive";
    [SerializeField] private Animator _animator;

    public void SetAnimationTrigger(string triggerName)
    {
        _animator.SetTrigger(triggerName);
    }

    public void SetupMovement(Vector2 movementVector)
    {
        if (movementVector.magnitude > 0)
        {
            _animator.SetBool("Walk", true);
        }
        else
        {
            _animator.SetBool("Walk", false);
        }
    }
}
