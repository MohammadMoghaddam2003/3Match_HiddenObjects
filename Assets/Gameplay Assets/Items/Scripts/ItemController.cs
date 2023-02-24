using System.Collections;
using System.Text;
using Data.Data.Scripts;
using Data.Events.Scripts;
using UnityEngine;

namespace Gameplay_Assets.Items.Scripts
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class ItemController : MonoBehaviour
    {
        [SerializeField] private Quaternion defaultRotation;
        [SerializeField] private ItemsSetting itemsSetting;
        
        private Rigidbody _rigidbody;
        private Camera _mainCamera;
        private Coroutine _rotateCoroutine;
        private Transform _childObject;
        private Collider _childCollider;
        private Collider _collider;
        private GameplayData _gameplayData;

        private EventSO _selectedEvent;
        private EventSO _returnedEvent;
        private EventSO _arriveToTargetEvent;
        private EventSO _playCollectParticleEvent;

        private Vector3 _pos;
        private Vector3 _mouseBeginPos;
        private Vector3 _rotateDirection;

        private float _height;
        private float _moveSpeed;
        private float _fingerMoveSpeed;
        private float _rotateSpeed;
        private float _resetRotationSpeed;
        private float _backToSceneForce;
        private float _selectOffset;

        private bool _drag;
        private bool _isSelected;
        private bool _gatheringPos;


        public Transform Basket { get; set; }

        
        
        
        private void Awake()
        {
            _childObject = transform.GetChild(0);
            _rigidbody = GetComponent<Rigidbody>();
            _childCollider = _childObject.GetComponent<Collider>();
            _collider = GetComponent<Collider>();
        }
        
        private void Start() => InitialFields();
        
        private void InitialFields()
        {
            _mainCamera = Camera.main;
            _rotateDirection = itemsSetting.GetRotateDirection;
            _height = itemsSetting.GetHeight;
            _moveSpeed = itemsSetting.GetMoveSpeed;
            _fingerMoveSpeed = itemsSetting.GetFingerMoveSpeed;
            _rotateSpeed = itemsSetting.GetRotateSpeed;
            _resetRotationSpeed = itemsSetting.GetResetRotateSpeed;
            _backToSceneForce = itemsSetting.GetBackToSceneForce;
            _selectedEvent = itemsSetting.GetSelectedEvent;
            _returnedEvent = itemsSetting.GetReturnedEvent;
            _arriveToTargetEvent = itemsSetting.GetArriveToTargetEvent;
            _playCollectParticleEvent = itemsSetting.GetPlayCollectParticleEvent;
            _gameplayData = itemsSetting.GetGamePlayData;
            _selectOffset = itemsSetting.GetSelectOffset;
        }

        private void OnMouseDown() => _mouseBeginPos = Input.mousePosition;

        private void OnMouseDrag()
        {
            if(Vector3.Distance(Input.mousePosition,_mouseBeginPos) < _selectOffset) return;
        
            RemoveGravity();
            FingerMoving();

            if (!_drag) _drag = true;
            if (_rotateCoroutine == null) _rotateCoroutine = StartCoroutine(Rotate(transform.GetChild(0)));
        }

        private void OnMouseUp()
        {
            if (Vector3.Distance(Input.mousePosition,_mouseBeginPos) < _selectOffset && !_drag) Select();
            else EndDrag();
        }

        private void Select()
        {
            if(_gatheringPos) return;
        
            
            if (!_isSelected)
            {
                _gameplayData.SelectedItemTag = new StringBuilder(transform.tag);
                _selectedEvent.Raise();
                GetBasket();
                CheckCanMove();
            }
            else
            {
                _isSelected = false;
                BackFromBasket();
            }
        }
        
        private void EndDrag()
        {
            EnableChildCollider();
            ApplyGravity();
            if(_rotateCoroutine is not null) StopCoroutine(_rotateCoroutine);
            _rotateCoroutine = null;
            _drag = false;
        }
        
        private void GetBasket() => Basket = _gameplayData.SelectedBasket;
        
        private void CheckCanMove()
        {
            if (!_gameplayData.SelectedItemValidation) return;
            Move();
        }
        
        private void Move()
        {
            DisableChildCollider();
            DisableCollider();
            SetIsSelected();
            StopAllCoroutines();
            StartCoroutine(MoveToTargetPos(_gameplayData.ItemTarget));
        }
        
        private void EnableChildCollider() => _childCollider.enabled = true;
        
        private void DisableChildCollider() => _childCollider.enabled = false;
        
        private void EnableCollider() => _collider.enabled = true;
        
        private void DisableCollider() => _collider.enabled = false;
        
        private void SetIsSelected() => _isSelected = true;
        
        private IEnumerator MoveToTargetPos(Vector3 target)
        {    
            RemoveGravity();
            StartCoroutine(ResetChildRotation());

        
            while (Vector3.Distance(transform.position , target) > .15f)
            {
                transform.position = Vector3.Lerp(transform.position, target, _moveSpeed * Time.fixedDeltaTime);
                yield return new WaitForSeconds(Time.fixedDeltaTime);
            }
        
            transform.position = target;
            FreezeRigidbodyConstraints();
            ResetThisObjectRotation();
            EnableCollider();
            StopCoroutine(MoveToTargetPos(target));

            NotifierItemArrive();
        }

        private IEnumerator ResetChildRotation()
        {
            while (true)
            {
                if (Quaternion.Angle(_childObject.rotation, defaultRotation) < 1 || Quaternion.Angle(_childObject.rotation, defaultRotation) > 179)
                {
                    _childObject.rotation = Quaternion.Euler(Vector3.zero);
                    break;
                }
            
            
                _childObject.rotation = Quaternion.Lerp(_childObject.rotation,defaultRotation,_resetRotationSpeed * Time.deltaTime);
                yield return new WaitForSeconds(Time.fixedDeltaTime);
            }

            StopCoroutine(ResetChildRotation());
        }
        
        private void FreezeRigidbodyConstraints() =>_rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        
        private void ResetThisObjectRotation()
        {
            transform.rotation = Quaternion.Euler(Vector3.zero);
            StartCoroutine(ResetChildRotation());
        }
        
        private void ApplyGravity()
        {
            if (!_rigidbody.useGravity) _rigidbody.useGravity = true;
        }
        
        private void RemoveGravity()
        {
            if (_rigidbody.useGravity) _rigidbody.useGravity = false;
        }
        
        private void FingerMoving()
        {
            Vector3 position = transform.position;
        
            _fingerMoveSpeed *= Mathf.Abs((CalculateFingerMoveDirection() - position).normalized.magnitude);
            _rigidbody.velocity = (CalculateFingerMoveDirection() - position) * (_fingerMoveSpeed * Time.fixedDeltaTime);
        }
        
        private Vector3 CalculateFingerMoveDirection()
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        
        
            if (Physics.Raycast(ray, out var raycastHit))
            {
                _pos = new Vector3(raycastHit.point.x,_height,raycastHit.point.z);
            }

            return _pos;
        }
        
        private IEnumerator Rotate(Transform rotatingObject)
        {
            while (true)
            {
                rotatingObject.Rotate(_rotateDirection * (_rotateSpeed * Time.fixedDeltaTime));
                yield return new WaitForSeconds(Time.fixedDeltaTime);
            }
        }

        private void NotifierItemArrive()
        {
            if (_gatheringPos) return;
            
            _arriveToTargetEvent.Raise();
        }

        private void BackFromBasket()
        {
            _gameplayData.UnUsingBasket = Basket;
            _returnedEvent.Raise();
            Basket = null;
            UnfreezeRigidbodyConstraints();
            ApplyGravity();
            _rigidbody.AddForce(-transform.forward * _backToSceneForce);
        }
        
        private void UnfreezeRigidbodyConstraints() => _rigidbody.constraints = RigidbodyConstraints.None;
        
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.CompareTag(transform.tag) && _gatheringPos) StartCoroutine(DestroyManage());
        }

        private IEnumerator DestroyManage()
        {
            transform.localScale *= 1.05f;
            yield return new WaitForSeconds(.2f);
            _playCollectParticleEvent.Raise();
            gameObject.SetActive(false);
        }

        public void MoveToGatheringPos()
        {
            if(!_isSelected) return;

            _gatheringPos = true;
            _isSelected = false;
            StopAllCoroutines();
            StartCoroutine(MoveToTargetPos(_gameplayData.GatheringPos));
        }
    }
}
