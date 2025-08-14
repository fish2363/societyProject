using UnityEngine;

[CreateAssetMenu(fileName = "HumanSettingSO", menuName = "SO/HumanSettingSO")]
public class HumanSettingSO : ScriptableObject
{
    // �⺻ �ӵ� ����
    public float minSpeed = 2;   // �ּ� �̵� �ӵ�
    public float maxSpeed = 5;   // �ִ� �̵� �ӵ�

    // �ֺ� ���� ����
    public float perceptionRadius = 2.5f; // ����, ����, �и� ��� �� �ֺ� Ž�� �ݰ�
    public float avoidanceRadius = 1;     // ������ �ִ� ���Ḧ ���� ���� �ݰ�

    // �̵� ���� ����
    public float maxSteerForce = 3;       // �� �����ӿ� ȸ��/���� ��ȯ�� �� �ִ� �ִ� ��

    // ���� �ൿ ����ġ
    public float alignWeight = 0.5f;     // ���� ����ġ: �ֺ� ����� ���� ���߱�
    public float cohesionWeight = 1;  // ���� ����ġ: �ֺ� ��� �߽����� ���̱�
    public float seperateWeight = 1;  // �и� ����ġ: �ʹ� ����� ��� ���ϱ�

    // ��ǥ ���� ����ġ
    public float targetWeight = 0.4f;    // ��ǥ(�ǹ�, ���ڸ� ��) ������ �̵��� �� ����ġ

    [Header("Collisions")]
    // �浹 ���� ����
    public LayerMask obstacleMask;       // �浹 üũ�� ���̾� ����ũ
    public float boundsRadius = .27f;    // �浹 ������ ��ü ������
    public float avoidCollisionWeight = 10; // �浹 ȸ�� �� ����ġ
    public float collisionAvoidDst = 5;     // �浹�� ������ �ִ� �Ÿ�

}
