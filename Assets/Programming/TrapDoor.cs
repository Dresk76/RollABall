using System;
using UnityEngine;
using RollABall.Programming.Core;

public class TrapDoor : MonoBehaviour
{
    public ChangeScene changeScene;

    private void OnEnable()
    {
        EventManager.OnOpenTrapDoor += OnOpenTrapDoor;
    }

    private void OnDisable()
    {
        EventManager.OnOpenTrapDoor -= OnOpenTrapDoor;
    }

    private void OnOpenTrapDoor(bool state)
    {
        gameObject.SetActive(state);
        //changeScene.ChangeScenes();
    }

}
