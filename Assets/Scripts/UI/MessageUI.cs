using System.Collections;
using UnityEngine;
using TMPro;

public class MessageUI : MonoBehaviour
{
    [SerializeField] private GameObject _messageGO;
    [SerializeField] private TextMeshProUGUI _messageTMP;
    [SerializeField] private float _durationInSeconds;

    public void ShowMessage(string message) 
    {
        StartCoroutine(ShowMessageCoroutine(message));
    }

    private IEnumerator ShowMessageCoroutine(string message)
    {
        _messageTMP.text = message;
        _messageGO.SetActive(true);
        yield return new WaitForSeconds(_durationInSeconds);
        _messageGO.SetActive(false);
    }
}
