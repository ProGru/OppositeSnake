using UnityEngine;
using System.Collections;

public class FruitCollectCommand : ICommand
{
    private Fruit _fruit;
    private OnFruitDrag _fruitDrag;
    private Node _node;

    public FruitCollectCommand(Fruit fruit)
    {
        _fruit = fruit;
        _node = fruit.gridPositionNode;
    }

    public void Execute()
    {
        _fruitDrag =  FruitManager.Instance.RetakeFruitFromGrid(_fruit);
    }

    public void Undo()
    {
        FruitManager.Instance.DragFruitToGrid(_fruit, _node);
        EventBroker.CallUndoStep();
        _fruitDrag.Destroy();
    }

}
