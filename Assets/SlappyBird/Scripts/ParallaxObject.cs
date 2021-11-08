using UnityEngine;

public class ParallaxObject : MonoBehaviour
{
    public float speed = 3f;
    public bool startMovement = false;

    //private float bounds = 0f;
    private float width = 0f;
    private BoxCollider2D boxCollider;

    void Start()
    {
        //bounds = Camera.main.orthographicSize * Screen.width / Screen.height;
        boxCollider = GetComponent<BoxCollider2D>();
        width = boxCollider.size.x;
    }

    protected virtual void Update()
    {
        Move();
    }

    void Move()
    {
        transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);

        if (transform.position.x < -width)//-bounds * 2f)
        {
            Reposition();
        }
    }

    protected virtual void Reposition()
    {
        Vector2 newPosition = new Vector2(width * 2f, 0);
        transform.position = (Vector2)transform.position + newPosition;
    }
}
