using Mono.Cecil;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildingData", menuName = "Scriptable Objects/BuildingData")]
public class BuildingData : ScriptableObject
{
    [Header("기본 정보")]
    public string buildingName;
    public GameObject buildingPrefab;
    public GameObject basePrefab;
     
   
  
}
