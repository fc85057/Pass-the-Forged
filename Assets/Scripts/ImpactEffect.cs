using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactEffect : MonoBehaviour
{

    void Start()
    {
        Destroy(gameObject, 0.3f);
    }

}
