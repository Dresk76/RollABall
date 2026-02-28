using UnityEngine;
using RollABall.Programming.Core.Events;

public class TrapDoor : MonoBehaviour
{
    [SerializeField] private int requiredKeys;
    private int _foundKey;

    private void OnEnable()
    {
        GameEvents.OnTrapDoorOpened += HandleOpenTrapDoor;
    }

    private void OnDisable()
    {
        GameEvents.OnTrapDoorOpened -= HandleOpenTrapDoor;
    }

    private void HandleOpenTrapDoor()
    {
        _foundKey ++;

        if (requiredKeys != _foundKey)
        {
            Debug.Log("Faltan llaves");
            return;
        }

        Debug.Log("Puerta abierta!");
        gameObject.SetActive(false);
        //changeScene.ChangeScenes();
    }
}
