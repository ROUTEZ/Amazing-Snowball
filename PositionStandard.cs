using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionStandard : MonoBehaviour
{
    public Transform stage;

    private Transform transform;

    // Start is called before the first frame update
    void Start()
    {
        stage = stage.GetComponent<Transform>();
        transform = GetComponent<Transform>();

        transform.position = new Vector3(stage.localScale.x / 2 * 10, transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
