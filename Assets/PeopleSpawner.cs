using UnityEngine;

public class PeopleSpawner : MonoBehaviour
{
    public HumanSettingSO settings;

    public Human prefab;
    public float spawnRadius = 10;
    private float timer;
    [Header("���� �ֱ�")]
    public float checkInterval = 1f;

    [SerializeField, Header("���� �̸�")] string[] maleFirstNames = { "��ȣ", "�Һ�", "��ȣ" };
    [SerializeField, Header("���� �̸�")] string[] femaleFirstNames = { "��ȣ", "�Һ�", "��ȣ" };
    [SerializeField, Header("����")] string[] lastNames = { "��", "��", "��", "��" };


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
        float growth = GetPopulationGrowth(NumericalValueManager.Instance.GetNumericalValue(NumericalValueType.Happiness));

        if (growth > 0)
        {
            int toSpawn = Mathf.RoundToInt(growth);
            for (int i = 0; i < toSpawn; i++)
            {
                SpawnPerson();
                NumericalValueManager.Instance.ModifyNumericalValue(NumericalValueType.PeopleCnt,ModifyType.Add,1);
            }
        }
        else if (growth < 0)
        {
            int toRemove = Mathf.RoundToInt(Mathf.Abs(growth));
            RemovePeople(toRemove);
            NumericalValueManager.Instance.ModifyNumericalValue(NumericalValueType.PeopleCnt, ModifyType.Add, -1);
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
        boid.Initialize(settings, null, randomDir,gender,GenerateRandomName(gender)); // ���� ����
    }

    void RemovePeople(int count)
    {
        if (PeopleManager.PeopleList.Count < 1) return;
        int rand;

        do rand = Random.Range(0, PeopleManager.PeopleList.Count - 1);
        while (PeopleManager.PeopleList[rand].IsWorker);

        Destroy(PeopleManager.PeopleList[rand].gameObject);
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
