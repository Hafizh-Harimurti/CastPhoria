using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileArrow : ProjectileBase
{
    public float damage;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifetimeMax);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Time.deltaTime * moveSpeed * transform.right;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject otherGameObject = collider.gameObject;
        if (!otherGameObject.CompareTag(ownerTag) && !otherGameObject.CompareTag("Projectile") && !otherGameObject.CompareTag("Spell"))
        {
            EntityBase otherEntity = otherGameObject.GetComponent<EntityBase>();
            otherEntity.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
