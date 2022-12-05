using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCashGrab : SpellBase
{
    public float pullSpeed;
    public List<GameObject> coins;
    public GameObject owner;
    void Start()
    {
        SpellCoin coin;
        foreach (GameObject coinGameObject in coins)
        {
            coin = coinGameObject.GetComponent<SpellCoin>();
            coin.SetCoinMovement(transform.position, pullSpeed, true);
        }
        Destroy(gameObject);
    }
}
