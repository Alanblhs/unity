using UnityEngine;

public class FlameWeapon : MonoBehaviour
{
    public float rotateSpeed;

    public Transform holder, fireballToSpawn;

    public float timeBetweenSpawn;
    private float spawnCounter;

    
    void Start()
    {
        
    }

    
    void Update()
    {
        holder.rotation = Quaternion.Euler(0f, 0f, holder.rotation.eulerAngles.z + (rotateSpeed * Time.deltaTime));

        spawnCounter -= Time.deltaTime;

        if(spawnCounter <= 0)
        {
            spawnCounter = timeBetweenSpawn;

            Instantiate(fireballToSpawn, fireballToSpawn.position, fireballToSpawn.rotation, holder).gameObject.SetActive(true);
        }
    }
}
