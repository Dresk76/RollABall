using System;
using UnityEngine;
using RollABall.Programming.Core;
using RollABall.Programming.Interfaces;

public class TrapDoor : MonoBehaviour, ITrapOpenAble
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
public void OnTrapOpen(bool status)
 {
    Debug.Log("My Status Is.. " + status);
 }
}
