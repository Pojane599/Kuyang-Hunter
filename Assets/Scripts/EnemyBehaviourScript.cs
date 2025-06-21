using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviourScript : MonoBehaviour
{
    public float Hitpoints;
    public float MaxHitpoints = 5;
    public HealthBarBehavior Healthbar;
    // Start is called before the first frame update
    void Start()
    {
        Hitpoints = MaxHitpoints;
        Healthbar.SetHeatlh(Hitpoints, MaxHitpoints);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeHit(float damage)
    {
        Hitpoints -= damage;

        if (Hitpoints <= 0)
        {
            Destroy(gameObject);
        }
    }   
}
