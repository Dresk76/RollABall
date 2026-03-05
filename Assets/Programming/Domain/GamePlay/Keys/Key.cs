using UnityEngine;
using RollABall.Programming.GamePlay.Interfaces;
using RollABall.Programming.Core.Events;

public class Key : MonoBehaviour
{
    [SerializeField] private float harvestTime = 0.1f;
    public TrapDoor trapDoor;
    private const int KeyValue = 1;


    private void OnTriggerEnter(Collider other) 
    {
        Destroy(gameObject, harvestTime);

        // Pasar evento de llave recuperada por Interface
        var keyRecovered = other.GetComponent<IKeyRecovered>();
        keyRecovered?.OnKeyRecovered(KeyValue); // ?. Si es != null

        // Indicar que se recolecto una llave
        GameEvents.RaiseTrapDoorOpened();
    }
}