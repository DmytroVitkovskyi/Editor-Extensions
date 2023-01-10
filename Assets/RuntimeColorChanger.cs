using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuntimeColorChanger : MonoBehaviour
{
    public Color color = Color.white;
    private void Awake()
    {
        GetComponent<Renderer>().material.color = color;
    }
}
