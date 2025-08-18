using System.Collections.Generic;
using UnityEngine;

public class PeopleManager : MonoBehaviour
{
    public HumanSettingSO settings;
    public ComputeShader compute;

    private static List<Human> _peoples = new();
    public static IReadOnlyList<Human> PeopleList => _peoples;

    const int threadGroupSize = 1024;

    public static void Register(Human enemy)
    {
        if (!_peoples.Contains(enemy))
            _peoples.Add(enemy);
    }

    void Update()
    {
        if (_peoples.Count > 0)
        {

            int numBoids = _peoples.Count;
            var boidData = new BoidData[numBoids];

            for (int i = 0; i < _peoples.Count; i++)
            {
                boidData[i].position = _peoples[i].position;
                boidData[i].direction = _peoples[i].forward;
            }

            var boidBuffer = new ComputeBuffer(numBoids, BoidData.Size);
            boidBuffer.SetData(boidData);

            compute.SetBuffer(0, "boids", boidBuffer);
            compute.SetInt("numBoids", _peoples.Count);
            compute.SetFloat("viewRadius", settings.perceptionRadius);
            compute.SetFloat("avoidRadius", settings.avoidanceRadius);

            int threadGroups = Mathf.CeilToInt(numBoids / (float)threadGroupSize);
            compute.Dispatch(0, threadGroups, 1, 1);

            boidBuffer.GetData(boidData);

            for (int i = 0; i < _peoples.Count; i++)
            {
                _peoples[i].avgFlockHeading = boidData[i].flockHeading;
                _peoples[i].centreOfFlockmates = boidData[i].flockCentre;
                _peoples[i].avgAvoidanceHeading = boidData[i].avoidanceHeading;
                _peoples[i].numPerceivedFlockmates = boidData[i].numFlockmates;
                
                _peoples[i].UpdateBoid();
            }

            boidBuffer.Release();
        }
    }

    public struct BoidData
    {
        public Vector3 position;
        public Vector3 direction;

        public Vector3 flockHeading;
        public Vector3 flockCentre;
        public Vector3 avoidanceHeading;
        public int numFlockmates;

        public static int Size
        {
            get
            {
                return sizeof(float) * 3 * 5 + sizeof(int);
            }
        }
    }

    public static void Unregister(Human enemy)
    {
        _peoples.Remove(enemy);
    }
}
