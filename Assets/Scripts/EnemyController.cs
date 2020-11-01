using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;

    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rigidbody;
    [SerializeField] Sprite landingSprite;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] ParticleSystem skullExplosion;
    
    private float dirX = -1f;
    private float moveSpeed = 2f;
    private Sprite defaultSprite;

    private void Start()
    {
        currentHealth = maxHealth;
        InvokeRepeating("EnemyJump", 2f, 5f);
        defaultSprite = spriteRenderer.sprite;
        // animator = GetComponent<Animator>();
    }


    private void FixedUpdate()
    {
        float speed = Random.Range(2, 4);
        rigidbody.velocity = new Vector2(dirX * speed, rigidbody.velocity.y);
    }

    private void EnemyJump()
    {
        //animator.SetTrigger("jumping");
        rigidbody.AddForce(Vector2.up * 300f);
    }
    
    public void TakeDamage(int damage, Vector3 skullExplostionPosition)
    {
        GameObject skull = Instantiate(skullExplosion.gameObject);
        skull.transform.position = transform.position;
        skull.GetComponent<ParticleSystem>().Play();
        this.gameObject.SetActive(false);
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("ground"))
            spriteRenderer.sprite = landingSprite;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            
            collision.gameObject.GetComponent<PlayerController>().ReduceHealth(10);
        }
    }   

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("ground"))
            spriteRenderer.sprite = defaultSprite;
    }
}
