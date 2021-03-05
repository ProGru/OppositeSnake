using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Zenject;

public class OnFruitDrag : MonoBehaviour, IDragHandler, IEndDragHandler
{

    public UnityEvent onDrag;
    public UnityEvent onEndDrag;
    public Fruit fruit;

    private Node _currentNode;

    private void Start()
    {
        fruit.gameObject.SetActive(false);
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        _currentNode = MouseInputsManager.Instance.GetNodeGridPostion();
        onDrag.Invoke();
        if (_currentNode == null) return;
        fruit.gameObject.SetActive(true);
        if (_currentNode.walkable)
        {
            fruit.transform.position = _currentNode.wordlPosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        onEndDrag.Invoke();
        if (_currentNode == null)
        {
            fruit.gameObject.SetActive(false);
            return;
        }
        FruitManager.Instance.DragFruitToGrid(fruit,_currentNode);
        Destroy(this.gameObject);
    }
}
