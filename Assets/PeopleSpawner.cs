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
    public int population;
    private float timer;
    public float happiness = 50f;
    public float checkInterval = 1f;

    void Awake()
    {
        //for (int i = 0; i < spawnCount; i++)
        //{
        //    Vector2 randomPoint = Random.insideUnitCircle * spawnRadius;
        //    Vector3 pos = transform.position + new Vector3(randomPoint.x, 0f, randomPoint.y);
        //    Human boid = Instantiate(prefab);
        //    boid.Initialize(settings, null);
        //    boid.transform.position = pos;

        //    // 랜덤 방향을 보고 생성
        //    Vector3 dirFromCenter = (pos - transform.position).normalized;
        //    Vector3 randomOffset = new Vector3(Random.Range(-0.3f, 0.3f), 0f, Random.Range(-0.3f, 0.3f));
        //    Vector3 finalDir = (dirFromCenter + randomOffset).normalized;
        //    boid.transform.rotation = Quaternion.LookRotation(finalDir, Vector3.up);
        //}
    }


    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= checkInterval)
        {
            timer = 0f;
            HandlePopulation();
        }
    }

    void HandlePopulation()
    {
        float growth = GetPopulationGrowth(happiness);

        if (growth > 0)
        {
            int toSpawn = Mathf.RoundToInt(growth);
            for (int i = 0; i < toSpawn; i++)
            {
                SpawnPerson();
                population++;
            }
        }
        else if (growth < 0)
        {
            int toRemove = Mathf.RoundToInt(Mathf.Abs(growth));
            RemovePeople(toRemove);
            population -= toRemove;
        }
    }

    void SpawnPerson()
    {
        Vector2 randomPoint = Random.insideUnitCircle * spawnRadius;
        Vector3 pos = transform.position + new Vector3(randomPoint.x, 0f, randomPoint.y);

        Vector3 randomDir = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;
        if (randomDir == Vector3.zero) randomDir = Vector3.forward;

        Human boid = Instantiate(prefab);
        boid.transform.position = pos;
        boid.Initialize(settings, null, randomDir); // 방향 전달
    }

    void RemovePeople(int count)
    {
        int idx = 0;
        int rand = Random.Range(0, PeopleManager.PeopleList.Count - 1);

        foreach(Human human in PeopleManager.PeopleList)
        {
            if (human.IsWorker) return;
            if (idx == rand) Destroy(human);
            idx++;
        }
    }

    float GetPopulationGrowth(float happiness)
    {
        if (happiness < 20f)
            return -1.0f;
        else if (happiness < 50f)
            return -1.0f + (happiness - 20f) / 30f * 2.0f;
        else
            return 1.0f + (happiness - 50f) / 50f * 1.5f;
    }
}
