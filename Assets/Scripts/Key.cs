using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] private float harvestTime = 0.1f;
    public TrapDoor trapDoor;


    private void OnTriggerEnter(Collider other) 
    {
        Destroy(gameObject, harvestTime);
        trapDoor.OpenTrapDoor();
    }
}