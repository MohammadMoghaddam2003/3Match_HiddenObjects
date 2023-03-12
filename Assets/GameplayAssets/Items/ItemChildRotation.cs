using System.Collections;
using UnityEngine;

namespace GameplayAssets.Items
{
    public class ItemChildRotation : MonoBehaviour
    {
        private Coroutine _rotateCoroutine;
        

        public void StartRotate(Vector3 rotateDirection, float rotateSpeed)
        {
            if (_rotateCoroutine is null) _rotateCoroutine = StartCoroutine(Rotate(rotateDirection, rotateSpeed));
        }
        
        public void StopRotate()
        {
            if(_rotateCoroutine is not null) StopCoroutine(_rotateCoroutine);
            _rotateCoroutine = null;
        }
        
        public void ResetRotation(Quaternion rotation) => transform.rotation = rotation;

        private IEnumerator Rotate(Vector3 rotateDirection, float rotateSpeed)
        {
            while (true)
            {
                transform.Rotate(rotateDirection * (rotateSpeed * Time.deltaTime));
                yield return null;
            }
        }
    }
}
