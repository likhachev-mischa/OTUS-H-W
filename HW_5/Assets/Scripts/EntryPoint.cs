using DI;
using GameEngine;
using UnityEngine;

//TODO: Удалить этот класс!
//Развернуть архитектуру на Zenject/VContainer/Custom
public sealed class EntryPoint : MonoBehaviour
{
    
    private UnitManager unitManager;
    private ResourceService resourceService;

    [Inject]
    private void Construct(UnitManager unitManager,ResourceService resourceService)
    {
        this.unitManager = unitManager;
        this.resourceService = resourceService;
    }
    
    private void Start()
    {
        this.unitManager.SetupUnits(FindObjectsOfType<Unit>());
        this.resourceService.SetResources(FindObjectsOfType<Resource>());
    }
}