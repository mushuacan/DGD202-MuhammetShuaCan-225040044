using UnityEngine;

//This code created with ChatGPT
//rb.MovePosition has changed by me (now player can pass through walls when walks forward and left at the same time. 02.06.2024)
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float turnSpeed = 10f;
    public float jumpForce = 5f; // Z�plama kuvveti
    private bool isGrounded; // Karakterin yere de�ip de�medi�ini kontrol eder
    private Rigidbody rb;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // �mleci kilitle
        Cursor.visible = false; // �mleci gizle
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Hareket Girdilerini Al
        float moveHorizontal = Input.GetAxis("Horizontal"); // a ve d veya sol ve sa� ok tu�lar�
        float moveVertical = Input.GetAxis("Vertical"); // w ve s veya yukar� ve a�a�� ok tu�lar�
        float turnHorizontal = Input.GetAxis("Mouse X"); // Fare yatay hareketi

        // Hareket Y�n�n� Belirle
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical).normalized;

        // Hareketi Uygula (karakterin bakt��� y�ne g�re)
        Vector3 newPositionHorizontal = transform.TransformDirection(moveHorizontal, 0, 0) * moveSpeed * Time.deltaTime;
        Vector3 newPositionVertical = transform.TransformDirection(0, 0, moveVertical) * moveSpeed * Time.deltaTime;
        Vector3 newPosition = rb.position + newPositionHorizontal + newPositionVertical;
        rb.MovePosition(newPosition);

        // D�n��� Uygula
        transform.Rotate(0, turnHorizontal * turnSpeed, 0);

        // Z�plama ��lemi
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
    }
}
