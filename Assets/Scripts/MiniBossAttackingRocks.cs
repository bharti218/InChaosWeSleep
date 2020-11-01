using UnityEngine;

public class MiniBossAttackingRocks : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] int damage;
    [SerializeField] GameObject stoneCfx;
    [SerializeField] AudioSource stoneSound;
    void Start()
    {
        rb.AddForce(new Vector2(-600f, 200));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        stoneSound.Play();
        if (collision.gameObject.tag.Equals("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().ReduceHealth(damage);
        }

        if (collision.gameObject.tag.Equals("ground"))
        {
            DamageRock();
        }
    }
    public void DamageRock()
    {
        GameObject cfx = Instantiate(stoneCfx);
        cfx.transform.position = transform.position;
        Destroy(gameObject);
    }

}
