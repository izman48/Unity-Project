using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Player2 : MonoBehaviour, Player
{
    public int playerID {get {return 2;}}
    public HeroKnightActions actions;
    // PlayerActions controls;

    // Start is called before the first frame update
    void Awake()
    {
        actions = GetComponent<HeroKnightActions>();
        // controls = new PlayerActions();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Collider2D enemy = Physics2D.OverlapBox(actions.m_swordHitBox.position, actions.m_swordHitBox.localScale, 0.0f, actions.enemyLayer);
        // actions.m_animator.SetTrigger("Block");
        // actions.m_animator.SetBool("IdleBlock", true);
        // -- Handle input and movement --
        float inputX = Input.GetAxis("Horizontal2");

        // Swap direction of sprite depending on walk direction
        actions.faceDirection(inputX);

        // Move
        if (!actions.m_rolling && actions.m_timeSinceAttack > 0.25f){
            // Debug.Log(m_knockback);
            actions.m_body2d.velocity = new Vector2(inputX * actions.m_speed, actions.m_body2d.velocity.y);
        }

        //Death
        if (Input.GetKeyDown("p"))
        {
            actions.m_animator.SetBool("noBlood", actions.m_noBlood);
            actions.m_animator.SetTrigger("Death");
        }
            
        //Hurt
        else if (Input.GetKeyDown("o"))
            actions.m_animator.SetTrigger("Hurt");

        // Block
        else if (Input.GetKeyDown("k") && !actions.m_animator.GetBool("Crouch"))
        {
            actions.block();
        }

        else if (Input.GetKeyUp("k")){
            actions.unblock();
        }
        
        else if (!actions.m_animator.GetBool("IdleBlock")) {

            //Attack
            if(Input.GetKeyDown("j") && actions.m_timeSinceAttack > 0.25f)
            {
                actions.attackPlayer(enemy);
                // return;
            }
            
            //Crouch
            else if (Input.GetKeyDown(KeyCode.DownArrow) && actions.m_grounded) {
                actions.crouch();
                    
            }
            else if (Input.GetKeyUp(KeyCode.DownArrow)) {
                actions.uncrouch();
            }

            // Roll
            else if (Input.GetKeyDown("right shift") && !actions.m_rolling)
            {
                actions.roll();
            }

            else if (Input.GetKeyDown("right shift")) {
                actions.uncrouch();
            }
                

            //Jump
            else if (Input.GetKeyDown(KeyCode.Keypad0) && actions.m_grounded)
            {
                actions.jump();    
            }

            //Run
            else if (Mathf.Abs(inputX) > Mathf.Epsilon)
            {
                // Reset timer
                actions.m_delayToIdle = 0.05f;
                actions.m_animator.SetInteger("AnimState", 1);
            }
            

            //Idle
            else
            {
                // Prevents flickering transitions to idle

                actions.m_delayToIdle -= Time.deltaTime;
                if(actions.m_delayToIdle < 0)
                    actions.m_animator.SetInteger("AnimState", 0);
                // m_rolling = false;
                
            }

        }
        
        // if grounded and blocking or crouching don't move
        if (actions.m_grounded && (actions.m_animator.GetBool("IdleBlock") || actions.m_animator.GetBool("Crouch") )){
            // Debug.Log(m_animator.GetBool("Crouch") );
            actions.m_body2d.velocity = new Vector2(0, actions.m_body2d.velocity.y);
        }

    }
}
