using UnityEngine;

public class MoveShapeMenu : MonoBehaviour
{
    public Vector3 direction;
    public float maxdistance;
    public float rotationspeed;
    public float speed;
    float currentRot;
    float time = 0f;
    void Awake()
    {
        rotationspeed *= (Random.value + 0.5f);
        speed *= (Random.value + 0.5f);
        transform.localScale *= (Random.value + 0.5f);
        GetComponent<SpriteRenderer>().color = Random.ColorHSV();
    }
    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        transform.position += direction * speed * Time.deltaTime;
        currentRot += rotationspeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0, 0, currentRot);
        if(transform.position.magnitude > maxdistance && time > 5f)
        {
            Destroy(gameObject);
        }
    }
}
