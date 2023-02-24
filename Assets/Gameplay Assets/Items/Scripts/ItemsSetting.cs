using Data.Data.Scripts;
using Data.Events.Scripts;
using UnityEngine;

namespace Gameplay_Assets.Items.Scripts
{
   [CreateAssetMenu(menuName = "Item/ItemsSetting",fileName = "ItemsSetting")]
   public class ItemsSetting : ScriptableObject
   {
      [Header("Dependency")]
      [SerializeField] private GameplayData gameplayData;
      
      [Header("Setting")]
      [SerializeField] private Vector3 rotateDirection;
      [SerializeField] private float height = 3;
      [SerializeField] private float moveSpeed = 5;
      [SerializeField] private float fingerMoveSpeed = 5;
      [SerializeField] private float rotateSpeed = 5;
      [SerializeField] private float resetRotationSpeed = 2.5f;
      [SerializeField] private float backToSceneForce = 2000;
      [SerializeField] private float selectOffset = 2;

      [Header("Events")]
      [SerializeField] private EventSO selectedEvent;
      [SerializeField] private EventSO returnedEvent;
      [SerializeField] private EventSO arriveToTargetEvent;
      [SerializeField] private EventSO playCollectParticleEvent;
      
      

      public Vector3 GetRotateDirection
      {
         get
         {
            if (rotateDirection.x > 0) return rotateDirection = new Vector3(1, 0, 0);
            if (rotateDirection.y > 0) return rotateDirection = new Vector3(0, 1, 0);
            return rotateDirection = new Vector3(0, 0, 1);
         }
      }
   
      public EventSO GetSelectedEvent { get => selectedEvent; }
   
      public EventSO GetReturnedEvent { get => returnedEvent; }
   
      public EventSO GetArriveToTargetEvent { get => arriveToTargetEvent; }
      
      public EventSO GetPlayCollectParticleEvent { get => playCollectParticleEvent; }
      
      public GameplayData GetGamePlayData { get => gameplayData; }

      public float GetHeight { get => height; }
   
      public float GetMoveSpeed { get => moveSpeed; }
   
      public float GetFingerMoveSpeed { get => fingerMoveSpeed; }
   
      public float GetRotateSpeed { get => rotateSpeed; }
   
      public float GetResetRotateSpeed { get => resetRotationSpeed; }
   
      public float GetBackToSceneForce { get => backToSceneForce; }
      
      public float GetSelectOffset { get => selectOffset; }
   }
}
