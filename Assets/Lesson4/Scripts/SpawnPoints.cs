using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace System_Programming.Lesson4
{
    public class SpawnPoints : MonoBehaviour
    {
        [field: SerializeField] public Transform[] Points { get; private set; }
    }
}