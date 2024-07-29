using Zenject;
using UnityEngine;

public abstract class NPC : MonoBehaviour, IInteractable
{
    [SerializeField] protected string text = "";
    protected AudioSource _audioSource;
    protected DialogUI _dialogUI;

    [Inject]
    private void Construct(DialogUI dialogUI)
    {
        _dialogUI = dialogUI;
    }

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public virtual void Interact()
    {
        _dialogUI.ShowDialogText(text);
        _audioSource.Play();
    }

}
