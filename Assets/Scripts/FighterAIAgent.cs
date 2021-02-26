using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class FighterAIAgent : Agent
{
    [SerializeField] private Transform target;
    public Tilemap tilemap;        
    private List<Vector3> availablePlaces;

    void Awake()
    {
        availablePlaces = new List<Vector3>();
        int count = 0;
        // controls = new PlayerActions();
        foreach (var position in tilemap.cellBounds.allPositionsWithin) {
            if (!tilemap.HasTile(position)) {
                continue;
            }
            Vector3 place = tilemap.CellToWorld(position);
            availablePlaces.Add(place);
            count++;
            // Debug.Log(place);
        }
        Debug.Log(count);
    }

    public override void Heuristic(float[] actionsOut)
    {
        actionsOut[0] = Input.GetAxisRaw("Horizontal2");
        actionsOut[1] = Input.GetButton("Jump") ? 1f : 0f;
        actionsOut[2] = Input.GetButton("Crouch") ? 1f : 0f;
        actionsOut[3] = Input.GetButton("Attack") ? 1f : 0f;
        actionsOut[4] = Input.GetButton("Block") ? 1f : 0f;
        actionsOut[5] = Input.GetButton("Roll") ? 1f : 0f;
    }
    public override void OnEpisodeBegin()
    {
        transform.localPosition = new Vector3(Random.Range(0f,6f), Random.Range(4f,-3.799437f), 0);
        target.localPosition = new Vector3(Random.Range(-7f, 0f), Random.Range(4f, -3.7f), 0);
    }
    
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(target.localPosition);
        foreach( Vector3 place in availablePlaces) {
            sensor.AddObservation(place); /*67x3*/
        }
    }
    public override void OnActionReceived(float[] vectorAction)
    {
        float moveX = vectorAction[0];
        float jump = vectorAction[1];
        float crouch = vectorAction[2];
        float attack = vectorAction[3];
        float block = vectorAction[4];
        float roll = vectorAction[5];
        // float moveY = vectorAction[1];
        float moveSpeed = 4f;
        float jumpHeight = 7.5f;
        transform.localPosition += new Vector3(moveX, 0, 0) * Time.deltaTime * moveSpeed;
        transform.localPosition += new Vector3(0, jump, 0) * Time.deltaTime * jumpHeight;
        if (attack > 0.9f) Debug.Log("attack");
        if (crouch > 0.9f) Debug.Log("crouch");
        if (block > 0.9f) Debug.Log("block");
        if (roll > 0.9f) Debug.Log("roll");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Goal>(out Goal goal)) {
            SetReward(+1f);
            EndEpisode();
        }
        if (other.TryGetComponent<Wall>(out Wall wall)) {
            SetReward(-1f);
            EndEpisode();
        }
    }
}
