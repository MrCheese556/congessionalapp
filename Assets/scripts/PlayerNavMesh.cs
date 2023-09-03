using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerNavMesh : MonoBehaviour
{ 
    [SerializeField] public Transform movePositionTransform;
    private NavMeshAgent navMeshAgent;
    // Start is called before the first frame update
    private void Awake() {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update() {
        navMeshAgent.destination = movePositionTransform.position;
    }
}
