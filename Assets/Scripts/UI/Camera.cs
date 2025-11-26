using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    private Transform target;

    void Start()
    {
        var player = Object.FindFirstObjectByType<PlayerMove>();
        if (player != null)
        {
            target = player.transform;
        }
        else
        {
            Debug.LogWarning("No se encontró el jugador para seguir con la cámara.");
        }
    }

    void LateUpdate()
    {
        if (target != null)
        {
            transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
        }
    }
}
