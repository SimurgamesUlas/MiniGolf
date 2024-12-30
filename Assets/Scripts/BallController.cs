using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float forcePower = 10f;
    public float maxDrag = 5f;

    Rigidbody2D rb;
    LineRenderer lr;

    Vector3 dragStartPos;

    Touch touch;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount >0){
            touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began){
                dragStart();
            }
            if(touch.phase == TouchPhase.Moved){
                Dragging();
            }
            if(touch.phase == TouchPhase.Ended){
                dragRelease();
            }
        }
    }
    private void OnTriggerStay2D(Collider2D col){
        if(col.gameObject.CompareTag("hole")){
            if(rb.velocity.magnitude <2f){
                gameObject.SetActive(false);
            }
        }
    }
    void dragStart(){
        dragStartPos = Camera.main.ScreenToWorldPoint(touch.position);
        dragStartPos.z = 0;
        lr.positionCount = 1;
        lr.SetPosition(0,dragStartPos);
    }

    void Dragging(){
        Vector3 dragginpos = Camera.main.ScreenToWorldPoint(touch.position);
        dragginpos.z = 0;
        lr.positionCount = 2;
        lr.SetPosition(1,dragginpos);   
    }
    void dragRelease(){
        lr.positionCount = 0;
        Vector3 dragreleasePos = Camera.main.ScreenToWorldPoint(touch.position);
        dragreleasePos.z = 0;
        Vector3 force = dragStartPos - dragreleasePos;
        rb.AddForce(force * forcePower);
    }
}
