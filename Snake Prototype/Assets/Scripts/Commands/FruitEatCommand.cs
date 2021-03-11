using UnityEngine;
using System.Collections;

public class FruitEatCommand : ICommand
{
    private Fruit _fruit;

    public FruitEatCommand(Fruit fruit)
    {
        _fruit = fruit;
    }

    public void Execute()
    {
        FruitManager.Instance.RemoveFruitFromGrid(_fruit);
        _fruit.gameObject.SetActive(false);
    }

    public void Undo()
    {
        _fruit.gameObject.SetActive(true);
        FruitManager.Instance.AddFruitToGrid(_fruit);
        EventBroker.CallUndoStep();
    }

    public bool CanExecute()
    {
        return true;
    }
}
