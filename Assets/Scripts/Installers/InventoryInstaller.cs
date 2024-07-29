using UnityEngine;
using Zenject;

public class InventoryInstaller : MonoInstaller
{
    [SerializeField] private int _numOfSlots;
    public override void InstallBindings()
    {
        BindInventory();
    }

    private void BindInventory()
    {
        Container.Bind<Inventory>()
            .FromInstance(new Inventory(_numOfSlots))
            .AsSingle()
            .NonLazy();
    }
}