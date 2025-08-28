
using System.Collections.Generic;
using UnityEngine;

public class UnlockZone : MonoBehaviour
{

    [SerializeField] public int price = 100;
     public StructureBuilder structureToUnlock;
    [SerializeField] public bool isFirstPoint;
    public List<UnlockZone> unlocksNextZones; //list of zones to activate after unlocking
    private bool isUnlocked = false;



    private void Start()
    {
        if (!isFirstPoint)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isUnlocked)
        {
            if (GameManager.Instance.SpendMoney(price)) //check if player has balance for unlock
            {
                UnlockFeature();
            }
            else
            {
                Debug.Log("not enough money");
            }
        }
    }

    private void UnlockFeature()
    {
        isUnlocked = true;
        structureToUnlock.StartBuild();
        Debug.Log($"attempt to unlock {structureToUnlock.structureName}");
        foreach (UnlockZone zone in unlocksNextZones)
        {
            zone.gameObject.SetActive(true);
        }

        gameObject.SetActive(false); //disable trigger zone


       
    }
}
