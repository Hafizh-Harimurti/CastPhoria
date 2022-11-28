using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFireball : ProjectileBase
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifetimeMax);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * Time.deltaTime * moveSpeed;
    }
}
