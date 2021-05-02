using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;


public class FighterAIAgent : Agent
{
    // [SerializeField] private Transform target;
    public Transform enemy;
    public Tilemap tilemap;        
    private List<Vector3> availablePlaces;
    public SpriteRenderer background;
    public int playerID;
    private FighterAIAgent otherScript;
    public HeroKnightActions actions;
    private bool m_crouch = false;
    private bool m_roll = false;
    private bool m_block = false;
    private bool m_jump = false;
    private bool m_attack = false;
    private bool m_passive = false;
    private float inputX = 0f;

    private float distanceReward = 0.01f;
    public Timer timer;

    public override void Initialize()
    {
        actions = GetComponent<HeroKnightActions>();
        otherScript = enemy.GetComponent<FighterAIAgent>();
        availablePlaces = new List<Vector3>();
        int count = 0;
        foreach (var position in tilemap.cellBounds.allPositionsWithin) {
            Vector3Int localPlace = new Vector3Int(position.x, position.y, position.z);
            if (!tilemap.HasTile(localPlace)) {
                continue;
            }
            
            Vector3 place = tilemap.CellToWorld(localPlace);
            availablePlaces.Add(place);
            count++;
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continousActions = actionsOut.ContinuousActions;
        ActionSegment<int> discreteActions = actionsOut.DiscreteActions;
        if (playerID == 2) {
            continousActions[0] = Input.GetAxisRaw("Horizontal2");
            discreteActions[0] = Input.GetButton("Jump") ? 1 : 0;
            discreteActions[1] = Input.GetButton("Crouch") ? 1 : 0;
            discreteActions[2] = Input.GetButton("Attack") ? 1 : 0;
            discreteActions[3] = Input.GetButton("Block") ? 1 : 0;
            discreteActions[4] = Input.GetButton("Roll") ? 1 : 0;
        } else {
            continousActions[0] = Input.GetAxisRaw("Horizontal1");
            discreteActions[0] = Input.GetButton("Jump2") ? 1 : 0;
            discreteActions[1] = Input.GetButton("Crouch2") ? 1 : 0;
            discreteActions[2] = Input.GetButton("Attack2") ? 1 : 0;
            discreteActions[3] = Input.GetButton("Block2") ? 1 : 0;
            discreteActions[4] = Input.GetButton("Roll2") ? 1 : 0;
        }
    }

    public override void OnEpisodeBegin()
    {

        if (playerID == 1) {
            transform.localPosition = new Vector3(-5.8f, -3.8f, 0);
            enemy.localPosition = new Vector3(3.3f, -3.8f, 0);
        } else {
            transform.localPosition = new Vector3(3.3f, -3.8f, 0);
            enemy.localPosition = new Vector3(-5.8f, -3.8f, 0);
        }
        // distanceReward = 0.01f;
        // target.localPosition = new Vector3(-2.26f, 3.81f, 0);
        // actions = GetComponent<HeroKnightActions>();
        actions.Reset();
        timer.Reset();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition.x);
        sensor.AddObservation(transform.localPosition.y);
        // sensor.AddObservation(target.localPosition);
        sensor.AddObservation(enemy.localPosition.x);
        sensor.AddObservation(enemy.localPosition.y);
        foreach( Vector3 place in availablePlaces) {
            sensor.AddObservation(place.x); /*67x2*/
            sensor.AddObservation(place.y);
        }
        sensor.AddObservation(timer.timeLeft);
        sensor.AddObservation(actions.currentHealth);
        
        FighterAIAgent enemyPlayer = enemy.GetComponent<FighterAIAgent>();
        sensor.AddObservation(enemyPlayer.inputX);
        sensor.AddObservation(enemyPlayer.m_attack);
        sensor.AddObservation(enemyPlayer.m_jump);
        sensor.AddObservation(enemyPlayer.m_block);
        sensor.AddObservation(enemyPlayer.m_crouch);
        sensor.AddObservation(enemyPlayer.m_roll);
        sensor.AddObservation(enemyPlayer.actions.currentHealth);
        float dist = Vector3.Distance(transform.localPosition, enemy.localPosition);
        sensor.AddObservation(dist);
        

    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        float jump = actions.DiscreteActions[0];
        float crouch = actions.DiscreteActions[1];
        float attack = actions.DiscreteActions[2];
        float block = actions.DiscreteActions[3];
        float roll = actions.DiscreteActions[4];
        float passive = actions.DiscreteActions[5];

        inputX = moveX;
        if (jump == 0) m_jump = false;
        if (attack == 0) m_attack = false;
        if (block == 0) m_block = false;
        if (roll == 0) m_roll = false;
        if (crouch == 0) m_crouch = false;
        if (passive == 0) m_passive = false;

        if (jump == 1) {
            m_jump = true;
            }
        if (attack == 1 && !m_attack) {
            // Debug.Log("Attack");
            m_attack = true;
        }
        if (crouch == 1 && !m_crouch) {
            // Debug.Log("crouch");
            m_crouch = true;
            }
        if (block == 1 && !m_block) {
            // Debug.Log("block");
            m_block = true;
            }
        if (roll == 1 && !m_roll) {
            // Debug.Log("roll");
            m_roll = true;
        }
        if (passive == 1 && !m_passive) {
            m_passive = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Wall>(out Wall wall)) {
            Debug.Log("Hit wall");
            SetReward(-1f);
            // otherScript.SetReward(+1f);
            otherScript.EndEpisode();
            EndEpisode();
        } 
        if (other.TryGetComponent<Goal>(out Goal goal)) {
            Debug.Log("Hit goal");
            SetReward(+1f);
            otherScript.SetReward(-1f);
            otherScript.EndEpisode();
            EndEpisode();
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Collider2D enemy = Physics2D.OverlapBox(actions.m_swordHitBox.position, actions.m_swordHitBox.localScale, 0.0f, actions.enemyLayer);
        actions.faceDirection(inputX);
        
        
        Vector3 sword = actions.m_swordHitBox.position;

        // Move
        if (!actions.m_rolling && actions.m_timeSinceAttack > 0.25f){
            actions.m_body2d.velocity = new Vector2(inputX * actions.m_speed, actions.m_body2d.velocity.y);
        }
        if (actions.m_rolling) 
            return;
        if (m_block && !actions.m_animator.GetBool("Crouch")) 
        {
            actions.block();
        }
        else if (!m_block && actions.m_animator.GetBool("IdleBlock")) 
        {
            actions.unblock();
        }
        else if (!actions.m_animator.GetBool("IdleBlock")) 
        {
            if (m_attack && actions.m_timeSinceAttack > 0.25f)
            {
                actions.attack(enemy);
            }
            else if (m_crouch && actions.m_grounded) 
            {
                actions.crouch();
            }
            else if (!m_crouch && actions.m_animator.GetBool("Crouch"))
            {
                actions.uncrouch();
            }
            else if (m_roll && !actions.m_rolling && actions.m_grounded)
            {
                actions.roll();
            }
            else if (!m_roll && actions.m_animator.GetBool("Crouch"))
            {
                actions.uncrouch();
            }
            else if (m_jump && actions.m_grounded && !m_attack)
            {
                actions.jump();
            }
            else if (Mathf.Abs(inputX) > Mathf.Epsilon)
            {
                // Reset timer
                actions.m_delayToIdle = 0.05f;
                actions.m_animator.SetInteger("AnimState", 1);    
            }
            else 
            {
                actions.m_delayToIdle -= Time.deltaTime;
                if(actions.m_delayToIdle < 0)
                    actions.m_animator.SetInteger("AnimState", 0);
            }
        }
        // if grounded and blocking or crouching don't move
        if (actions.m_grounded && (actions.m_animator.GetBool("IdleBlock") || actions.m_animator.GetBool("Crouch") )){
            actions.m_body2d.velocity = new Vector2(0, actions.m_body2d.velocity.y);
        }

    }

    public void giveReward(float reward) {
        // if (reward > 0.0000001f)
        //     Debug.Log(reward);
        AddReward(reward);
    }

    public void nextEpisode() {
        EndEpisode();
    }
}
