using UnityEngine;
using RollABall.Programming.Core;

public class TrapDoor : MonoBehaviour
{
    public ChangeScene changeScene;
    [SerializeField] private int requiredKeys;
    private int _foundKey;

    private void OnEnable()
    {
        EventManager.OnOpenTrapDoor += OnOpenTrapDoor;
    }

    private void OnDisable()
    {
        EventManager.OnOpenTrapDoor -= OnOpenTrapDoor;
    }

    private void OnOpenTrapDoor()
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
