
using System.Collections;
using UnityEngine;
namespace Nasser.io.SimpleLogger
{
    public class Cube : MonoBehaviour
    {
        Rigidbody rb;
        public float shakeDown = 1f;
        void Start()
        {
            Debug.Log("Cube");
            rb.velocity = Vector3.zero;
        }

        private void Update()
        {
            if (shakeDown < 1)
            {
                shakeDown += Time.deltaTime;
                transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(2, 2, 2), shakeDown);
            }

        }

        int count = 0;
        private void OnMouseDown()
        {
            Debug.Log("Click  ->  " + count++);
            transform.localScale = new Vector3(3,3,3);
            shakeDown = 0;
        }
        private void OnMouseEnter()
        {
            Debug.LogWarning("Enter  ->  " + count++);
        }

        private void OnMouseExit()
        {
            Debug.LogError("Exit  ->  " + count++);
        }

    }
}
