using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Wallet", order = 1)]
public class Wallet : AbstractSOContainer
{

    #region variables and properties
    [SerializeField] private float money = 0;
    public float Money
    {
        get { return money; }
        set { money = value; }
    }

    #endregion

    #region Functions
    public void GiveMoney(float amount)
    {
        if (amount <= 0)
        {
            UnityEngine.Debug.Log("can't give <= 0 dollars.");
            return; //TODO: throw exception?
        }

        if (amount > money)
        {
            UnityEngine.Debug.Log("can't give more money than you have. This game does not currently allow for debt mechanics.");
            return; //TODO: throw exception?
        }

        money -= amount;
    }

    public void GetMoney(float amount)
    {
        if (amount <= 0)
        {
            UnityEngine.Debug.Log("can't 'get' 0 or negative money");
            return; //TODO: throw exception?
        }

        money += amount;
    }
    #endregion

}
