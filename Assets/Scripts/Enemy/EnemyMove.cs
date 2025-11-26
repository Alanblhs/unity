using UnityEngine;
using System.Collections;

public class EnemyMove : MonoBehaviour
{
    public Rigidbody2D theRigidbody;
    public float moveSpeed = 1f;
    public float damage = 1f;

    private Transform target;
    private Animator animator;

    public float hitWaitTime = 0.5f;
    private float hitCounter;

    public float health = 10f; 
    public float knockBackTime = 0.5f;
    private float knockBackCounter;

    public GameObject expPickupPrefab;
    public int expValue = 5;

    public GameObject healthPickupPrefab;
    [Range(0f, 1f)] public float healthDropChance = 0.2f;

    public bool isDead = false;

    [Header("Audio")]
    [SerializeField] private AudioSource hitAudioSource;

    void Awake()
    {
        theRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        var player = Object.FindFirstObjectByType<PlayerMove>();
        if (player != null)
        {
            target = player.transform;
        }
        else
        {
            Debug.LogWarning("No se encontró el jugador. El enemigo no tiene target.");
        }

        moveSpeed = Random.Range(moveSpeed * 0.8f, moveSpeed * 1.2f);
    }

    void Update()
    {
        if (knockBackCounter > 0)
        {
            knockBackCounter -= Time.deltaTime;

            if (moveSpeed > 0)
            {
                moveSpeed = -moveSpeed * 2f;
            }
            if (knockBackCounter <= 0)
            {
                moveSpeed = Mathf.Abs(moveSpeed * 0.5f);
            }
        }
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            theRigidbody.linearVelocity = direction * moveSpeed;

            animator.SetBool("Move", direction != Vector2.zero);
            transform.localScale = new Vector3(direction.x > 0 ? 1 : -1, 1, 1);
        }

        if (hitCounter > 0f)
        {
            hitCounter -= Time.deltaTime;
        }

        if (MapBounds.instance != null)
        {
            Vector3 pos = transform.position;
            pos.x = Mathf.Clamp(pos.x, MapBounds.instance.minBounds.x, MapBounds.instance.maxBounds.x);
            pos.y = Mathf.Clamp(pos.y, MapBounds.instance.minBounds.y, MapBounds.instance.maxBounds.y);
            transform.position = pos;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && hitCounter <= 0f)
        {
            if (PlayerHealth.instance != null)
            {
                PlayerHealth.instance.TakeDamage(damage);
            }
            else
            {
                Debug.LogWarning("PlayerHealth.instance es null. No se puede aplicar daño.");
            }

            hitCounter = hitWaitTime;
        }
    }

    public void TakeDamage(float damageToTake)
    {
        if (isDead) return;

        health -= damageToTake;

        
        if (hitAudioSource != null)
        {
            hitAudioSource.Play();
        }

        DamageNumberController.instance.SpawnDamage(damageToTake, transform.position);

        if (health <= 0)
        {
            isDead = true;
            StartCoroutine(HandleDeath()); 
        }
    }

    public void TakeDamage(float damageToTake, bool shouldKnockBack)
    {
        TakeDamage(damageToTake);

        if (shouldKnockBack && !isDead)
        {
            knockBackCounter = knockBackTime;
        }
    }

    private IEnumerator HandleDeath()
    {
        yield return new WaitForSeconds(0.1f); 

        EnemySpawner.RegistrarMuerte();

        if (expPickupPrefab != null)
        {
            GameObject drop = Instantiate(expPickupPrefab, transform.position, Quaternion.identity);
            drop.GetComponent<ExperiencePickup>().expValue = expValue;
        }

        if (healthPickupPrefab != null && Random.value < healthDropChance)
        {
            Instantiate(healthPickupPrefab, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
