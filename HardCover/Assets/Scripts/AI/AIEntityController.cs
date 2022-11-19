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

    [Header("Vision Info")]
    public float viewRange;

    bool flipped;
    float startingScale;
    Vector2 currentFacingDir;
    float flipXTime = 0.1f;
    float facingDirTime;

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
        currentFacingDir = Vector2.right;
        flipped = false;
        startingScale = transform.localScale.x;
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
            animator.SetBool("isMoving", false);
            rb.velocity *= 1 - Time.fixedDeltaTime;
            return;
        }
        animator.SetBool("isMoving", true);
        Vector2 moveVect = movementInput - (Vector2)transform.position;
        Debug.DrawLine(transform.position, (Vector2)transform.position + moveVect, Color.red);
        moveVect.y = 0;
        moveVect.Normalize();

        SetDirection(moveVect);

        Debug.DrawLine(transform.position, (Vector2)transform.position + moveVect, Color.green);
        rb.AddForce(moveVect * Time.fixedDeltaTime * moveForce * rb.mass);
        if (rb.velocity.magnitude > maxSpeed) rb.velocity = moveVect * maxSpeed;
    }

    public Portal GetClosestStairs(Vector2 pos, float radius)
    {
        Collider2D[] colArray = Physics2D.OverlapCircleAll(pos, radius);
        if (colArray.Length == 0) return null;

        float shortestLength = float.MaxValue;
        Portal closestDoor = null;
        
        foreach (Collider2D col in colArray)
        {
            Portal foundStairs = col.GetComponent<Portal>();
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
        if (allPatrolData == null || allPatrolData.Count == 0) return null;
        foreach (PatrolData patrolData in allPatrolData)
        {
            if (patrolData.PatrolAvailable(GlobalGameData.currentDay, GlobalGameData.currentTime)) // replace 0 with current in-game time
            {
                if (patrolData.patrolPoints.Count == 0) Debug.LogError("Patrol Data has no patrol points!");
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
    public void SetDirection(Vector3 direction)
    {
        Vector2 newDir = direction;
        newDir.y = 0;
        newDir.Normalize();
        if (Vector2.Dot(newDir, currentFacingDir) < 0f)
        {
            // moving opposite way
            facingDirTime -= Time.deltaTime;

            if (facingDirTime < 0)
            {
                StartCoroutine(FlipXCoroutine(flipXTime));
                currentFacingDir = newDir;
                facingDirTime = flipXTime;
            }
        }
        else
        {
            facingDirTime = flipXTime;
        }
    }

    public Vector3 GenerateRelativePos(float distance)
    {
        Vector3 dir = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
        return dir * distance;
    }
    IEnumerator FlipXCoroutine(float seconds)
    {
        float totalTime = seconds;
        float t = 0;
        float startScale = 1;
        float endScale = 1;
        if (flipped)
        {
            startScale = -startingScale;
            endScale = startingScale;
        }
        else
        {
            startScale = startingScale;
            endScale = -startingScale;
        }
        while (t < totalTime)
        {
            float lerp = Mathf.Lerp(startScale, endScale, t / totalTime);
            if (lerp <= 0.001f && lerp >= -0.001f) lerp = 0.001f;
            transform.localScale = new Vector3(lerp, transform.localScale.y, transform.localScale.z);
            t += Time.deltaTime;
            if (t > totalTime) t = totalTime;
            yield return null;
        }
        transform.localScale = new Vector3(endScale, transform.localScale.y, transform.localScale.z);
        flipped = !flipped;
    }

    public bool PlayerHasBook()
    {
        return GlobalGameData.playerStats.gameObject.GetComponent<InteractionController>().GetBook() != null;
    }

    public bool PlayerInSight()
    {
        // return false if ai is currently walking in stairs

        // player is not on the same level
        Vector2 diff = transform.position - GlobalGameData.playerStats.transform.position;
        if (Mathf.Abs(diff.y) > 1f) return false;
        return diff.magnitude < viewRange;
    }


    public IEnumerator CatchPlayerCoroutine()
    {
        // start dialogue about catching player
        Debug.Log("caught player with book!");
        yield return new WaitForSeconds(1f);
        // player pays the bribe
        Debug.Log("player pays a fine");
        yield return new WaitForSeconds(1f);
        // screen fades to black
        Debug.Log("screen fades to black");
        yield return new WaitForSeconds(1f);
        // time skip
        Debug.Log("time skip");
        yield return new WaitForSeconds(1f);
        // screen fades back to normal
        Debug.Log("screen is fading back");
        yield return new WaitForSeconds(1f);
        // game resumes
        Debug.Log("resume game");

    }
}