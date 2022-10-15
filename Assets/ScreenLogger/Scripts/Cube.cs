
using UnityEngine;
namespace Nasser.io.SimpleLogger
{
    public class Cube : MonoBehaviour
    {

        void Start()
        {
            Debug.Log("Cube");
        }

        int count = 0;
        private void OnMouseDown()
        {
            Debug.Log("Click  ->  " + count++);
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
