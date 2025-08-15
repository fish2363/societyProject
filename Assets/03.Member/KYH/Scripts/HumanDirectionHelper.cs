using UnityEngine;

public static class HumanDirectionHelper
{
    const int numViewDirections = 300; // 원 위의 점 개수
    public static readonly Vector3[] directions;

    static HumanDirectionHelper()
    {
        directions = new Vector3[numViewDirections];

        float angleIncrement = Mathf.PI * 2f / numViewDirections; // 360도 / 개수

        for (int i = 0; i < numViewDirections; i++)
        {
            float angle = i * angleIncrement;
            float x = Mathf.Cos(angle);
            float z = Mathf.Sin(angle);
            float y = 0f; // 높이 고정 → 2D 평면
            directions[i] = new Vector3(x, y, z);
        }
    }
}
