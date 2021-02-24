using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class HeroKnight : MonoBehaviour {

    [SerializeField] float      m_speed = 4.0f;
    [SerializeField] float      m_jumpForce = 7.5f;
    [SerializeField] float      m_rollForce = 6.0f;
    [SerializeField] bool       m_noBlood = false;
    [SerializeField] GameObject m_slideDust;
    public LayerMask            enemyLayer;
    public HealthBar            healthBar;

    public Animator            m_animator;
    private Rigidbody2D         m_body2d;
    private BoxCollider2D       m_hitbox;
    private Sensor_HeroKnight   m_groundSensor;
    private bool                m_grounded = false;
    public bool                m_rolling = false;
    private bool                m_crouch = false;
    public int                 m_facingDirection = 1;
    private int                 m_currentAttack = 0;
    private float               m_timeSinceAttack = 0.0f;
    private float               m_delayToIdle = 0.0f;
    private int                 attackDamage = 15;
    private Transform           m_swordHitBox;
    public int                  maxHealth = 100;
    public int                   currentHealth;
    private bool                finished;
    private float                m_knockback = 2.0f;
    public float sword_height;
    // private bool                m_attacking = false;


    // Use this for initialization
    void Start ()
    {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_hitbox = GetComponent<BoxCollider2D>();
        m_swordHitBox = transform.Find("SwordHitBox").transform;
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_HeroKnight>();
        healthBar.SetMaxHealth(maxHealth); 
        finished = false;
        currentHealth = maxHealth;
        sword_height = 1.05f;
    }

    

    // Update is called once per frame
    void Update ()
    {
        // Debug.Log(m_body2d.position);
        if (m_body2d.position.y <= -5.0f) {
            currentHealth = 0;
            healthBar.SetHealth(currentHealth);
        }


        Collider2D enemy = Physics2D.OverlapBox(m_swordHitBox.position, m_swordHitBox.localScale, 0.0f, enemyLayer);
        
        

        // Increase timer that controls attack combo
        m_timeSinceAttack += Time.deltaTime;
        m_knockback += Time.deltaTime;
        
        adjustSwordHeight();

        checkGrounded();

        checkFalling();

        if (m_rolling || m_knockback < 0.5f ) {
            m_body2d.isKinematic = true;
            return;
        }

        m_body2d.isKinematic = false;
/* move*/
        // -- Handle input and movement --
        float inputX = Input.GetAxis("Horizontal");

        // Swap direction of sprite depending on walk direction
        faceDirection(inputX);

        // Move
        if (!m_rolling && m_timeSinceAttack > 0.25f){
            // Debug.Log(m_knockback);
            m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);
        }
        /*dont move*/
        
        //Set AirSpeed in animator
        m_animator.SetFloat("AirSpeedY", m_body2d.velocity.y);

        //Death
        if (Input.GetKeyDown("e"))
        {
            m_animator.SetBool("noBlood", m_noBlood);
            m_animator.SetTrigger("Death");
        }
            
        //Hurt
        else if (Input.GetKeyDown("q"))
            m_animator.SetTrigger("Hurt");

        // Block
        else if (Input.GetMouseButtonDown(1) && !m_animator.GetBool("Crouch"))
        {
            block();
        }

        else if (Input.GetMouseButtonUp(1)){
            unblock();
        }
        
        else if (!m_animator.GetBool("IdleBlock")) {

            //Attack
            if(Input.GetMouseButtonDown(0) && m_timeSinceAttack > 0.25f)
            {
                attack(enemy);
                // return;
            }
            
            //Crouch
            else if (Input.GetKeyDown("s") && m_grounded) {
                crouch();
                    
            }
            else if (Input.GetKeyUp("s")) {
                uncrouch();
            }

            // Roll
            else if (Input.GetKeyDown("left shift") && !m_rolling)
            {
                roll();
            }

            else if (Input.GetKeyDown("left shift")) {
                uncrouch();
            }
                

            //Jump
            else if (Input.GetKeyDown("space") && m_grounded)
            {
                jump();    
            }

            //Run
            else if (Mathf.Abs(inputX) > Mathf.Epsilon)
            {
                // Reset timer
                m_delayToIdle = 0.05f;
                m_animator.SetInteger("AnimState", 1);
            }
            

            //Idle
            else
            {
                // Prevents flickering transitions to idle

                m_delayToIdle -= Time.deltaTime;
                if(m_delayToIdle < 0)
                    m_animator.SetInteger("AnimState", 0);
                // m_rolling = false;
                
            }

        }
        
        // if grounded and blocking or crouching don't move
        if (m_grounded && (m_animator.GetBool("IdleBlock") || m_animator.GetBool("Crouch") )){
            // Debug.Log(m_animator.GetBool("Crouch") );
            m_body2d.velocity = new Vector2(0, m_body2d.velocity.y);
        }

            
    }

    public void knockback() {
        m_body2d.velocity = new Vector2(m_facingDirection*-5, 1);
        m_knockback = 0.0f;
    }

    public void jump() {
        m_animator.SetTrigger("Jump");
        m_grounded = false;
        m_animator.SetBool("Grounded", m_grounded);
        m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
        m_groundSensor.Disable(0.2f);
    }

    public void roll() {
        m_rolling = true;
        m_animator.SetTrigger("Roll");
        m_hitbox.offset = new Vector2(0.0f, 0.462f);
        m_hitbox.size = new Vector2(0.73f, 0.8f);
        m_body2d.velocity = new Vector2(m_facingDirection * m_rollForce, m_body2d.velocity.y);
    }

    public void crouch() {
        m_crouch = true;
        m_animator.SetBool("Crouch", true);
        m_hitbox.offset = new Vector2(0.0f, 0.462f);
        m_hitbox.size = new Vector2(0.73f, 0.8f);
        if (m_facingDirection == 1) {
            m_swordHitBox.localPosition = new Vector3(0.7f, 0.65f, 0.0f);
        } else {
            m_swordHitBox.localPosition = new Vector3(-0.5f, 0.65f, 0.0f);
        }
    }

    public void uncrouch() {
        m_crouch = false;
        m_animator.SetBool("Crouch", false);
        m_hitbox.offset = new Vector2(0.0f, 0.662f);
        m_hitbox.size = new Vector2(0.73f, 1.2f);
        if (m_facingDirection == 1) {
            m_swordHitBox.localPosition = new Vector3(0.7f, 1.05f, 0.0f);
        } else {
            m_swordHitBox.localPosition = new Vector3(-0.5f, 1.05f, 0.0f);
        }
    }

    public void block() {
        m_animator.SetTrigger("Block");
        m_animator.SetBool("IdleBlock", true);
        m_body2d.velocity = new Vector2(0, m_body2d.velocity.y);
    }

    public void unblock() {
        m_animator.SetBool("IdleBlock", false);
    }

    public void attack(Collider2D enemy) {
        m_currentAttack++;

        // Loop back to one after third attack
        if (m_currentAttack > 3)
            m_currentAttack = 1;

        // Reset Attack combo if time since last attack is too large
        if (m_timeSinceAttack > 1.0f)
            m_currentAttack = 1;

        // if (enemy) {
        //     Debug.Log("We hit " + enemy.name);
        //     // Enemy e = enemy.GetComponent<Enemy>();
        //     if (e.m_animator.GetBool("IdleBlock") && e.m_facingDirection != m_facingDirection) {
        //         m_animator.SetTrigger("Hurt");
        //         knockback();
        //         m_timeSinceAttack = 0.0f;
        //         return;
        //     } else {
        //         e.TakeDamage(attackDamage);
        //         e.m_animator.SetBool("IdleBlock", false);
        //         m_rolling = false;
        //     }
            
        // }
        // // Call one of three attack animations "Attack1", "Attack2", "Attack3"
        // m_animator.SetTrigger("Attack" + m_currentAttack);

        // // Reset timer
        // m_timeSinceAttack = 0.0f;
        // if (m_grounded)
        //         m_body2d.velocity = new Vector2(0, m_body2d.velocity.y);
    }

    public void faceDirection(float inputX) {
        if (inputX > 0)
        {   
            
            m_swordHitBox.localPosition = new Vector3(0.7f, sword_height, 0.0f);
            GetComponent<SpriteRenderer>().flipX = false;
            m_facingDirection = 1;
        }
            
        else if (inputX < 0)
        {
            m_swordHitBox.localPosition = new Vector3(-0.5f, sword_height, 0.0f);
            GetComponent<SpriteRenderer>().flipX = true;
            m_facingDirection = -1;
        }
    }

    public void checkGrounded() {
        //Check if character just landed on the ground
        if (!m_grounded && m_groundSensor.State())
        {
            m_grounded = true;
            m_animator.SetBool("Grounded", m_grounded);
            // m_body2d.velocity = new Vector2(0, m_body2d.velocity.y);
        }
    }

    public void checkFalling() {
        //Check if character just started falling
        if (m_grounded && !m_groundSensor.State())
        {
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
        }
    }

    public void adjustSwordHeight() {
        sword_height = 1.05f;

        if (m_crouch) {
            sword_height = 0.65f;
        }
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

    }

    // Animation Events
    // Called in end of roll animation.
    void AE_ResetRoll()
    {
        m_rolling = false;
        uncrouch();
    }
}
