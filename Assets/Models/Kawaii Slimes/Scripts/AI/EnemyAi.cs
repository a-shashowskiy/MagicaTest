
using UnityEngine;
using UnityEngine.AI;
public enum SlimeAnimationState { Idle,Walk,Jump,Attack,Damage}
public class EnemyAi : MonoBehaviour
{
    public float health = 20; 
    public float protection = 1;
    public int damage = 5;
    [SerializeField] GameObject destroyVFX;

    public Face faces;
    public GameObject SmileBody;
    public SlimeAnimationState currentState; 
   
    public Animator animator;
    public NavMeshAgent agent;
    //public Transform[] waypoints;
    public int damType;

    private int m_CurrentWaypointIndex;

    private bool move;
    private Material faceMaterial;
    private Vector3 originPos;
    private Transform destinationPos;

    public enum WalkType { Patroll ,ToOrigin }
    private WalkType walkType = WalkType.ToOrigin;
    

    void Start()
    {
        destinationPos = GameObject.FindGameObjectWithTag("Player").transform;
        faceMaterial = SmileBody.GetComponent<Renderer>().materials[1];
        walkType = WalkType.ToOrigin;

        originPos = destinationPos.position;
    }
    //public void WalkToNextDestination()
    //{
    //    currentState = SlimeAnimationState.Walk;
    //    m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
    //    agent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
    //    SetFace(faces.WalkFace);
    //}
    //public void CancelGoNextDestination() =>CancelInvoke(nameof(WalkToNextDestination));

    void SetFace(Texture tex)
    {
        faceMaterial.SetTexture("_MainTex", tex);
    }
    void Update()
    {
        originPos = destinationPos.position;

        switch (currentState)
        {
            case SlimeAnimationState.Idle:
                
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle")) return;
                StopAgent();
                SetFace(faces.Idleface);
                break;

            case SlimeAnimationState.Walk:

                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Walk")) return;

                agent.isStopped = false;
                agent.updateRotation = true;

                if (walkType == WalkType.ToOrigin)
                {
                    agent.SetDestination(destinationPos.position);
                    // Debug.Log("WalkToOrg");
                    SetFace(faces.WalkFace);
                    // agent reaches the destination
                    if (agent.remainingDistance < agent.stoppingDistance)
                    {
                        currentState = SlimeAnimationState.Attack;
                    }                       
                }

                // set Speed parameter synchronized with agent root motion moverment
                animator.SetFloat("Speed", agent.velocity.magnitude);
                

                break;

            case SlimeAnimationState.Jump:

                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Jump")) return;

                StopAgent();
                SetFace(faces.jumpFace);
                animator.SetTrigger("Jump");

                //Debug.Log("Jumping");
                break;

            case SlimeAnimationState.Attack:

                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack")) return;
                StopAgent();
                SetFace(faces.attackFace);
                animator.SetTrigger("Attack");

               // Debug.Log("Attacking");

                break;
            case SlimeAnimationState.Damage:

               // Do nothing when animtion is playing
               if(animator.GetCurrentAnimatorStateInfo(0).IsName("Damage0")
                    || animator.GetCurrentAnimatorStateInfo(0).IsName("Damage1")
                    || animator.GetCurrentAnimatorStateInfo(0).IsName("Damage2") ) return;

                StopAgent();
                animator.SetTrigger("Damage");
                animator.SetInteger("DamageType", damType);
                SetFace(faces.damageFace);

                //Debug.Log("Take Damage");
                break;
       
        }

    }


    private void StopAgent()
    {
        agent.isStopped = true;
        animator.SetFloat("Speed", 0);
        agent.updateRotation = false;
    }
    // Animation Event
    public void AlertObservers(string message)
    {
      
        if (message.Equals("AnimationDamageEnded"))
        {
            // When Animation ended check distance between current position and first position 
            //if it > 1 AI will back to first position 

            float distanceOrg = Vector3.Distance(transform.position, originPos);
            if (distanceOrg > 1f)
            {
                walkType = WalkType.ToOrigin;
                currentState = SlimeAnimationState.Walk;
            }
            else currentState = SlimeAnimationState.Walk;

            //Debug.Log("DamageAnimationEnded");
        }

        if (message.Equals("AnimationAttackEnded"))
        {
            currentState = SlimeAnimationState.Walk;           
        }

        if (message.Equals("AnimationJumpEnded"))
        {
            currentState = SlimeAnimationState.Walk;
        }
    }

    public void TakeDamage(float damage)
    {
        health = health - damage*protection;
        if (health <= 0)
        {
            Instantiate(destroyVFX, transform.position+Vector3.up, Quaternion.identity);
            Destroy(gameObject);
        }
        else
        {
            damType = Random.Range(0, 2);
            currentState = SlimeAnimationState.Damage;
        }
    }

    void OnAnimatorMove()
    {
        // apply root motion to AI
        Vector3 position = animator.rootPosition;
        position.y = agent.nextPosition.y;
        transform.position = position;
        agent.nextPosition = transform.position;
    }
    }
