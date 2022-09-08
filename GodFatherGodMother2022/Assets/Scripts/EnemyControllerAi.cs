using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
public class EnemyControllerAi : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public float startWaitTime = 4;
    public float timeToRotate = 2;
    public float speedWalk = 6;
    public float speedRun = 9;

    //les angle pour la vue et la taille de la vision
    public float viewRadius = 10;
    public float nearViewRadius = 10;
    public float viewAngle = 10;
    public float nearViewAngle = 10;
    public LayerMask playerMask;
    public LayerMask obstacleMask;
    public float meshResolution = 1f;
    public int edgeIteration = 4;
    public float edgeDistance = 0.5f;
    public GameObject playerInGame;

    //different endroit ou l'ai va aller
    public Transform[] waypoints;
    public int m_CurrentWaypointIndex;

    public Vector3 playerLastPosition = Vector3.zero;
    Vector3 m_PlayerPosition;

    float m_WaitTime;
    float m_TimeToRotate;

    bool m_PlayerInRange;
    bool m_PlayerNear;
    bool m_IsPatrol;
    bool m_CaughtPlayer;
   public bool canSee;
    

    // Start is called before the first frame update
    void Start()
    {
        //initialisation des variable
        m_PlayerPosition = Vector3.zero;
        m_IsPatrol = true;
        m_CaughtPlayer = false;
        m_PlayerInRange = false;
        m_WaitTime = startWaitTime;
        m_TimeToRotate = timeToRotate;
        canSee = false;
        m_CurrentWaypointIndex = 0;
        navMeshAgent = GetComponent<NavMeshAgent>();

        navMeshAgent.isStopped = false;
        navMeshAgent.speed = speedWalk;
        navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);

       

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
            
        }
    }
    // Update is called once per frame
    void Update()
    {
        EnviromentView();

        if(!m_IsPatrol && canSee == true)
        {
            Chasing();
        }
        else
        {
            Patrolling();
        }
    }
    //LE CHASE !!!!!! la chasse mon gars
    private void Chasing()
    {
        m_PlayerNear = false;
        playerLastPosition = Vector3.zero;
       

            if (!m_CaughtPlayer)
            {
                //canSee = true;
                Move(speedRun);
                navMeshAgent.SetDestination(m_PlayerPosition);


            }
        
        //s'il est pas pres du joueur il peut re partir en patrouille
        if(navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            if(m_WaitTime <= 0 && !m_CaughtPlayer && Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) > 6f)
            {
                //navMeshAgent.SetDestination(playerLastPosition);
                m_IsPatrol =true;
                m_PlayerNear=false;
                Move(speedWalk);
                m_TimeToRotate = timeToRotate;
                m_WaitTime = startWaitTime;
                navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
                canSee = false;

            }
            else
            {
                if(Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position )>= 2.5f)
                    {
                    Stop();
                    m_WaitTime -= Time.deltaTime;
                }
            }
        }
    }
    private void Patrolling()
    {
        if(m_PlayerNear)
        {
            //si l'ennemie est proche du joueru il va vers sa position
            if(m_TimeToRotate <= 0)
            {

                Move(speedWalk);
                LookingPlayer(playerLastPosition);
            }
            else
            {
                Stop();
                m_TimeToRotate -= Time.deltaTime;
            }
        }
        else
        {
             m_PlayerNear = false;
            playerLastPosition = Vector3.zero;
            navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
            if(navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                if(m_WaitTime >= 0)
                {
                    NextPoint();
                    Move(speedWalk);
                    m_WaitTime -= Time.deltaTime;
                }
            }
        }
    }
    void Move(float speed)
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.speed = speed;
    }
    //Stop les mouvement de l'enemy
    void Stop()
    {
        navMeshAgent.isStopped = true;
        navMeshAgent.speed = 0;
    }
    //Enemy va au prochain waypoint
    public void NextPoint()
    {
        //ajouté 1 a l'index de waypoint pour qu'il aille au point suivant
        //m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
        navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);



    }
    void CaughtPlayer()
    {
        m_CaughtPlayer = true;
    }
    //ici l'ai va aller a la dernier position connu du joueur puis reviendra a son etat de patrouille
    void LookingPlayer(Vector3 player)
    {
        navMeshAgent.SetDestination(player);
        
        if (Vector3.Distance(transform.position, player) <= 0.3)
        {
            


                if (m_WaitTime <= 0)
                {
                    m_PlayerNear = false;
                    Move(speedWalk);
                    navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
                    m_WaitTime = startWaitTime;
                    Debug.Log(m_WaitTime);
                    m_TimeToRotate = timeToRotate;
                }
                else
                {
                    Stop();
                    m_WaitTime -= Time.deltaTime;
                }
            
        }
    }
    //Abilité de l'ennemie a nous voir et les obstacle
    void EnviromentView()
    {
        //crée une sphrere autour de l'ennemie pour qu'il nous voi
        Collider[] playerInRange = Physics.OverlapSphere(transform.position, viewRadius, playerMask);
        Collider[] playerInRangeNear = Physics.OverlapSphere(transform.position, nearViewRadius, playerMask);
        

        for (int i = 0; i < playerInRange.Length; i++)
        {
            //enregistrer la position du player apres l'avoir vue
            Transform player = playerInRange[i].transform;
            Vector3 dirToPlayer = (player.position - transform.position).normalized;
           
            //l'angle a laquel l'enemie peut nous voir
            if (Vector3.Angle(transform.forward, dirToPlayer) < viewAngle / 2 )
            {
                float dstToPlayer = Vector3.Distance(transform.position, player.position);
                if (!Physics.Raycast(transform.position, dirToPlayer, dstToPlayer, obstacleMask))
                {
                    m_PlayerInRange = true;
                    m_IsPatrol = false;
                    canSee = true;
                }
                //verifier si le joueur est deriere un obstcle
                else
                {
                    m_PlayerInRange = false;
                    canSee = false;
                    
                }
            }
        
            if (Vector3.Distance(transform.position, player.position) > viewRadius)
            {
                m_PlayerInRange = false;
                //canSee = false;
            }
        
            if (m_PlayerInRange)
            {
                m_PlayerPosition = player.transform.position;
            }
        }
        for (int i = 0; i < playerInRangeNear.Length; i++)
        {
            //enregistrer la position du player apres l'avoir vue
            Transform player = playerInRangeNear[i].transform;
            Vector3 dirToPlayer = (player.position - transform.position).normalized;
           
            //l'angle a laquel l'enemie peut nous voir
            if (Vector3.Angle(transform.forward, dirToPlayer) < nearViewRadius / 2 )
            {
                float dstToPlayer = Vector3.Distance(transform.position, player.position);
                if (!Physics.Raycast(transform.position, dirToPlayer, dstToPlayer, obstacleMask))
                {
                    m_PlayerInRange = true;
                    m_IsPatrol = false;
                }
                //verifier si le joueur est deriere un obstcle
                else
                {
                    m_PlayerInRange = false;
                }
            }
        
            if (Vector3.Distance(transform.position, player.position) > nearViewRadius)
            {
                m_PlayerInRange = false;
            }
        
            if (m_PlayerInRange)
            {
                m_PlayerPosition = player.transform.position;
            }
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, viewRadius);
        Gizmos.DrawSphere(transform.position, nearViewRadius);
    }
}
