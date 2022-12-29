using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Checkpoint
{
    public class CheckpointBase : MonoBehaviour
    {

        public MeshRenderer meshRenderer;

        public int key = 01;

        private bool checkpointActived = false;
        private string _checkpointKey = "Checkpoint Key";
        private void OnTriggerEnter(Collider other)
        {
            if (!checkpointActived && other.transform.tag == "Player")
            {
                CheckCheckpoint();
            }
        }

        private void CheckCheckpoint()
        {
            TurnItOn();
            SaveCheckpoint();
        }

        [NaughtyAttributes.Button]
        private void TurnItOn()
        {
            meshRenderer.material.SetColor("_EmissionColor", Color.white);
        }

        private void TurnItOff()
        {
            meshRenderer.material.SetColor("_EmissionColor", Color.grey);
        }

        private void SaveCheckpoint()
        {
            if(PlayerPrefs.GetInt(_checkpointKey, 0)> key)
            {
                PlayerPrefs.SetInt(_checkpointKey, key);
            }

            CheckpointManager.Instance.SaveCheckPoint(key);

            checkpointActived = true;
        }

    }
}
