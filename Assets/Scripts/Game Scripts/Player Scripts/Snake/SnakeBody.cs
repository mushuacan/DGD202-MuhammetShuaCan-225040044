using UnityEngine;

public class SnakeBody : MonoBehaviour
{
    public float swaySpeed = 2f;
    public int position = 1;
    public float howMuchSway = 1f;

    private float timer = 0f;

    public SnakeScale snakeScale;

    private void Start()
    {
        timer = position / 3f;
    }

    private void Update()
    {
        // Karakteri saða veya sola salla
        if (Time.timeScale != 0f)
        {
            timer += Time.deltaTime;
            transform.localPosition += (Vector3.right * Mathf.Sin(timer * 8) * swaySpeed * 0.003f * howMuchSway / position);
        }
        if(Mathf.Sin(timer) == 0f)
        {
            transform.localPosition = new Vector3(0, transform.position.y, transform.position.z);
        }
        if (snakeScale != null)
        {
            snakeScale.ArrangeScale(-1, position);
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