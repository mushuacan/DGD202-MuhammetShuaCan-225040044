using UnityEngine;

public class SnakeHead : MonoBehaviour
{
    public float rotationSpeed = 200f;

    void Update()
    {
        Vector3 direction = Vector3.zero;

        // W, A, S, D tuþlarýný kontrol et
        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector3.back;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector3.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector3.right;
        }

        // Eðer direction sýfýr deðilse, yönü normalize et ve rotasyonu uygula
        if (direction != Vector3.zero)
        {
            Rotate(direction.normalized);
        }
    }

    void Rotate(Vector3 direction)
    {
        // Parent objesine göre dönüþ yap
        Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, transform.parent.rotation * toRotation, rotationSpeed * Time.deltaTime);
    }
}

