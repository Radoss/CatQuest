using UnityEngine;
using Zenject;

public class UIInstaller : MonoInstaller
{
    [SerializeField] private GameObject _dialogUI;
    [SerializeField] private GameObject _storeUI;
    [SerializeField] private GameObject _messageUI;
    [SerializeField] private GameObject _questLogUI;
    [SerializeField] private GameObject _inventoryUI;

    public override void InstallBindings()
    {
        BindDialogUI();
        BindStoreUI();
        BindMessageUI();
        BindInventoryUI();
        BindQuestLogUI();
    }

    private void BindQuestLogUI()
    {
        Container.Bind<QuestLogUI>()
       .To<QuestLogUI>()
       .FromComponentOn(_questLogUI)
       .AsSingle();
    }

    private void BindInventoryUI()
    {
        Container.Bind<InventoryUI>()
        .To<InventoryUI>()
        .FromComponentOn(_inventoryUI)
        .AsSingle();
    }

    private void BindMessageUI()
    {
        Container.Bind<MessageUI>()
        .To<MessageUI>()
        .FromComponentOn(_messageUI)
        .AsSingle();
    }

    private void BindStoreUI()
    {
        Container.Bind<StoreUI>()
        .To<StoreUI>()
        .FromComponentOn(_storeUI)
        .AsSingle();
    }

    private void BindDialogUI()
    {
        Container.Bind<DialogUI>()
            .To<DialogUI>()
            .FromComponentOn(_dialogUI)
            .AsSingle();
    }
}
