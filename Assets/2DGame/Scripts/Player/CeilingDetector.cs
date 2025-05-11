using System;
using UnityEngine;

public class CeilingDetector : MonoBehaviour
{
    public event Action Detected;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Detected?.Invoke();
    }
}