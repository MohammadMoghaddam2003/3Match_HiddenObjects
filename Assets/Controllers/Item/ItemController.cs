using System.Collections;
using System.Text;
using Controllers.Audio;
using Data.Data;
using Data.Events;
using Game_Manager;
using GameplayAssets.Items;
using UnityEngine;

namespace Controllers.Item
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class ItemController : MonoBehaviour
    {
        [SerializeField] private Vector3 defaultRotation;
        [SerializeField] private ItemsSetting itemsSetting;
        
        private Rigidbody _rigidbody;
        private Camera _mainCamera;
        private Coroutine _rotateCoroutine;
        private Transform _childObject;
        private Collider _childCollider;
        private Collider _collider;
        private GameplayData _gameplayData;
        private AudioController _audioController;

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
            _audioController = GameManager.Instance.GetAudioController;
        }

        private void OnMouseDown() => _mouseBeginPos = Input.mousePosition;

        private void OnMouseDrag()
        {
            if(Vector3.Distance(Input.mousePosition,_mouseBeginPos) < _selectOffset || _isSelected) return;
        
            RemoveGravity();
            FingerMoving();

            if (!_drag) _drag = true;
            if (_rotateCoroutine == null) _rotateCoroutine = StartCoroutine(Rotate(transform.GetChild(0)));
        }

        private void OnMouseUp()
        {
            if (Vector3.Distance(Input.mousePosition,_mouseBeginPos) < _selectOffset && !_drag) Select();
            else EndDrag();
            
            ResetChildRotation();
        }

        private void Select()
        {
            if (_gatheringPos)
            {
                PlayWrongSound();
                return;
            }
        
            
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
                PlayReturnSound();
                StartCoroutine(BackFromBasket());
            }
        }
        
        private void PlayCollectSound() => _audioController.CollectObject();
        private void PlayWrongSound() => _audioController.WrongClickObject();
        private void PlayReturnSound() => _audioController.ReturnObject();

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
            if (!_gameplayData.SelectedItemValidation)
            {
                PlayWrongSound();
                return;
            }
            
            PlayCollectSound();
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
        
            while (Vector3.Distance(transform.position , target) > .15f)
            {
                transform.position = Vector3.Lerp(transform.position, target, _moveSpeed * Time.fixedDeltaTime);
                yield return new WaitForSeconds(Time.fixedDeltaTime);
            }
        
            transform.position = target;
            FreezeRigidbodyConstraints();
            StartCoroutine(ResetThisObjectRotation());
            StopCoroutine(ResetThisObjectRotation());
            EnableCollider();
            StopCoroutine(MoveToTargetPos(target));

            NotifierItemArrive();
        }

        private void ResetChildRotation() => _childObject.rotation = transform.rotation;
        
        private void FreezeRigidbodyConstraints() =>_rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        
        private IEnumerator ResetThisObjectRotation()
        {
            while (Quaternion.Angle(_childObject.rotation,Quaternion.Euler(defaultRotation)) > 1)
            { 
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(defaultRotation), _resetRotationSpeed * Time.fixedDeltaTime); 
                yield return new WaitForSeconds(Time.fixedDeltaTime);
            }
            
            
            ResetChildRotation();
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

        private IEnumerator BackFromBasket()
        {
            DisableCollider();
            _gameplayData.UnUsingBasket = Basket;
            _returnedEvent.Raise();
            Basket = null;
            UnfreezeRigidbodyConstraints();
            ApplyGravity();
            _rigidbody.AddForce(-Vector3.forward * _backToSceneForce);

            yield return new WaitForSeconds(.3f);
            EnableCollider();
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


        public void Disabler()
        {
            _rigidbody.isKinematic = true;
            _childCollider.enabled = false;
            _collider.enabled = false;
            this.enabled = false;  
        } 
    }
}
