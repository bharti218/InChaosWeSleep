using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform spellAttackPoint;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] ParticleSystem spell;
    [SerializeField] private Transform meleeAttackPoint;
    [SerializeField] private Slider slider;
    [SerializeField] AudioSource ghostMoan;
    [SerializeField] ParticleSystem bloodParticleSystem;
    [SerializeField] Transform maxXPos;

    private float moveSpeed = 5f;
    private float dirX;
    private bool facingRight = true;
    private Vector3 localScale;
    private bool isAttacking = false;
    private GameMaster gm;
    private int maxHealth = 100;
    private int currentHealth;

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        transform.position = gm.lastCheckpointPos;
        localScale = transform.localScale;
        spell.Stop();
        currentHealth = maxHealth;
        slider.value = slider.maxValue;

    }
    private void Update()
    {
        // ================= Contols for andriod =================== //
        
            dirX = CrossPlatformInputManager.GetAxis("Horizontal") * moveSpeed;
       

        if (CrossPlatformInputManager.GetButtonDown("Jump") && rb.velocity.y == 0)
            rb.AddForce(Vector2.up * 300f);


        if (CrossPlatformInputManager.GetButtonDown("Attack"))
            SpellAttack();

        if (CrossPlatformInputManager.GetButtonDown("melee"))
            MeleeAttack();
        // =================== // 

        // =========== Controls for pc ================ //
        
            dirX = Input.GetAxisRaw("Horizontal") * moveSpeed;

        if (Input.GetButtonDown("Jump") && rb.velocity.y == 0)
            rb.AddForce(Vector2.up * 300f);

        if (Input.GetKeyDown("x"))
            SpellAttack();

        if (Input.GetKeyDown("c"))
            MeleeAttack();
        // =============== //

        animator.SetFloat("speed", Mathf.Abs(dirX));

        if (rb.velocity.y == 0)
            animator.SetBool("isJumping", false);
        else if (rb.velocity.y > 0)
            animator.SetBool("isJumping", true);


    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(dirX, rb.velocity.y);
        if (isAttacking)
        {
            DamageEnemyBySpell();
        }
    }

    private void LateUpdate()
    {
        if (dirX > 0)
            facingRight = true;
        else if (dirX < 0)
            facingRight = false;
        if ((facingRight && localScale.x < 0) || (!facingRight && localScale.x > 0))
            localScale.x *= -1;
        transform.localScale = localScale;


    }

    void SpellAttack()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            spell.gameObject.SetActive(true);
            spell.Play();
            animator.SetTrigger("attack");
            Invoke("DisableSpell", .8f);
        }
    }

   
    void MeleeAttack()
    {
        animator.SetTrigger("melee");
        DamageEnemyByMelee();
    }

    void DamageEnemyBySpell()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(spellAttackPoint.position, attackRange, enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.GetComponent<EnemyController>())
            {
                enemy.GetComponent<EnemyController>().TakeDamage(20,transform.position);
                ghostMoan.Play();
            }

            if (enemy.GetComponent<MiniBossAttackingRocks>())
            {
                enemy.GetComponent<MiniBossAttackingRocks>().DamageRock();
            }
            if (enemy.GetComponent<Level01MiniBoss>())
            {
                enemy.GetComponent<Level01MiniBoss>().TakeDamage();
            }
        }
    }

    void DisableSpell()
    {
        isAttacking = false;
        spell.Stop();
        spell.gameObject.SetActive(false);
    }
    void DamageEnemyByMelee()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(meleeAttackPoint.position, attackRange, enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.GetComponent<EnemyController>())
            {
                enemy.GetComponent<EnemyController>().TakeDamage(20, transform.position);
                ghostMoan.Play();
            }

            if (enemy.GetComponent<MiniBossAttackingRocks>())
            {
                enemy.GetComponent<MiniBossAttackingRocks>().DamageRock();
            }
            if (enemy.GetComponent<Level01MiniBoss>())
            {
                enemy.GetComponent<Level01MiniBoss>().TakeDamage();
            }

        }
    }

    public void ReduceHealth(int damage)
    {
        currentHealth -= damage;
        slider.value = currentHealth;
        GameObject blood = Instantiate(bloodParticleSystem.gameObject);
        blood.gameObject.transform.position = transform.position;
        if (currentHealth<=0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SetFullHealth()
    {
        currentHealth = maxHealth;
        slider.value = currentHealth;
    }
}
