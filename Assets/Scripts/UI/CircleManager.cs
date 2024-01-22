using System;
using UnityEngine;

namespace UI
{
    public class CircleManager : MonoBehaviour
    {
        [SerializeField] private LineRenderer circleRenderer;

        private static CircleManager _instance;
        
        public static CircleManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<CircleManager>();
                }

                return _instance;
            }
            set => _instance = value;
        }

        private void Start()
        {
            if (_instance == null)
            {
                _instance = this;
            }
           
        }

        public void DrawCircle(int steps, float radius, Vector3 position)
        {
            circleRenderer.enabled = true;
            circleRenderer.positionCount = steps + 1;
            float x;
            float y;
            float z = 0f;
            float angle = 20f;
 
            for (int i = 0; i < (steps + 1); i++)
            {
                x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
                y = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;
 
                circleRenderer.SetPosition(i,new Vector3(x,y,z) + position );
 
                angle += (360f / steps);
            }
        }

        public void DestroyCircle()
        {
            //Destroy the circle
            circleRenderer.positionCount = 0;
            circleRenderer.enabled = false;
        }
    }
}
