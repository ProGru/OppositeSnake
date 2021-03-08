using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Fruit : MonoBehaviour
{
    public Image image;
    public Node gridPositionNode;
    public string playerTag;
    public string snakeTag;

    private bool _draged;

    public void SetDragMode(bool state)
    {
        _draged = state;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (_draged) return;
        if (other.gameObject.CompareTag(playerTag))
        {
            ICommand command = new FruitCollectCommand(this);
            command.Execute();
            EventBroker.CallCommandHandler(command);
        }else if (other.gameObject.CompareTag(snakeTag))
        {
            ICommand command = new FruitEatCommand(this);
            command.Execute();
            EventBroker.CallCommandHandler(command);
        }
    }

}
