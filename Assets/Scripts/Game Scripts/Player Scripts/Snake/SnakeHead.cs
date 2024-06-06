using UnityEngine;

public class SnakeHead : MonoBehaviour
{
    public float rotationSpeed = 200f;

    void Update()
    {
        // W, A, S, D tuþlarýný kontrol et
        if (Input.GetKey(KeyCode.W))
        {
            Rotate(Vector3.forward);
        }
        if (Input.GetKey(KeyCode.S))
        {
            Rotate(Vector3.back);
        }
        if (Input.GetKey(KeyCode.A))
        {
            Rotate(Vector3.left);
        }
        if (Input.GetKey(KeyCode.D))
        {
            Rotate(Vector3.right);
        }
    }

    void Rotate(Vector3 direction)
    {
        // Parent objesine göre dönüþ yap
        Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, transform.parent.rotation * toRotation, rotationSpeed * Time.deltaTime);
    }
}
