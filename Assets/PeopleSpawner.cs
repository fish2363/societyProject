using UnityEngine;

public class PeopleSpawner : MonoBehaviour
{
    public HumanSettingSO settings;

    public enum GizmoType { Never, SelectedOnly, Always }

    public Human prefab;
    public float spawnRadius = 10;
    public int spawnCount = 10;
    public Color colour;
    public GizmoType showSpawnRegion;

    void Awake()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            Vector2 randomPoint = Random.insideUnitCircle * spawnRadius;
            Vector3 pos = transform.position + new Vector3(randomPoint.x, 0f, randomPoint.y);
            Human boid = Instantiate(prefab);
            boid.Initialize(settings, null);
            boid.transform.position = pos;
            boid.transform.forward = Random.insideUnitSphere;
        }
    }

    private void OnDrawGizmos()
    {
        if (showSpawnRegion == GizmoType.Always)
        {
            DrawGizmos();
        }
    }

    void OnDrawGizmosSelected()
    {
        if (showSpawnRegion == GizmoType.SelectedOnly)
        {
            DrawGizmos();
        }
    }

    void DrawGizmos()
    {

        Gizmos.color = new Color(colour.r, colour.g, colour.b, 0.3f);
        Gizmos.DrawSphere(transform.position, spawnRadius);
    }

}
