using System.Collections;
using Controllers.UI;
using UnityEngine;
using UnityEngine.UI;

namespace GameplayAssets.Star
{
    public class StarMovement : MonoBehaviour
    {
        [SerializeField] private Vector3 defaultScale;
        [SerializeField] private Vector3 defaultRotation;
        [SerializeField] private float speed = 20;
        [SerializeField] private float waitTime = .3f;
    

        
        private Coroutine _move;
        private UIController _uiController;
        private Image _image;
        private Vector3 _targetPos;
        private Vector3 _targetScale;

        public UIController SetUIController { set => _uiController = value; }



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
            yield return new WaitForSeconds(waitTime);
            _image.enabled = true;

            while (Vector3.Distance(new Vector3( transform.position.x,0 ,  transform.position.z),new Vector3(_targetPos.x,0,_targetPos.z)) >.45f)
            {
                Vector3 direction = _targetPos-transform.position;
                ChangeScale();
                transform.Translate( (direction.normalized * speed) * Time.deltaTime);
                yield return null;
            }
            
            _uiController.ChangePlayerStarSprite();
            StopCoroutine(_move);
            gameObject.SetActive(false);
        }
        
        
        private void ChangeScale() => transform.localScale = Vector3.Lerp(transform.localScale,_targetScale,(speed * 10) * Time.deltaTime);


        public void SetDefault()
        {
            transform.localScale = defaultScale;
            transform.localRotation = Quaternion.Euler(defaultRotation);
        }
    }
}
