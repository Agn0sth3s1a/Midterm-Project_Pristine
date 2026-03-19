using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof(NavMeshAgent))]

public class EnemyNavigation : MonoBehaviour
{

    [SerializeField] LevelManager levelManager;

    [SerializeField] GameObject TheBody;

    public LayerMask groundMask;

    public Transform target;

    public bool Pancaked;

    NavMeshAgent agent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent> ();
        Pancaked = false;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, 100f, groundMask))
            {
                agent.SetDestination(hit.point);
            }
        }*/

        if(!Pancaked)
        {
            agent.SetDestination(target.position);
            TheBody.transform.position = this.transform.position;
            TheBody.transform.rotation = this.transform.rotation;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        levelManager.PlayerLost();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Debug.Log("Player hit enemy");
            Pancaked = true;
            levelManager.PlayerWon();
        }
    }
}
