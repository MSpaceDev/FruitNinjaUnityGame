using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour {

    public float minVelocity = 0.1f;
    Vector3 lastMousePos;

    private Rigidbody2D rb;
    private Collider2D col;

	// Use this for initialization
	void Awake () {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
	}

    // Update is called once per frame
    void Update () {
        col.enabled = IsMouseMoving();

        MoveBladeToMouse();
	}

    private void MoveBladeToMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10;

        rb.position = Camera.main.ScreenToWorldPoint(mousePos);
    }

    private bool IsMouseMoving()
    {
        Vector3 curMousePos = transform.position;
        float travelled = (curMousePos - lastMousePos).magnitude;
        lastMousePos = curMousePos;

        if (travelled > 0.1f)
            return true;

        return false;
    }
}
