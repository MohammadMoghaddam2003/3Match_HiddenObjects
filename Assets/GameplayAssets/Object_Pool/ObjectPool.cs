using System.Collections;
using UnityEngine;

namespace GameplayAssets.Object_Pool
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] private GameObject star;
        [SerializeField] private Transform uICanvas;
        [SerializeField] private int starsCount = 16;
        
        
        private Queue _stars = new Queue();
        private GameObject _star;

        
        
        private void Start() => Generate();
        
        private void Generate()
        {
            for (int i = 0; i < starsCount; i++)
            {
                _star = Instantiate(star, Vector3.zero, Quaternion.identity, uICanvas);
                _star.SetActive(false);
                _stars.Enqueue(_star);
            }
        }

        public GameObject GetStar
        {
            get
            {
                _star = (GameObject)_stars.Peek();
                _star.SetActive(true);
                return (GameObject) _stars.Dequeue();;    
            }
        }

        public GameObject RecycleStar
        {
            set
            {
                _star = value;
                _star.SetActive(false);
                _stars.Enqueue(_star);
            }
        }
    }
}
