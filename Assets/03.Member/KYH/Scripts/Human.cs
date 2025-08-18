using UnityEngine;

public enum Gender { Male, Female }



public class Human : MonoBehaviour
{
    HumanSettingSO settingSO;

    [SerializeField] HatContainer[] hatContainers;
    [SerializeField] Gender npcGender;
    [SerializeField] Animator[] animator;
    [SerializeField] string npcName;

    public bool IsWorker { get; private set; }
    
    [HideInInspector]
    public Vector3 position;
    [HideInInspector]
    public Vector3 forward;
    Vector3 velocity;

    // To update:
    Vector3 acceleration;
    [HideInInspector]
    public Vector3 avgFlockHeading;
    [HideInInspector]
    public Vector3 avgAvoidanceHeading;
    [HideInInspector]
    public Vector3 centreOfFlockmates;
    [HideInInspector]
    public int numPerceivedFlockmates;

    // Cached
    Material material;
    Transform cachedTransform;
    Transform target;

    private void OnEnable()
    {
        PeopleManager.Register(this);
        cachedTransform = transform;
    }

   
    public void Initialize(HumanSettingSO settings, Transform target, Vector3 initialForward,Gender gender, string name)
    {
        this.target = target;
        this.settingSO = settings;

        position = cachedTransform.position;
        forward = initialForward.normalized; // 스폰 시 방향 그대로 사용

        float startSpeed = (settings.minSpeed + settings.maxSpeed) / 2;
        velocity = forward * startSpeed;

        cachedTransform.forward = forward; // 시각적으로도 초기 방향 적용
        velocity = transform.forward * startSpeed;
        npcGender = gender;
        npcName = name;
        if (npcGender == Gender.Male)
        {
            animator[0].gameObject.SetActive(true);
            hatContainers[0].RandomWearHat();
        }
        else
        {
            animator[1].gameObject.SetActive(true);
            hatContainers[1].RandomWearHat();
        }
    }



    public void UpdateBoid()
    {
        Vector3 acceleration = Vector3.zero;

        if (target != null)
        {
            Vector3 offsetToTarget = (target.position - position);
            acceleration = SteerTowards(offsetToTarget) * settingSO.targetWeight;
        }

        if (numPerceivedFlockmates != 0)
        {
            centreOfFlockmates /= numPerceivedFlockmates;

            Vector3 offsetToFlockmatesCentre = (centreOfFlockmates - position);

            var alignmentForce = SteerTowards(avgFlockHeading) * settingSO.alignWeight;
            var cohesionForce = SteerTowards(offsetToFlockmatesCentre) * settingSO.cohesionWeight;
            var seperationForce = SteerTowards(avgAvoidanceHeading) * settingSO.seperateWeight;

            acceleration += alignmentForce;
            acceleration += cohesionForce;
            acceleration += seperationForce;
        }

        if (IsHeadingForCollision())
        {
            Vector3 collisionAvoidDir = ObstacleRays();
            Vector3 collisionAvoidForce = SteerTowards(collisionAvoidDir) * settingSO.avoidCollisionWeight;
            acceleration += collisionAvoidForce;
        }

        velocity += acceleration * Time.deltaTime;
        float speed = velocity.magnitude;
        Vector3 dir = velocity / speed;
        speed = Mathf.Clamp(speed, settingSO.minSpeed, settingSO.maxSpeed);
        velocity = dir * speed;

        cachedTransform.position += velocity * Time.deltaTime;
        cachedTransform.forward = dir;
        position = cachedTransform.position;
        forward = dir;
    }
    bool IsHeadingForCollision()
    {
        RaycastHit hit;
        if (Physics.SphereCast(position, settingSO.boundsRadius, forward, out hit, settingSO.collisionAvoidDst, settingSO.obstacleMask))
        {
            return true;
        }
        else { }
        return false;
    }

    Vector3 ObstacleRays()
    {
        Vector3[] rayDirections = HumanDirectionHelper.directions;

        for (int i = 0; i < rayDirections.Length; i++)
        {
            Vector3 dir = cachedTransform.TransformDirection(rayDirections[i]);
            Ray ray = new Ray(position, dir);
            if (!Physics.SphereCast(ray, settingSO.boundsRadius, settingSO.collisionAvoidDst, settingSO.obstacleMask))
            {
                return dir;
            }
        }

        return forward;
    }
    Vector3 SteerTowards(Vector3 vector)
    {
        Vector3 v = vector.normalized * settingSO.maxSpeed - velocity;
        return Vector3.ClampMagnitude(v, settingSO.maxSteerForce);
    }

    private void OnDisable()
    {
        PeopleManager.Unregister(this);
    }
}
