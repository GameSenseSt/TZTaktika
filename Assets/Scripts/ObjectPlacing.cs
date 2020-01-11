using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ObjectPlacing : MonoBehaviour
{
    //For putting GameObjects in "cells"
    void Update()
    {
        transform.position = new Vector3(Mathf.Round(transform.position.x),
                                         Mathf.Round(transform.position.y),
                                         0) ;
    }
}
