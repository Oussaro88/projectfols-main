using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleEntity : BaseEntity
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnDeath()
    {
        //Definition by default for now
        Destroy(gameObject);
    }
}
