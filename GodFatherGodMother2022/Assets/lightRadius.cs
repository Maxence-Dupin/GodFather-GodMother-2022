using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightRadius : MonoBehaviour
{
    public EnemyControllerAi enemyControllerAi;
    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.name == "Player")
        {
            enemyControllerAi.viewRadius += 10;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
