using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallFollower : MonoBehaviour
{

    public Transform snowball = null;


    // Start is called before the first frame update
    void Start()
    {
        snowball = snowball.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(snowball.position.x, snowball.position.y - snowball.localScale.y / 2, snowball.position.z);

        transform.localScale = new Vector3(snowball.localScale.x, transform.localScale.y, transform.localScale.z);
    }
}
