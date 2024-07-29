using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DialogUI : MonoBehaviour
{
    [SerializeField] private GameObject _UIDialogWindow;
    [SerializeField] private TextMeshProUGUI  _dialogText;
    [SerializeField] private Button  _acceptButton;
    public bool IsDialogActive { get { return _UIDialogWindow.activeSelf; } }

    public void ToggleDialog(bool val)
    {
        _UIDialogWindow.SetActive(val);
        if (val == true)
        {
            FitAllTextInTheDialog();
        }
    }

    public void ShowDialogText(string text)
    {
        _acceptButton.gameObject.SetActive(false);
        _dialogText.text = text;
        ToggleDialog(true);
    }

    public void ShowDialogQuest(string text, Action OnAcceptButtonClicked)
    {
        _acceptButton.gameObject.SetActive(true);
        _dialogText.text = text;
        _acceptButton.onClick.RemoveAllListeners();
        _acceptButton.onClick.AddListener(() => OnButtonClicked(OnAcceptButtonClicked));
        ToggleDialog(true);
    }

    public void OnButtonClicked(Action OnAcceptButtonClicked)
    {
        OnAcceptButtonClicked?.Invoke();
        ToggleDialog(false);
        _acceptButton.onClick.RemoveAllListeners();
    }

    private void FitAllTextInTheDialog()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(
            _UIDialogWindow.GetComponent<RectTransform>()
            );
    }
}
