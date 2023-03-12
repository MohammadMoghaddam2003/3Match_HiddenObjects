using System.Collections;
using UnityEngine;

namespace GameplayAssets.Items
{
    public class ItemRotation : MonoBehaviour
    {
        public void ResetRotation(Vector3 defaultRotation, float resetRotationSpeed, ItemChildRotation itemChildRotation)
        {
            StartCoroutine(ResetObjectRotation(defaultRotation, resetRotationSpeed, itemChildRotation));
            StopCoroutine(ResetObjectRotation(defaultRotation, resetRotationSpeed, itemChildRotation));
        }
        
        private IEnumerator ResetObjectRotation(Vector3 defaultRotation, float resetRotationSpeed, ItemChildRotation itemChildRotation)
        {
            while (Quaternion.Angle(transform.rotation,Quaternion.Euler(defaultRotation)) > 1)
            { 
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(defaultRotation), resetRotationSpeed * Time.deltaTime); 
                yield return null;
            }
            
            itemChildRotation.ResetRotation(transform.rotation);
        }

    }
}
