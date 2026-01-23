using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDoor : MonoBehaviour
{
    public ChangeScene changeScene;

    public void OpenTrapDoor()
    {
        gameObject.SetActive(false);
        changeScene.ChangeScenes();
    }

}
