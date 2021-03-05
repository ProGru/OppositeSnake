using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FruitManager : Singleton<FruitManager>
{
    private Level _settings;
    private List<Fruit> fruitsToUse;
    private FruitPanel _currentFruitPanel;
    public Level Settings
    {
        get { return _settings; }
        set
        {
            _settings = value;
            ResetFruits();
        }
    }
    public List<Fruit> fruitsOnMap;
    public int fruitCount;
    public GameObject fruidUIElement;

    private void ResetFruits()
    {
        _currentFruitPanel = FindObjectOfType<FruitPanel>();
        fruitsToUse = _settings.fruits;
        foreach(Fruit fruit in fruitsToUse)
        {
            GameObject fruitEL = Instantiate(fruidUIElement, _currentFruitPanel.gameObject.transform);
            OnFruitDrag fruitDrag = fruitEL.GetComponent<OnFruitDrag>();
            fruitDrag.fruit = Instantiate(fruit);
        }

        fruitCount = fruitsToUse.Count;
    }

    public void DragFruitToGrid(Fruit fruit, Node node)
    {
        node.haveFruit = true;
        fruit.gridPositionNode = node;
        fruitsOnMap.Add(fruit);
    }
}
