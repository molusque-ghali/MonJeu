using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour

{
    public Transform PlayerTransform;
    Vector3 range;
    private void Awake()
    {
        range = transform.position - PlayerTransform.position;
    }
    private void FixedUpdate()
    {
        transform.position = new Vector3(range.x + PlayerTransform.position.x, transform.position.y, range.z + PlayerTransform.position.z);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
