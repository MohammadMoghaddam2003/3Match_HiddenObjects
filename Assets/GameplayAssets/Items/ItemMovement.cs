using System.Collections;
using GameData.Data;
using UnityEngine;

namespace GameplayAssets.Items
{
    public class ItemMovement : MonoBehaviour
    {
        public static FinishedJob FinishedMoving;
        public static FinishedJob FinishedReturn;

        private Camera _mainCamera;
        private bool _gatheringPos;

        

        private void Awake() => _mainCamera = Camera.main;
        
        public void FingerMoving(Rigidbody itemRigidbody, float fingerMoveSpeed, ref Vector3 pos, float height)
        {
            Vector3 position = transform.position;
            Vector3 direction = (CalculateFingerMoveDirection(ref pos, height) - position);
        
            fingerMoveSpeed *= Mathf.Abs(direction.normalized.magnitude);
            itemRigidbody.velocity = direction * (fingerMoveSpeed * Time.fixedDeltaTime);
        }
        
        public void MoveToGatheringPosition(GameplayData gameplayData, float moveSpeed)
        {
            _gatheringPos = true;
            Move(gameplayData.GatheringPos, moveSpeed);
        }
        
        public void MoveToBasket(GameplayData gameplayData, float moveSpeed) => Move(gameplayData.ItemTarget, moveSpeed);
        
        private void Move(Vector3 target, float moveSpeed)
        {
            StopAllCoroutines();
            StartCoroutine(MoveToTargetPos(target, moveSpeed));
        }

        private Vector3 CalculateFingerMoveDirection(ref Vector3 pos, float height)
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        
        
            if (Physics.Raycast(ray, out var raycastHit))
            {
                pos = new Vector3(raycastHit.point.x,height,raycastHit.point.z);
            }

            return pos;
        }
        
        private IEnumerator MoveToTargetPos(Vector3 target, float moveSpeed)
        {    
        
            while (Vector3.Distance(transform.position , target) > .25f)
            {
                transform.position = Vector3.Lerp(transform.position, target, moveSpeed * Time.fixedDeltaTime);
                yield return new WaitForSeconds(Time.fixedDeltaTime);
            }
            
            
            transform.position = target;

            StopCoroutine(MoveToTargetPos(target, moveSpeed));

            if (!_gatheringPos) FinishedMoving();
        }
        
        public void ReturnFromBasket(Rigidbody itemRigidbody, float backToSceneForce) => StartCoroutine(Return(itemRigidbody, backToSceneForce));
        
        private IEnumerator Return(Rigidbody itemRigidbody,float backToSceneForce)
        {
            itemRigidbody.AddForce(-Vector3.forward * backToSceneForce);

            yield return new WaitForSeconds(.23f);

            FinishedReturn();
        }
    }
    
    
    public delegate void FinishedJob();
}
