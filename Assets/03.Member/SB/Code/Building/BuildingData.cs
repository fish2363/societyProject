using Mono.Cecil;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildingData", menuName = "Scriptable Objects/BuildingData")]
public class BuildingData : ScriptableObject
{
    [Header("기본 정보")]
    public string buildingName;
    public GameObject buildingPrefab;
    public int cost;
 
    [Header("배치 관련")]
    public Vector3 buildingSize;
    public Vector2 baseSize;
  
}
