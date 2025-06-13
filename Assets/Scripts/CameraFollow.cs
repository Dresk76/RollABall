using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject ball;
    private Vector3 offset;


    private void Start()
    {
        offset = transform.position - ball.transform.position;
    }

    // LateUpdate se ejecuta espues del frame del metodo Update
    private void LateUpdate()
    {
        transform.position = ball.transform.position + offset;
        //transform.position = new Vector3(ball.transform.position.x + offset.x, ball.transform.position.y + offset.y, offset.z);
    }
}
