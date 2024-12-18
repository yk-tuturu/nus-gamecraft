using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    private SpriteRenderer sprite;

    public Transform path; 
    public List<Transform> waypoints = new List<Transform>();
    public float moveSpeed = 2f;
    public int waypointIndex = 0;

    public Table targetTable; 

    public bool isMoving = true;
    public bool isMovingAway = false;
    public bool doubled = false;

    private Vector3 prevPos;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = SpriteManager.instance.GetCustomerSprite();

        foreach (Transform child in path) {
            waypoints.Add(child);
        }
        transform.position = waypoints[waypointIndex].transform.position;
        prevPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving || isMovingAway) {
            Move();
        }
    }

    private void Move()
    {
        if (waypointIndex <= waypoints.Count - 1)
        {
            Debug.Log("moving");
            Debug.Log(transform.position);
            Debug.Log(waypoints[waypointIndex].transform.position);
            transform.position = Vector3.MoveTowards(transform.position,
               waypoints[waypointIndex].transform.position,
               moveSpeed * Time.deltaTime);

            if (transform.position == waypoints[waypointIndex].transform.position)
            {
                waypointIndex += 1;
            }
        }

        if (waypointIndex == waypoints.Count) {
            if (isMovingAway) {
                Debug.Log("destroying customer");
                Destroy(gameObject);
                return;
            }

            Debug.Log("arrived");
            if (doubled) {
                targetTable.OccupyTable(2, this);
            } else {
                targetTable.OccupyTable(1, this);
            }
            isMoving = false;
            
        } else {
            if (transform.position.x >= prevPos.x) {
                sprite.flipX = true;
            } else {
                sprite.flipX = false;
            }
            prevPos = transform.position;
        }
    }

    public void beginMoveAway(Transform newPath) {
        waypointIndex = 0;
        path = newPath;
        waypoints = new List<Transform>();
        foreach (Transform child in path) {
            waypoints.Add(child);
        }

        transform.position = waypoints[waypointIndex].transform.position;
        isMovingAway = true;
    }
}
