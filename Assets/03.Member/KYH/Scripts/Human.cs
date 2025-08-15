using UnityEngine;

public class Human : MonoBehaviour
{
    HumanSettingSO settingSO;

    [SerializeField] HatContainer hatContainer;


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

    private void Start()
    {
        hatContainer.RandomWearHat();
    }

    public void Initialize(HumanSettingSO settings, Transform target)
    {
        this.target = target;
        settingSO = settings;

        position = cachedTransform.position;
        forward = cachedTransform.forward;

        float startSpeed = (settings.minSpeed + settings.maxSpeed) / 2;
        velocity = transform.forward * startSpeed;
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
