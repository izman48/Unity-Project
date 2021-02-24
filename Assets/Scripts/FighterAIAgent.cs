using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class FighterAIAgent : Agent
{
    [SerializeField] private Transform target;
    public override void Heuristic(float[] actionsOut)
    {
        actionsOut[0] = Input.GetAxisRaw("Horizontal2");
        actionsOut[1] = Input.GetButton("Jump") ? 1f : 0f;
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
    }
    public override void OnActionReceived(float[] vectorAction)
    {
        float moveX = vectorAction[0];
        // float moveY = vectorAction[1];
        float moveSpeed = 4f;
        float jumpHeight = 7.5f;
        float jump = vectorAction[1];
        transform.localPosition += new Vector3(moveX, 0, 0) * Time.deltaTime * moveSpeed;
        transform.localPosition += new Vector3(0, jump, 0) * Time.deltaTime * jumpHeight;
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
