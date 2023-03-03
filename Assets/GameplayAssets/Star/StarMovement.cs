using System.Collections;
using Controllers.UI;
using UnityEngine;
using UnityEngine.UI;

namespace GameplayAssets.Star
{
    public class StarMovement : MonoBehaviour
    {
        [SerializeField] private float speed = 20;
        [SerializeField] private float waitTime = .3f;
    

        
        private Rigidbody _rigidbody;
        private Coroutine _move;
        private UIController _uiController;
        private Image _image;
        private Vector3 _targetPos;
        private Vector3 _targetScale;

        public UIController SetUIController { set => _uiController = value; }


        private void Awake() => _rigidbody = GetComponent<Rigidbody>();

        public void StartMove()
        {
            _image = GetComponent<Image>();
            _image.enabled = false;
            _move = StartCoroutine(Move());
        } 


        private IEnumerator Move()
        {
            _targetPos = _uiController.GetPlayerStar;
            _targetScale = _uiController.GetTargetScale;
            speed *= Screen.width;    
            yield return new WaitForSeconds(waitTime);
            _rigidbody.useGravity = false;
            _image.enabled = true;

            while (Vector3.Distance(transform.position,_targetPos) > 60f)
            {
                Vector3 direction = _targetPos - transform.position;
                ChangeScale();
                _rigidbody.velocity = (direction.normalized * speed) * Time.deltaTime;
                yield return null;
            }
            
            _uiController.ChangePlayerStarSprite();
            StopCoroutine(_move);
            gameObject.SetActive(false);
        }
        
        
        private void ChangeScale() => transform.localScale = Vector3.Lerp(transform.localScale,_targetScale,(speed * 10) * Time.deltaTime);
    }
}
