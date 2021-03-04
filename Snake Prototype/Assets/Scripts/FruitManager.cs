using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FruitManager : Singleton<FruitManager>
{
    private Level _settings;
    private List<Fruit> fruitsToUse;
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

    private void ResetFruits()
    {
        fruitsToUse = _settings.fruits;
        fruitCount = fruitsToUse.Count;
    }
}
