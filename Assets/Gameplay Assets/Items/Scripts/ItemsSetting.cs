using UnityEngine;
using UnityEngine.Rendering.Universal;


[CreateAssetMenu(menuName = "Item/ItemsSetting",fileName = "ItemsSetting")]
public class ItemsSetting : ScriptableObject
{
   [SerializeField] private Vector3 rotateDirection;
   [SerializeField] private float height = 3;
   [SerializeField] private float moveSpeed = 5;
   [SerializeField] private float fingerMoveSpeed = 5;
   [SerializeField] private float rotateSpeed = 5;
   [SerializeField] private float resetRotationSpeed = 2.5f;
   [SerializeField] private float backToSceneForce = 2000;


   public Vector3 GetRotateDirection
   {
      get
      {
         if (rotateDirection.x > 0) return rotateDirection = new Vector3(1, 0, 0);
         if (rotateDirection.y > 0) return rotateDirection = new Vector3(0, 1, 0);
         return rotateDirection = new Vector3(0, 0, 1);
      }
   }

   public float GetHeight { get => height; }
   
   public float GetMoveSpeed { get => moveSpeed; }
   
   public float GetFingerMoveSpeed { get => fingerMoveSpeed; }
   
   public float GetRotateSpeed { get => rotateSpeed; }
   
   public float GetResetRotateSpeed { get => resetRotationSpeed; }
   
   public float GetBackToSceneForce { get => backToSceneForce; }
}
