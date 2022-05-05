using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator animator_;
    [SerializeField]
    private float speed_;
    void Start()
    {
        animator_ = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        var moveVec = transform.forward;
        moveVec.Normalize();
       
        transform.transform.position += (moveVec * speed_);
    }
}
