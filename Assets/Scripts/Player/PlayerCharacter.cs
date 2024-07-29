using UnityEngine;
using Zenject;

public class PlayerCharacter : Character
{
    public int NumberOfSlotsInInventory { get { return _numberOfSlotsInInventory; } }
    private IInput _playerInput;
    private PlayerInteractions _playerInteractions;
    private Attacker _attacker;
    private PlayerDialogsUI _playerDialogsUI;
    [SerializeField] private int _numberOfSlotsInInventory = 20;

    [Inject]
    private void Construct(IInput playerInput, DialogUI dialogUI, StoreUI storeUI)
    {
        _playerInput = playerInput;
    }

    protected override void Start()
    {
        base.Start();
        _attacker = GetComponent<Attacker>();
        _playerInteractions = GetComponent<PlayerInteractions>();
        _playerDialogsUI = GetComponent<PlayerDialogsUI>();
        _playerInput.OnInteractEvent += _playerInput_OnInteractEvent;
        PotionSO.OnHealedEvent += PotionSO_OnHealedEvent;
        _attacker.OnAtackEvent += Attacker_OnAtackEvent;
    }

    private void OnDestroy()
    {
        _playerInput.OnInteractEvent -= _playerInput_OnInteractEvent;
        PotionSO.OnHealedEvent -= PotionSO_OnHealedEvent;
        _attacker.OnAtackEvent -= Attacker_OnAtackEvent;
    }

    private void PotionSO_OnHealedEvent(int healthPoints)
    {
        if (IsDead)
        {
            return;
        }
        _health.Heal(healthPoints);
    }

    private void _playerInput_OnInteractEvent()
    {
        if (IsDead || _playerDialogsUI.IsInteractableDialogOpened)
        {
            return;
        }
        Collider2D hit = _playerInteractions.Interact(_charRenderer.IsSpriteFlipped);
        if (hit != null)
        {
            if (hit.TryGetComponent<IInteractable>(out IInteractable interactable))
            {
                interactable.Interact();
            }

            if (hit.TryGetComponent<IDamagable>(out IDamagable damagable))
            {
                _attacker.TryAttack(damagable);
            }
        }
    }

    private void FixedUpdate()
    {
        if (IsDead)
        {
            return;
        }
        _charMovement.MoveCharacter(_playerInput.MovementInputVector);
        _charRenderer.RenderCharacter(_playerInput.MovementInputVector);
        _charAnimations.SetupMovement(_playerInput.MovementInputVector);
        if (_playerInput.MovementInputVector.magnitude > 0)
        {
            _playerDialogsUI.CloseNotStationaryDialogs();
        }
    }

}
