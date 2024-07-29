using System;
using UnityEngine;
using Zenject;
using static UnityEngine.EventSystems.EventTrigger;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _playerInput;
    [SerializeField] private GameObject _mobileInput;
    [SerializeField] private GameObject _questList;
    [SerializeField] private GameObject _dataPersistenceManager;

    public override void InstallBindings()
    {
        BindWallet();
        BindInput();
        BindPlayer();
        BindPlayerHealth();
        BindQuestDialog();
        BindQuestList();
        BindExpirienceGetter();
        BindNewGameStarter();
        BindNewCurrentGame();
    }

    private void BindNewCurrentGame()
    {
        Container.Bind<CurrentGame>()
        .FromInstance(new CurrentGame(GameState.InProgress, _questList.GetComponent<QuestList>()))
        .AsSingle()
        .NonLazy();
    }

    private void BindNewGameStarter()
    {
        Container.Bind<INewGameStarter>()
       .To<DataPersistenceManager>()
       .FromComponentOn(_dataPersistenceManager)
       .AsSingle();
    }

    private void BindQuestList()
    {
        Container.Bind<QuestList>()
        .To<QuestList>()
        .FromComponentOn(_questList)
        .AsSingle();
    }

    private void BindExpirienceGetter()
    {
        Container.Bind<ExperienceGetter>()
        .To<ExperienceGetter>()
        .FromComponentOn(_player)
        .AsSingle();
    }

    private void BindQuestDialog()
    {
        Container.Bind<QuestDialog>()
        .FromInstance(new QuestDialog(Container))
        .AsSingle()
        .NonLazy();

        //Container.BindFactory<QuestDialog, QuestDialog.Factory>();
    }

    private void BindWallet()
    {
        Container.Bind<Wallet>()
        .To<Wallet>()
        .FromComponentOn(_player)
        .AsSingle();
    }

    private void BindPlayerHealth()
    {
        Container.Bind<IHealth>()
            .To<Health>()
            .FromComponentOn(_player)
            .AsSingle();
    }

    private void BindPlayer()
    {
        Container.Bind<PlayerCharacter>()
            .ToSelf()
            .FromComponentOn(_player)
            .AsSingle();
    }

    private void BindInput()
    {
        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            Container.Bind<IInput>()
                .To<PlayerMobileInput>()
                .FromComponentOn(_mobileInput)
                .AsSingle();   
            
            _mobileInput.GetComponent<PlayerMobileInput>().ActivateMobileInput();
        }
        else
        {
            Container.Bind<IInput>()
                .To<NewPlayerInput>()
                .FromComponentOn(_playerInput)
                .AsSingle();
        }
    }
}
