using UnityEngine;

public class SnakeBody : MonoBehaviour
{
    public float swaySpeed = 2f;
    public int position = 1;
    public float howMuchSway = 1f;

    private float timer = 0f;

    private void Start()
    {
        timer = position / 3;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        // Karakteri saða veya sola salla
        if (Time.timeScale != 0f)
        {
            transform.localPosition += (Vector3.right * Mathf.Sin(Time.time * 8) * swaySpeed * 0.003f * howMuchSway / position);
        }
    }

    // Segment sýrasýný ayarlamak için bir metod
    public void SetPosition(int index)
    {
        position = index * 2;
        howMuchSway = 1f / (7f - position);
        if (howMuchSway < 0f)
        {
            howMuchSway = 0;
        }
    }
}