using System.Collections;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class ItemController : MonoBehaviour, IItemController
{
    [SerializeField] private Quaternion defaultRotation;
    
    // This is wrong, should be get this scriptable object & Gameplay controller from game manager 
    [SerializeField] private ItemsSetting itemsSetting;
    [SerializeField] private GameplayController gameplayController;


    private Rigidbody _rigidbody;
    private Camera _mainCamera;
    private Coroutine _rotateCoroutine;
    private Transform _childObject;

    private Vector3 _pos;
    private Vector3 _rotateDirection;

    private float _height;
    private float _moveSpeed;
    private float _fingerMoveSpeed;
    private float _rotateSpeed;
    private float _resetRotationSpeed;
    private float _backToSceneForce;
    private float _time;

    private bool _isSelected;
    private bool _collectedAll;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }


    private void Start()
    {
        InitialFields();
    }

    private void InitialFields()
    {
        _rotateDirection = itemsSetting.GetRotateDirection;
        _height = itemsSetting.GetHeight;
        _moveSpeed = itemsSetting.GetMoveSpeed;
        _fingerMoveSpeed = itemsSetting.GetFingerMoveSpeed;
        _rotateSpeed = itemsSetting.GetRotateSpeed;
        _resetRotationSpeed = itemsSetting.GetResetRotateSpeed;
        _backToSceneForce = itemsSetting.GetBackToSceneForce;
        _mainCamera = Camera.main;
        _childObject = transform.GetChild(0);
    }

    private void OnMouseDrag()
    {
        _time += Time.fixedDeltaTime;
        
        if(_time < 1f || _isSelected) return;
        
        RemoveGravity();
        FingerMoving();
       
        if (_rotateCoroutine == null) _rotateCoroutine = StartCoroutine(Rotate(transform.GetChild(0)));
    }

    private void OnMouseUp()
    {
        if (_time < 2) Select();
        else EndDrag();

        _time = 0;
    }


    private void EndDrag()
    {
        ApplyGravity();
        if(_rotateCoroutine is not null) StopCoroutine(_rotateCoroutine);
        _rotateCoroutine = null;
    }


    private void Select()
    {
        if(_collectedAll) return;
        
        
        if (!_isSelected)
        {
            gameplayController.SelectedItem(this, out  _isSelected);
        }
        else
        {
            _isSelected = false;
            BackFromBasket();
        }
    }


    private void BackFromBasket()
    {
        gameplayController.RemoveItem(this);
        UnfreezeRigidbodyConstraints();
        ApplyGravity();
        _rigidbody.AddForce(-transform.forward * _backToSceneForce);
    }

    private IEnumerator MoveToTargetPos(Vector3 target)
    {        
        StartCoroutine(ResetChildRotation());

        
        while (Vector3.Distance(transform.position , target) > .15f)
        {
            transform.position = Vector3.Lerp(transform.position, target, _moveSpeed * Time.fixedDeltaTime);
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
        
        transform.position = target;
        FreezeRigidbodyConstraints();
        ResetThisObjectRotation();
        StopCoroutine(MoveToTargetPos(target));

        
        if (_collectedAll) gameObject.SetActive(false);
        else NotifierItemArrive();
    }
    

    private IEnumerator ResetChildRotation()
    {
        _rigidbody.useGravity = false;
        
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


    private void ResetThisObjectRotation()
    {
        transform.rotation = Quaternion.Euler(Vector3.zero);
        StartCoroutine(ResetChildRotation());
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
        StartCoroutine(gameplayController.SelectedItemsController());
    }

    private void FreezeRigidbodyConstraints()
    {
        _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
    }
    
    
    private void UnfreezeRigidbodyConstraints()
    {
        _rigidbody.constraints = RigidbodyConstraints.None;
    }
    
    
    private void FingerMoving()
    {
        if (Vector3.Distance(transform.position, CalculateFingerMoveDirection()) <= .1f) return;
        

        transform.position = 
                Vector3.Lerp(transform.position, CalculateFingerMoveDirection(), _fingerMoveSpeed * Time.fixedDeltaTime);
    }



    private void RemoveGravity()
    {
        if (_rigidbody.useGravity) _rigidbody.useGravity = false;
    }
    

    private void ApplyGravity()
    {
        if (!_rigidbody.useGravity) _rigidbody.useGravity = true;
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
    
    
    
    public void Move(Vector3 target)
    {
        StopAllCoroutines();
        StartCoroutine(MoveToTargetPos(target));
    }

    public bool SetCollectedAll { set => _collectedAll = value; }
}
