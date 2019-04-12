using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour {

    // configuration parameters
    WaveConfig waveConfig;
    List<Transform> waypoints;
    int waypointIndex = 0;
    [SerializeField] bool isBossPath = false;

    // Use this for initialization
    void Start () {
        waypoints = waveConfig.GetWaypoints();
        transform.position = waypoints[waypointIndex].transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        Move();
    }

    public void SetWaveConfig(WaveConfig waveConfigToSet)
    {
        this.waveConfig = waveConfigToSet;
    }

    private void Move()
    {
        if (waypointIndex <= waypoints.Count - 1)
        {
            var targetPos = waypoints[waypointIndex].transform.position;
            // var movementThisFrame = FindObjectOfType<WaveConfig>().GetMoveSpeed() * Time.deltaTime;
            var movementThisFrame = waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPos, movementThisFrame);

            if (transform.position == targetPos)
            {
                waypointIndex++;
            }
        }
        else if(!isBossPath)
        {
            //if (!isBossPath) { Destroy(gameObject); }
            Destroy(gameObject);
        }
    }
}
