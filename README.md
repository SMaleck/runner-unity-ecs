# *runner-unity-ecs*
A reference implementation of a simple runner game, using Unity ECS

## **Unity Dependencies**
- Unity ECS
- Hybrid Render Pipeline

## **Custom Dependencies*
- Based on my [Unity Game Template](https://github.com/SMaleck/unity-game-facilities), which uses
    - UniRX
    - Zenject
    - DOTween

## **Reference Docs**
- [Unity ECS Manual](https://docs.unity3d.com/Packages/com.unity.entities@0.5/manual/index.html)
- [Converting your game to DOTS - Unite Copenhagen](https://www.youtube.com/watch?v=BNMrevfB6Q0)

## **Entity Authoring**
There are several currently valid approaches to author entities
>**NOTE:** At time of writing (*FEB 2019*), Unity still considers authoring an "area of active development", so this might change significantly.

### *1 - Simple Conversion*
See **Clouds** at: `Source.Features.EntitySpawning.Factories.CloudEntityFactory`

### *2 - IConvertGameObjectToEntity*
See **Obstacles** at: `Source.Features.EntitySpawning.Factories.ObstacleEntityFactory`

### *3 - Pure code*
See **Floor Tiles** at: `Source.Features.EntitySpawning.Factories.FloorTileEntityFactory`

### *4 - IConvert & Setting ComponentData in Code*
See **Player** at: `Source.Features.EntitySpawning.Factories.PlayerEntityFactory`
