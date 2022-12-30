using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cloth
{
    public class ClothItemBase : MonoBehaviour
    {
        public ClothType clothType;
        public string compareTag = "Player";
        public float duration =2f;

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.CompareTag(compareTag))
            {
                Collect();
            }
        }

        public virtual void Collect()
        {
            Debug.Log("Collect");
            var setup = ClothManager.Instance.GetSetupByType(clothType);
            Player.Instance.ChangeTexture(setup, duration);
            HideObject();
        }

        private void HideObject()
        {
            gameObject.SetActive(false);
        }
    }

}

