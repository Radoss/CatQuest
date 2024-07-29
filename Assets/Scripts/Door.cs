using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class Door : MonoBehaviour
{
    [SerializeField] private string _sceneToLoad;
    private Conditions _conditions;
    private MessageUI _messageUI;

    [Inject]
    private void Construct(MessageUI messageUI)
    {
        _messageUI = messageUI;
    }

    private void Start()
    {
        _conditions = GetComponent<Conditions>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerCharacter>(out PlayerCharacter player))
        {
            if (_conditions != null)
            {
                ConditionCheckStruct conditionsMet = _conditions.ConditionsCheck();
                if (!conditionsMet.IsMet)
                {
                    _messageUI.ShowMessage(conditionsMet.Message);
                    return;
                }
            }
            SceneManager.LoadScene(_sceneToLoad);
        }
    }
}
