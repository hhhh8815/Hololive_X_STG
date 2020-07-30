using UnityEngine;

public class TestMap : MonoBehaviour
{
    [Header("移動速度")]
    public float speed = 0.01f;

    private void Update()
    {
        if (transform.position.z == -19)
        {

        }
        transform.position = transform.position + Vector3.forward * speed;
    }
}
