using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public int damage = 20;
    public Health HealthManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D Collision)
    {
        if (Collision.gameObject.tag=="Player")
        {
            if (HealthManager == null)
            {
                HealthManager = Collision.gameObject.GetComponent<Health>();
            }
            HealthManager.TakeDamage(damage);
        }
    }
}
