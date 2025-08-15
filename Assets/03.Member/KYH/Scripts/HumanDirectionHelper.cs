using UnityEngine;

public static class HumanDirectionHelper
{
    const int numViewDirections = 300; // �� ���� �� ����
    public static readonly Vector3[] directions;

    static HumanDirectionHelper()
    {
        directions = new Vector3[numViewDirections];

        float angleIncrement = Mathf.PI * 2f / numViewDirections; // 360�� / ����

        for (int i = 0; i < numViewDirections; i++)
        {
            float angle = i * angleIncrement;
            float x = Mathf.Cos(angle);
            float z = Mathf.Sin(angle);
            float y = 0f; // ���� ���� �� 2D ���
            directions[i] = new Vector3(x, y, z);
        }
    }
}
