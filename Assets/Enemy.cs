using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public int                  maxHealth = 100;
    public int                   currentHealth;
    public Animator            m_animator;
    [SerializeField] bool       m_noBlood = false;
    public HealthBar            healthBar;
    private Rigidbody2D         m_body2d;
    private bool                finished;
    public int                 m_facingDirection = 1;
    private int                 m_currentAttack = 0;
    private float               m_timeSinceAttack = 0.0f;
    private Transform           m_swordHitBox;
    public LayerMask            enemyLayer;
    private int                 attackDamage = 2;
    private int                 mode;
    private float                m_knockback = 2.0f;

    void Start()
    {
        currentHealth = maxHealth;
        m_animator = GetComponent<Animator>();
        m_swordHitBox = transform.Find("SwordHitBox").transform;
        healthBar.SetMaxHealth(maxHealth); 
        finished = false;
        GetComponent<SpriteRenderer>().flipX = false;
        // m_animator.SetTrigger("Block");
        m_animator.SetBool("IdleBlock", false);
        m_body2d = GetComponent<Rigidbody2D>();
        mode = Random.Range(0,2);
    }

    void Update() {

        m_timeSinceAttack += Time.deltaTime;
        m_knockback += Time.deltaTime;
        
        // Debug.Log(m_knockback);

        if (m_knockback < 0.5f ) {
            Debug.Log("reached");
            m_body2d.isKinematic = true;
            return;
        }
        
        m_body2d.isKinematic = false;
        
        m_body2d.velocity = new Vector2(0, m_body2d.velocity.y);
        
        // Debug.Log(mode);
// /*
        if (mode == 0) {
            m_animator.SetTrigger("Block");
            m_animator.SetBool("IdleBlock", true);
        } else {
            
            Collider2D enemy = Physics2D.OverlapBox(m_swordHitBox.position, m_swordHitBox.localScale, 0.0f, enemyLayer);
            
            if (enemy && !finished) {

                if(m_timeSinceAttack > 0.25f) {
                    m_currentAttack++;


                    // Loop back to one after third attack
                    if (m_currentAttack > 3)
                        m_currentAttack = 1;

                    // Reset Attack combo if time since last attack is too large
                    if (m_timeSinceAttack > 1.0f)
                        m_currentAttack = 1;

                            
                            
                            // if (enemy.m_facingDirection == m_facingDirection) {
                            //     m_facingDirection *= -1;
                            //     GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
                            // }
                    
                            Debug.Log("We hit " + enemy.name);
                            Player1 h = GetComponent<Player1>();
                            HeroKnightActions e = h.actions;
                            if (e.m_animator.GetBool("IdleBlock") && e.m_facingDirection != m_facingDirection) {
                                m_animator.SetTrigger("Hurt");
                                knockback();
                                return;
                            } else {
                                e.TakeDamage(attackDamage);
                                e.m_rolling = false;
                                e.m_animator.SetBool("IdleBlock", false);
                            }
                            
                        

                    
                    // Call one of three attack animations "Attack1", "Attack2", "Attack3"
                    m_animator.SetTrigger("Attack" + m_currentAttack);
                    // Reset timer
                    m_timeSinceAttack = 0.0f;
                }
            }
        
        }
        
// */
        
    }

    public void knockback() {
        m_body2d.velocity = new Vector2(m_facingDirection*-5, 1);
        m_knockback = 0.0f;
    }

    public void TakeDamage(int damage) {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if (currentHealth > 0)
            m_animator.SetTrigger("Hurt");
        if (currentHealth <= 0 && !finished) {
            m_animator.SetBool("noBlood", m_noBlood);
            m_animator.SetTrigger("Death");
            // GetComponent<PlatformEffector2D>().enabled = true;
            this.enabled = false;
            finished = true;
        }

        mode = Random.Range(0,2);


    }

    
}
