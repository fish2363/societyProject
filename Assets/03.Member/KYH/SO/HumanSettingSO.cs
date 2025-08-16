using UnityEngine;

[CreateAssetMenu(fileName = "HumanSettingSO", menuName = "SO/HumanSettingSO")]
public class HumanSettingSO : ScriptableObject
{
    // 기본 속도 범위
    public float minSpeed = 2;   // 최소 이동 속도
    public float maxSpeed = 5;   // 최대 이동 속도

    // 주변 인지 범위
    public float perceptionRadius = 2.5f; // 정렬, 응집, 분리 계산 시 주변 탐지 반경
    public float avoidanceRadius = 1;     // 가까이 있는 동료를 피할 때의 반경

    // 이동 조향 관련
    public float maxSteerForce = 3;       // 한 프레임에 회전/방향 전환할 수 있는 최대 힘

    // 군집 행동 가중치
    public float alignWeight = 0.5f;     // 정렬 가중치: 주변 사람과 방향 맞추기
    public float cohesionWeight = 1;  // 응집 가중치: 주변 사람 중심으로 모이기
    public float seperateWeight = 1;  // 분리 가중치: 너무 가까운 사람 피하기

    // 목표 추적 가중치
    public float targetWeight = 0.4f;    // 목표(건물, 일자리 등) 쪽으로 이동할 때 가중치

    [Header("Collisions")]
    // 충돌 관련 설정
    public LayerMask obstacleMask;       // 충돌 체크할 레이어 마스크
    public float boundsRadius = .27f;    // 충돌 감지용 구체 반지름
    public float avoidCollisionWeight = 10; // 충돌 회피 힘 가중치
    public float collisionAvoidDst = 5;     // 충돌을 감지할 최대 거리

}
