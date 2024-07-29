using UnityEngine;
using Zenject;

public class ProjectilePoolInstaller : MonoInstaller
{
    [SerializeField] private Transform _prefab;
    [SerializeField] private Transform _parent;
    [SerializeField] private int _count;
    public override void InstallBindings()
    {
        BindPool();
    }

    private void BindPool()
    {
        Container.Bind<ProjectilePool>()
            .FromInstance(new ProjectilePool(_prefab, _parent, _count))
            .AsSingle()
            .NonLazy();
    }
}
