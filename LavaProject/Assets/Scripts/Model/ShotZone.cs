using System;
using UnityEngine;


public class ShotZone : MonoBehaviour
{
    public Action InZone = delegate { };
    public Action OutZone = delegate { };

    private void OnTriggerEnter(Collider other)
    {
        var player = other.gameObject.GetComponent<Player>();

        if (player != null)
        {
            InZone.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.gameObject.GetComponent<Player>();

        if (player != null)
        {
            OutZone.Invoke();
        }
    }
}
