using UnityEngine;

public class PeopleSpawner : MonoBehaviour
{
    public HumanSettingSO settings;

    public Human prefab;
    public float spawnRadius = 10;
    public int population;
    private float timer;
    [Header("행복도")]
    public float happiness = 50f;
    [Header("생성 주기")]
    public float checkInterval = 1f;

    [SerializeField, Header("남자 이름")] string[] maleFirstNames = { "연호", "소빈", "동호" };
    [SerializeField, Header("여자 이름")] string[] femaleFirstNames = { "연호", "소빈", "동호" };
    [SerializeField, Header("성씨")] string[] lastNames = { "김", "이", "박", "최" };


    public Gender GetRandomGender()
    {
        return Random.value < 0.5f ? Gender.Male : Gender.Female;
    }
    public string GenerateRandomName(Gender gender)
    {
        string firstName = gender == Gender.Male
            ? maleFirstNames[Random.Range(0, maleFirstNames.Length)]
            : femaleFirstNames[Random.Range(0, femaleFirstNames.Length)];

        string lastName = lastNames[Random.Range(0, lastNames.Length)];

        return lastName + firstName;
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
        Gender gender = GetRandomGender();
        boid.transform.position = pos;
        boid.Initialize(settings, null, randomDir,gender,GenerateRandomName(gender)); // 방향 전달
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
