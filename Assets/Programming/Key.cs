using UnityEngine;
using RollABall.Programming.Interfaces;
using RollABall.Programming.Core;

public class Key : MonoBehaviour
{
    [SerializeField] private float harvestTime = 0.1f;
    public TrapDoor trapDoor;
    private const int KeyValue = 1;


    private void OnTriggerEnter(Collider other) 
    {
        Destroy(gameObject, harvestTime);

        // Pasar evento de llave recuperada por Interface
        var doorOpened = other.GetComponent<IKeyRecovered>();
        doorOpened?.OnKeyRecovered(KeyValue); // ?. Si es != null
        
        // Pasar evento de llave recuperada por EventManager
        // EventManager.KeyRecovered(KeyValue);
    }
}