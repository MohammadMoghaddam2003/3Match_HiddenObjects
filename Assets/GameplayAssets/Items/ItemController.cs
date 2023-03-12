using System.Collections;
using System.Text;
using GameData.Data;
using UnityEngine;

namespace GameplayAssets.Items
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(ItemMovement))]
    [RequireComponent(typeof(ItemRotation))]
    [RequireComponent(typeof(ItemColliderManagement))]
    public class ItemController : MonoBehaviour
    {
        [SerializeField] private Vector3 defaultRotation;
        [SerializeField] private ItemsSetting itemsSetting;
        [SerializeField] private Gameplay.GameplayController gameplayController;
        
        
        private Rigidbody _rigidbody;
        private GameplayData _gameplayData;
        private ItemMovement _itemMovement;
        private ItemChildRotation _itemChildRotation;
        private ItemRotation _itemRotation;
        private ItemChildCollider _itemChildCollider;
        private ItemColliderManagement _itemColliderManagement;
        
        private Vector3 _pos;
        private Vector3 _mouseBeginPos;
        private Vector3 _rotateDirection;

        private float _height;
        private float _fingerMoveSpeed;
        private float _moveSpeed;
        private float _rotateSpeed;
        private float _resetRotationSpeed;
        private float _backToSceneForce;
        private float _selectOffset;
        private float _touchDistance;

        private bool _isSelected;
        private bool _gatheringPos;
        private bool _drag;
        
        private Transform Basket { get; set; }
        
        public Gameplay.GameplayController SetGameplayController { set => gameplayController = value; }
        
        
        
        private void Awake() => InitialFields();
        
        private void InitialFields()
        {
            _itemMovement = GetComponent<ItemMovement>();
            _rigidbody = GetComponent<Rigidbody>();
            _itemRotation = GetComponent<ItemRotation>();
            _itemColliderManagement = GetComponent<ItemColliderManagement>();
            _itemChildRotation = transform.GetChild(0).GetComponent<ItemChildRotation>();
            _itemChildCollider = transform.GetChild(0).GetComponent<ItemChildCollider>();

            _isSelected = false;
            _rotateDirection = itemsSetting.GetRotateDirection;
            _height = itemsSetting.GetHeight;
            _moveSpeed = itemsSetting.GetMoveSpeed;
            _fingerMoveSpeed = itemsSetting.GetFingerMoveSpeed;
            _rotateSpeed = itemsSetting.GetRotateSpeed;
            _resetRotationSpeed = itemsSetting.GetResetRotateSpeed;
            _backToSceneForce = itemsSetting.GetBackToSceneForce;
            _gameplayData = itemsSetting.GetGamePlayData;
            _selectOffset = itemsSetting.GetSelectOffset;
        }

        private void OnMouseDown() => _mouseBeginPos = Input.mousePosition;

        private void OnMouseDrag()
        {
            _touchDistance  = Vector3.Distance(Input.mousePosition, _mouseBeginPos) / 100;
            if(_touchDistance < _selectOffset || _isSelected) return;
        
            FingerMove();
        }

        private void FingerMove()
        {
            RemoveGravity();
            _itemMovement.FingerMoving(_rigidbody,_fingerMoveSpeed,ref _pos,_height);

            if (!_drag) _drag = true;
            _itemChildRotation.StartRotate(_rotateDirection, _rotateSpeed);
        }

        private void OnMouseUp()
        {
            if (_touchDistance < _selectOffset && !_drag) Select();
            else if(!_isSelected) EndDrag();
            
            _itemChildRotation.ResetRotation(transform.rotation);
        }

        private void Select()
        {
            if (_gatheringPos)
            {
                gameplayController.PlayWrongClickObjectSound();
                return;
            }
            
            if (!_isSelected)
            {
                _gameplayData.SelectedItemTag = new StringBuilder(transform.tag);
               if(gameplayController.SelectedItem())
               {
                   SelectedManage();
                   GetBasket();
                   Move();
                   SetCompleteCollectionMethods();
               }
            }
            else
            {
                gameplayController.PlayReturnObjectSound();
                SelectedManage();
                ReturnManager();
            }
        }

        private void SetCompleteCollectionMethods() => gameplayController.CompleteCollection += MoveToGatheringPos;
        
        private void EndDrag()
        {
            _itemChildCollider.EnableChildCollider();
            ApplyGravity();
            _itemChildRotation.StopRotate();
            _drag = false;
        }
        
        private void GetBasket() => Basket = _gameplayData.SelectedBasket;

        private void Move()
        {
            _itemChildCollider.DisableChildCollider();
            _itemColliderManagement.DisableCollider();
            RemoveGravity();
            AddFinishedMovingMethods();
            _itemMovement.MoveToBasket(_gameplayData, _moveSpeed);
        }
        
        private void AddFinishedMovingMethods()
        {
            ItemMovement.FinishedMoving += FreezeRigidbodyConstraints;
            ItemMovement.FinishedMoving += ResetRotation;
            ItemMovement.FinishedMoving += _itemColliderManagement.EnableCollider;
            ItemMovement.FinishedMoving += NotifierItemArrive;
        }
        
        private void RemoveFinishedMovingMethods()
        {
            ItemMovement.FinishedMoving -= FreezeRigidbodyConstraints;
            ItemMovement.FinishedMoving -= ResetRotation;
            ItemMovement.FinishedMoving -= _itemColliderManagement.EnableCollider;
            ItemMovement.FinishedMoving -= NotifierItemArrive;
        }

        private void SelectedManage() => _isSelected = !_isSelected;
        
        private void FreezeRigidbodyConstraints() => _rigidbody.constraints = RigidbodyConstraints.FreezeAll;

        private void ApplyGravity()
        {
            if (!_rigidbody.useGravity) _rigidbody.useGravity = true;
        }
        
        private void RemoveGravity()
        {
            if (_rigidbody.useGravity) _rigidbody.useGravity = false;
        }

        private void ResetRotation() => _itemRotation.ResetRotation(defaultRotation, _resetRotationSpeed, _itemChildRotation);

        private void NotifierItemArrive()
        {
            if (_gatheringPos) return;
            gameplayController.CheckBasket();
            
            RemoveFinishedMovingMethods();
        }

        private void ReturnManager()
        {
            _itemColliderManagement.DisableCollider();
            ReturnBasketManage();
            
            gameplayController.RemoveItem();
            
            UnfreezeRigidbodyConstraints();
            ApplyGravity();
            _itemMovement.ReturnFromBasket(_rigidbody,_backToSceneForce);
            
            ItemMovement.FinishedReturn += _itemColliderManagement.EnableCollider;
        }

        private void ReturnBasketManage()
        {
            _gameplayData.UnUsingBasket = Basket;
            Basket = null;
        }
        
        private void UnfreezeRigidbodyConstraints() => _rigidbody.constraints = RigidbodyConstraints.None;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.CompareTag(transform.tag) && _gatheringPos) StartCoroutine(DestroyManage());
        }

        private IEnumerator DestroyManage()
        {
            ScaleUp();
            yield return new WaitForSeconds(.2f);
            gameplayController.PlayCollectParticleManager();
            gameObject.SetActive(false);
        }
        
        private void ScaleUp() => transform.localScale *= 1.05f;
        
        public void MoveToGatheringPos()
        {
            if(!_isSelected) return;

            _gatheringPos = true;
            SelectedManage();
            _itemMovement.MoveToGatheringPosition(_gameplayData, _moveSpeed);
        }
        

        public void Disabler()
        {
            _rigidbody.isKinematic = true;
            _itemChildCollider.DisableChildCollider();
            _itemColliderManagement.DisableCollider();
            this.enabled = false;  
        } 
    }
}
