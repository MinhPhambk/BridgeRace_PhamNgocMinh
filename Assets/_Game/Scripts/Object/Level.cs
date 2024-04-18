using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Level : MonoBehaviour
{
    [SerializeField] private NavMeshData navMeshData;
    [SerializeField] private Transform finishPosotion;

    public Vector3 GetEndPosition()
    {
        return finishPosotion.position;
    }
}
