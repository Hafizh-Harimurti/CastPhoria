using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFireball : ProjectileBase
{
    public float damage;
    public Vector3 direction;
    void Start()
    {
        Destroy(gameObject, lifetimeMax);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Time.deltaTime * moveSpeed * direction;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject otherGameObject = collider.gameObject;
        if (!otherGameObject.CompareTag(ownerTag) && (otherGameObject.CompareTag("Player") || otherGameObject.CompareTag("Enemy")))
        {
            EntityBase otherEntity = otherGameObject.GetComponent<EntityBase>();
            otherEntity.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
