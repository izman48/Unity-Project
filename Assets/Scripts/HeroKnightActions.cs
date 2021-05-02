using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class HeroKnightActions : MonoBehaviour
{
    [SerializeField] public float      m_speed = 4.0f;
    [SerializeField] public float      m_jumpForce = 7.5f;
    [SerializeField] public float      m_rollForce = 6.0f;
    [SerializeField] public bool       m_noBlood = false;
    [SerializeField] public GameObject m_slideDust;
    // public GameObject           otherPlayer;
    public LayerMask            enemyLayer;
    public HealthBar            healthBar;

    public Animator            m_animator;
    public Rigidbody2D         m_body2d;
    public BoxCollider2D       m_hitbox;
    public Sensor_HeroKnight   m_groundSensor;
    public bool                m_grounded = false;
    public bool                m_rolling = false;
    public bool                m_crouch = false;
    public int                 m_facingDirection = 1;
    public int                 m_currentAttack = 0;
    public float               m_timeSinceAttack = 0.0f;
    public float               m_delayToIdle = 0.0f;
    public int                 attackDamage = 15;
    public Transform           m_swordHitBox;
    public int                 maxHealth = 100;
    public int                 currentHealth;
    public bool                finished;
    public float               m_knockback = 2.0f;
    public float               m_knockback_cooldown = 0.5f;
    private float jump_reward = 0.00f;
    private float attack_reward = 0.01f;
    public float block_reward = 0.00f;
    private float crouch_reward = 0f;
    private float roll_reward = 0f;
    private float damageTakenReward = -0.01f;
    public Timer timer;

    public float sword_height;
    private FighterAIAgent parentScript;
    public GameObject enemy;
    // Player player;
    // Start is called before the first frame update
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_hitbox = GetComponent<BoxCollider2D>();
        // player = GetComponent<Player>();
        m_swordHitBox = transform.Find("SwordHitBox").transform;
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_HeroKnight>();
        healthBar.SetMaxHealth(maxHealth); 
        finished = false;
        currentHealth = maxHealth;
        sword_height = 1.05f;
        faceDirection(m_facingDirection);
        parentScript = GetComponent<FighterAIAgent>();
        // timer = GetComponent<Timer>();
        // Debug.Log(parentScript);
    }

    public void Reset()
    {
        // Debug.Log("Reset");
        this.enabled = true;
        // jump();
        m_animator.SetTrigger("Reset");
        m_rolling = false;
        m_crouch = false;
        // crouch();
        // uncrouch();
        // checkGrounded();
        // checkFalling();
        // m_body2d = GetComponent<Rigidbody2D>();
        // m_hitbox = GetComponent<BoxCollider2D>();
        // player = GetComponent<Player>();
        // m_swordHitBox = transform.Find("SwordHitBox").transform;
        // m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_HeroKnight>();
        healthBar.SetMaxHealth(maxHealth); 
        finished = false;
        currentHealth = maxHealth;
        sword_height = 1.05f;
        faceDirection(m_facingDirection);
        parentScript = GetComponent<FighterAIAgent>();
        // timer = GetComponent<Timer>();

        // Debug.Log(parentScript);
    }
    
    // Update is called once per frame
    void Update()
    {
        // if (m_body2d.position.y <= -5.0f) {
        //     currentHealth = 0;
        //     healthBar.SetHealth(currentHealth);
        //     Debug.Log(player.playerID);
        //     StartCoroutine(DieRoutine(player.playerID));
        // }

        m_timeSinceAttack += Time.deltaTime;
        m_knockback += Time.deltaTime;
        
        adjustSwordHeight();

        checkGrounded();

        checkFalling();

        if (m_rolling || m_knockback < m_knockback_cooldown ) {
            m_body2d.isKinematic = true;
            return;
        }

        m_body2d.isKinematic = false;

        //Set AirSpeed in animator
        m_animator.SetFloat("AirSpeedY", m_body2d.velocity.y);
        
    }

    public void knockback() {
        TakeDamage(attackDamage/4);
        m_body2d.velocity = new Vector2(m_facingDirection*-5, 1);
        m_knockback = 0.0f;
    }

    public void jump() {
        // Debug.Log("jumped");
        parentScript.giveReward(jump_reward);
        // jump_reward *= 0.1f;
        m_animator.SetTrigger("Jump");
        m_grounded = false;
        m_animator.SetBool("Grounded", m_grounded);
        m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
        m_groundSensor.Disable(0.2f);
    }

    public void roll() {
        // Debug.Log("rolled");
        parentScript.giveReward(roll_reward);
        roll_reward *= 0.1f;
        m_rolling = true;
        m_animator.SetTrigger("Roll");
        m_hitbox.offset = new Vector2(0.0f, 0.462f);
        m_hitbox.size = new Vector2(0.73f, 0.8f);
        m_body2d.velocity = new Vector2(m_facingDirection * m_rollForce, 0);
        checkGrounded();

        checkFalling();
    }

    public void crouch() {
        parentScript.giveReward(crouch_reward);
        crouch_reward *= 0.1f;
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

    public void attack(Collider2D enemyCollider) {
        // When Enemy Attacks Player

        
        m_currentAttack++;

        // Loop back to one after third attack
        if (m_currentAttack > 3)
            m_currentAttack = 1;

        // Reset Attack combo if time since last attack is too large
        if (m_timeSinceAttack > 1.0f)
            m_currentAttack = 1;

        // Debug.Log("reached");
        if (enemyCollider) {
            Debug.Log("We hit " + enemyCollider.name);
            FighterAIAgent player = enemy.GetComponent<FighterAIAgent>();
            HeroKnightActions e = player.actions;
            // if they block
            if (e.m_animator.GetBool("IdleBlock") && e.m_facingDirection != m_facingDirection) {
                player.giveReward(e.block_reward);
                // e.block_reward *= 0.1f;

                m_animator.SetTrigger("Hurt");
                knockback();
                m_timeSinceAttack = 0.0f;
                return;
            } else {
                // they get hit
                parentScript.giveReward(attack_reward);
                // attack_reward *= 0.1f;

                e.TakeDamage(attackDamage); // how much damage and who is taking it
                e.m_animator.SetBool("IdleBlock", false);
                e.m_rolling = false;
                // if (e.currentHealth <= 0) {
                //     Debug.Log("BOT WINS");
                //     SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                // }
            }
            
        }
        // Call one of three attack animations "Attack1", "Attack2", "Attack3"
        m_animator.SetTrigger("Attack" + m_currentAttack);

        // Reset timer
        m_timeSinceAttack = 0.0f;
        if (m_grounded)
                m_body2d.velocity = new Vector2(0, m_body2d.velocity.y);
        
    }
    // public void attackPlayer(Collider2D enemy) {
    //     // When Enemy Attacks Player

        
    //     m_currentAttack++;

    //     // Loop back to one after third attack
    //     if (m_currentAttack > 3)
    //         m_currentAttack = 1;

    //     // Reset Attack combo if time since last attack is too large
    //     if (m_timeSinceAttack > 1.0f)
    //         m_currentAttack = 1;

    //     if (enemy) {
    //         Debug.Log("We hit " + enemy.name);
    //         Player1 player = enemy.GetComponent<Player1>();
    //         HeroKnightActions e = player.actions;
    //         if (e.m_animator.GetBool("IdleBlock") && e.m_facingDirection != m_facingDirection) {
    //             m_animator.SetTrigger("Hurt");
    //             knockback(2);
    //             m_timeSinceAttack = 0.0f;
    //             return;
    //         } else {
                
    //             e.TakeDamage(attackDamage, 1); // how much damage and who is taking it
    //             e.m_animator.SetBool("IdleBlock", false);
    //             m_rolling = false;
    //             // if (e.currentHealth <= 0) {
    //             //     Debug.Log("BOT WINS");
    //             //     SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    //             // }
    //         }
            
    //     }
    //     // Call one of three attack animations "Attack1", "Attack2", "Attack3"
    //     m_animator.SetTrigger("Attack" + m_currentAttack);

    //     // Reset timer
    //     m_timeSinceAttack = 0.0f;
    //     if (m_grounded)
    //             m_body2d.velocity = new Vector2(0, m_body2d.velocity.y);
    // }

    // public void attackEnemy(Collider2D enemy) {
    //     // Player attacks enemy
        

    //     m_currentAttack++;

    //     // Loop back to one after third attack
    //     if (m_currentAttack > 3)
    //         m_currentAttack = 1;

    //     // Reset Attack combo if time since last attack is too large
    //     if (m_timeSinceAttack > 1.0f)
    //         m_currentAttack = 1;

    //     if (enemy) {
    //         Debug.Log("We hit " + enemy.name);
    //         Player2 player = enemy.GetComponent<Player2>();

    //         HeroKnightActions e = player.actions;
    //         if (e.m_animator.GetBool("IdleBlock") && e.m_facingDirection != m_facingDirection) {
    //             m_animator.SetTrigger("Hurt");
    //             knockback(1);
    //             m_timeSinceAttack = 0.0f;
    //             return;
    //         } else {
    //             e.TakeDamage(attackDamage, 2); // how much damage and who is taking it
    //             e.m_animator.SetBool("IdleBlock", false);
    //             m_rolling = false;
    //             // if (e.currentHealth <= 0) {
    //             //     Debug.Log("Player WINS");
    //             //     SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    //             // }
    //         }
            
    //     }
    //     // Call one of three attack animations "Attack1", "Attack2", "Attack3"
    //     m_animator.SetTrigger("Attack" + m_currentAttack);

    //     // Reset timer
    //     m_timeSinceAttack = 0.0f;
    //     if (m_grounded)
    //             m_body2d.velocity = new Vector2(0, m_body2d.velocity.y);
    // }

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
            // Debug.Log(parentScript);
            // parentScript.giveReward(-1f);
            StartCoroutine(DieRoutine());
            
        } else {
            parentScript.giveReward(-1 * (1 - currentHealth/maxHealth)/100);
            FighterAIAgent enemyPlayer = enemy.GetComponent<FighterAIAgent>();
            if (enemyPlayer.actions.currentHealth > currentHealth)
                enemyPlayer.giveReward((enemyPlayer.actions.currentHealth - currentHealth)/100);
        }
        
    }

    IEnumerator DieRoutine()
    {
        // Debug.Log("DEAD");
        int playerNum = 0;
        if (parentScript.playerID == 2) {playerNum = 1;} else {playerNum = 2;}
        m_animator.SetBool("noBlood", m_noBlood);
        m_animator.SetTrigger("Death");
        this.enabled = false;
        finished = true;
        yield return new WaitForSeconds(1);
        Debug.Log("PLAYER " + playerNum + " WINS");
        FighterAIAgent enemyPlayer = enemy.GetComponent<FighterAIAgent>();
        parentScript.SetReward(-1f);
        float time = timer.getTime();
        enemyPlayer.AddReward(+1f + time/90);
        // } 
        timer.Reset();

        
        enemyPlayer.nextEpisode();
        parentScript.nextEpisode();

        // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Animation Events
    // Called in end of roll animation.
    void AE_ResetRoll()
    {
        m_rolling = false;
        uncrouch();
    }
}
