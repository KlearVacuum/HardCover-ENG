using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

// Contains all data and functions of this AI
public class AIEntityController : MonoBehaviour
{
    public bool debugEnable;
    [HideInInspector]
    public bool canTransit;
    public List<PatrolData> allPatrolData = new List<PatrolData>();

    [Header("Movement Info")]
    public Transform forwardDir;
    public float mMaxTravelTime;
    private float currentTravelTime;
    public float moveForce;
    public float maxSpeed;
    [HideInInspector] public bool climbStairs;
    [HideInInspector] public int patrolIndex;
    [HideInInspector] public PatrolData activePatrol;

    private Vector2 movementInput;

    [HideInInspector]
    public Vector3 relativePos;

    [HideInInspector]
    public float actionDuration;

    [HideInInspector]
    public Transform moveToTarget;
    public MCoroutine currentCoroutine;

    // Components
    [SerializeField] SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    Collider2D col;
    Animator animator;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        animator = spriteRenderer.GetComponent<Animator>();
    }

    void Start()
    {

    }

    void Update()
    {
        currentTravelTime -= Time.deltaTime;
        if (actionDuration > 0)
        {
            actionDuration -= Time.deltaTime;
        }
    }
    private void FixedUpdate()
    {
        MovementUpdate();
    }

    void MovementUpdate()
    {
        if (movementInput == Vector2.zero)
        {
            rb.velocity *= 1 - Time.fixedDeltaTime;
            return;
        }
        Vector2 moveVect = movementInput - (Vector2)transform.position;
        Debug.DrawLine(transform.position, (Vector2)transform.position + moveVect, Color.red);
        moveVect.y = 0;
        moveVect.Normalize();

        Debug.DrawLine(transform.position, (Vector2)transform.position + moveVect, Color.green);
        rb.AddForce(moveVect * Time.fixedDeltaTime * moveForce * rb.mass);
        if (rb.velocity.magnitude > maxSpeed) rb.velocity = moveVect * maxSpeed;
    }

    public PortalTrigger GetClosestStairs(Vector2 pos, float radius)
    {
        Collider2D[] colArray = Physics2D.OverlapCircleAll(pos, radius);
        if (colArray.Length == 0) return null;

        float shortestLength = float.MaxValue;
        PortalTrigger closestDoor = null;
        
        foreach (Collider2D col in colArray)
        {
            PortalTrigger foundStairs = col.GetComponent<PortalTrigger>();
            if (foundStairs != null)
            {
                if (Mathf.Abs(transform.position.y - foundStairs.transform.position.y) < 0.5f)
                {
                    float distance = Vector2.Distance(pos, foundStairs.transform.position);
                    if (distance < shortestLength)
                    {
                        shortestLength = distance;
                        closestDoor = foundStairs;
                    }
                }
            }
        }
        return closestDoor;
    }

    public PatrolData PatrolAvailable()
    {
        foreach (PatrolData patrolData in allPatrolData)
        {
            if (patrolData.PatrolAvailable(1)) // replace 0 with current in-game time
            {
                return patrolData;
            }
        }
        return null;
    }

    public void SetTravelTime(float travelTime)
    {
        currentTravelTime = travelTime;
    }

    public void ResetTravelTime()
    {
        currentTravelTime = mMaxTravelTime;
    }

    public bool TravelTimeExceeded()
    {
        return currentTravelTime <= 0;
    }

    public void StopMoving()
    {
        movementInput = Vector2.zero;
    }

    public void MoveTowardPos(Vector2 pos)
    {
        // set movement input to pos
        if (Vector2.Distance(transform.position, pos) < 0.1f) StopMoving();
        else movementInput = pos;
    }

    public void MoveAwayFromTarget()
    {
        if (moveToTarget != null)
            MoveTowardPos(2 * transform.position - moveToTarget.position);

        if (TravelTimeExceeded())
        {
            ResetTravelTime();
            canTransit = true;
        }
    }

    public void MoveTowardTarget()
    {
        if (moveToTarget != null)
        {
            MoveTowardPos(moveToTarget.position);
        }

        if (TravelTimeExceeded())
        {
            ResetTravelTime();
            canTransit = true;
        }
    }
    public void MoveBesideTarget(Vector2 relativePos)
    {
        if (moveToTarget != null)
        {
            MoveTowardPos((Vector2)moveToTarget.position + relativePos);
        }

        if (TravelTimeExceeded())
        {
            ResetTravelTime();
            canTransit = true;
        }
    }

    public void SetMoveSpeed(float _speed)
    {
        moveForce = _speed;
    }

    public Vector3 GenerateRelativePos(float distance)
    {
        Vector3 dir = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
        return dir * distance;
    }
}