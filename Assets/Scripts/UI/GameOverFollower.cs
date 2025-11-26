using UnityEngine;

public class GameOverFollower : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private PlayerHealth playerHealth;

    void Update()
    {
        if (playerTransform == null || playerHealth == null) return;

        if (!playerHealth.IsDead())
        {
            
            transform.position = playerTransform.position + new Vector3(0, 0, -1); 
        }
        
    }
}
