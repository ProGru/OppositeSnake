using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitDragCommand : ICommand
{

    private Fruit _fruit;
    private Node _node;
    private OnFruitDrag _fruitDrag;

    public FruitDragCommand(Fruit fruit, Node fruitNodeOn, OnFruitDrag fruitDrag)
    {
        _fruit = fruit;
        _node = fruitNodeOn;
        _fruitDrag = fruitDrag;
    }

    public void Execute()
    {
        FruitManager.Instance.DragFruitToGrid(_fruit, _node);
    }

    public void Undo()
    {
        FruitManager.Instance.RetakeFruitFromGrid(_fruit, _fruitDrag);
    }


}
