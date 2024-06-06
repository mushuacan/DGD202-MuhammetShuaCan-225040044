using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float swaySpeed = 2f; 
    public int position = 1;

    private float timer = 0f;

    private void Start()
    {
        timer = position / 3;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        // Karakteri saða veya sola salla
        if (Time.timeScale != 0f )
        {
            transform.localPosition += (Vector3.right * Mathf.Sin(timer * 8) * swaySpeed * 0.003f);
        }
    }
}
