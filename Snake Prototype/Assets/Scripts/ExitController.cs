using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class ExitController : MonoBehaviour
{

    public string winingTag;

    private void Start()
    {
        EventBroker.WinHandler += Win;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(winingTag))
            EventBroker.CallWin();
    }

    public void Win()
    {
        Debug.Log("Good Job!");
    }
}
