# OppositeSnake
This not a full game only part of basic functions with no animations and graphics. Only contains some parts of UI.  
 It's a game where you are running from a snake - there you are a target.
 
## Table of content:
* [Level Loading](#level-loading)
* [Language support](#language-support)
* [Platform support](#platform-support)
* [Event Broker](#event-broker)
* [Player Controller](#player-controller)
* [Snake Controller](#snake-controller)
* [Map Grid](#map-grid)


### Level Loading
As default GameManager is using `AsynLevelLoader` to load Scene  [AsynLevelLoader](https://github.com/ProGru/OppositeSnake/blob/e2633fa1b2820c44339f534d9a436df812396aa4/Snake%20Prototype/Assets/Scripts/AsynLevelLoader.cs)  
You can implement [ILevelLoader](https://github.com/ProGru/OppositeSnake/blob/e2633fa1b2820c44339f534d9a436df812396aa4/Snake%20Prototype/Assets/Scripts/Interfaces/ILevelLoader.cs) for your own implementation.

To create level: right click -> Level
Rename this `Level` ScriptableObject as you like.  
Set this fields:
```C#    
public string levelName;
public List<Fruit> fruits;
public Level nextLevel;
public int fruitForBestResult;
```
Where:
* `levelName` name of scene to load.
* `fruits` list of fruits for player to use.
* `nextLevel` Level to load after this level completes.
* `fruitForBestResult` Number of fruits to get the best score.

To load this created Level call `Load()` on this ScriptableObject

### Language support

This game supports Polish and English.  
To create different language support i used Preview version of Localization package:  
com.unity.localization@0.10.0-preview

### Platform support
This game is multiplatform.  
To create this game multiplatform i used new Unity Input System Package:  
com.unity.inputsystem@1.0.2

### Event Broker
`EventBroker` is a place where all of the Events are called and subscribed.
* When a new scene is loaded, 'OnSceneLoadStart' is called.
* After the scene is fully loaded `OdSceneLoadComplete` is called.
* After the player uses `MoveCommand` the `PlayerMoveHandler` is called.
* After the snake makes its move `SnakeMoveHandler` is called.
* When `SnakeMultiStepCommand` or `FruitEatCommand` or `FruitCollectCommand` is `Undo` the `UndoStep` is called.
* When a player Win `WindHandler` is called.
* When a player Loses `GameOverHandler` is called.

### Player Controller
All player moves are implementing [ICommand](https://github.com/ProGru/OppositeSnake/blob/e2633fa1b2820c44339f534d9a436df812396aa4/Snake%20Prototype/Assets/Scripts/Interfaces/ICommand.cs) interface  
Basic Player Movement is controlled by [PlayerController](https://github.com/ProGru/OppositeSnake/blob/e2633fa1b2820c44339f534d9a436df812396aa4/Snake%20Prototype/Assets/Scripts/Controllers/PlayerController.cs)  
To basic player commands belong:
* [FruitCollectCommand](https://github.com/ProGru/OppositeSnake/blob/e2633fa1b2820c44339f534d9a436df812396aa4/Snake%20Prototype/Assets/Scripts/Commands/FruitCollectCommand.cs) Collecting fruit back from map grid.
* [FruitDragCommand](https://github.com/ProGru/OppositeSnake/blob/e2633fa1b2820c44339f534d9a436df812396aa4/Snake%20Prototype/Assets/Scripts/Commands/FruitDragCommand.cs) Drag fruit from UI to map grid.
* [MoveCommand](https://github.com/ProGru/OppositeSnake/blob/e2633fa1b2820c44339f534d9a436df812396aa4/Snake%20Prototype/Assets/Scripts/Commands/MoveCommand.cs) Move player by given vector (You should use only Vector3.left, Vector3.right, Vector3.forward, Vector3.back for it to work on grid)

All `ICommand`'s should be added to commandList in `CommandManager` using `AddCommand(ICommand command)`  

#### RedoStepBack
Using `RedoStepBack()` you can Undo last Command. (This function Undo SnakMoves and its consequences that happens automatically)

### Snake Controller
This is the place where all magic happens.  
After Player Move the `MakeSnakeMove` is called and the snake makes it move.  
Before Snake Move there is a check for Game Over using the `CheckForGameOVer` method.  
Snake Movement is executed using `SnakeMoveExecutor` !! keep in mind that if the snake makes more than one stepAtOnce and the player makes his move, the snake can be too slow to execute all commands !!  
Game support Multiple snakes per lvl but only one player.

### Map Grid
Fruits, Player, Obstacles and snakes are on [GridMap](https://github.com/ProGru/OppositeSnake/blob/e2633fa1b2820c44339f534d9a436df812396aa4/Snake%20Prototype/Assets/Scripts/GridMap.cs).  
To create GridMap use Constructor:
```C# 
public GridMap(int _mapSizeX, int _mapSizeZ, float _fieldSize, Vector3 _startPoint, LayerMask _obstacleMask = default)
```
Where:
* _mapSizeX is X size of grid.
* _mapSizeZ is Z size of grid 
* _fieldSize is size of one grid
* _startPoint is point where the grid will start 
* _obstacleMask is a mask of elements that will be set as obstacles and mark as unwalkable.

To each grid there is a corresponding Node if on passed LayerMask there are elements in Node this node will be unwalkable.
(Walkable checking is checked all way up and down in Vector2 in Node)

!!!! If you want to see the grid and snake path in the Editor: !!!
* Put `DrawGridMap` on the Scene.
* Inject GridMap using Zenject.
* pass Treanforms of player and snake.
* set List of Nodes to path variables.

### Snake Path Finding 
By default snake is using variation of A* pathfinding
If you want, use your own implementation of `IPathFinder` and set it using the Start method in `SnakeController`.  



