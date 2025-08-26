using System.Collections.Generic;
using UnityEngine;

public class PolicyCardBoard : MonoBehaviour
{
    [SerializeField] private GameObject ecoCardGroup;
    [SerializeField] private GameObject industryCardGroup;
    [SerializeField] private GameObject deckPanel;

    private Dictionary<string, GameObject> cardGroups;

    private void OnEnable()
    {
        ecoCardGroup.SetActive(true);
        industryCardGroup.SetActive(false);
        deckPanel.SetActive(false);
    }
    private void Awake()
    {
        cardGroups = new Dictionary<string, GameObject>();
        cardGroups.Add("Eco", ecoCardGroup);
        cardGroups.Add("Industry", industryCardGroup);
        cardGroups.Add("Deck", deckPanel);
    }

    public void ShowGroup(string categoryUI)
    {
        foreach (var group in cardGroups)
        {
           if(group.Key == categoryUI)
                group.Value.SetActive(true);
           else
                group.Value.SetActive(false);
        }
    }


}
