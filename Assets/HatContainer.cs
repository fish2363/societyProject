using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class HatContainer : MonoBehaviour
{

    private List<Hat> hats = new();

    private void Awake()
    {
        hats = GetComponentsInChildren<Hat>(true).ToList();
    }

    public void RandomWearHat()
    {
        int rand = Random.Range(0,hats.Count-1);
        Debug.Log(hats.Count);
        Debug.Log(rand);
        hats[rand].gameObject.SetActive(true);
    }
}
