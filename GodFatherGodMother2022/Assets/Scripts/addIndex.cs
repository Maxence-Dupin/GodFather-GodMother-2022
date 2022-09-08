using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addIndex : MonoBehaviour
{
    public EnemyControllerAi enemyControllerAi;
    public int indexAdd;
    public bool IsLast;
    private void OnTriggerEnter(Collider col)
    {
    
        if(IsLast == true)
        {
            enemyControllerAi.m_CurrentWaypointIndex -= enemyControllerAi.m_CurrentWaypointIndex;
        }
        else if(col.gameObject.name == "Enemy")
        {
            enemyControllerAi.m_CurrentWaypointIndex += 1;
            print("PLUS 1 mouahaha");
        }
    }
}
