using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Itens;

public class PlayerMagneticTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        ItemColectableBase i = other.transform.GetComponent<ItemColectableBase>();
        if(i != null)
        {
            i.gameObject.AddComponent<Magnetic>();
        }
    }
}
