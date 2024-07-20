using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontroler : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;
    public float forwardSpeed;
    private int desiredLane = 1; //0:left 1:middle 2:right
    public float laneDistance; //distance between two lanes
    public float jumpForce; 
    public float gravity;
    public Animator animator;
    private bool isSliding = false;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!playermanger.isGameStarted)
        {
            return;
        }

        animator.SetBool("isGameStarted", true);

        controller.Move(direction * Time.deltaTime);
        direction.z = forwardSpeed;
        
        animator.SetBool("isGrounded", controller.isGrounded);

        if (Input.GetKeyDown(KeyCode.DownArrow) && !isSliding)
        {
            StartCoroutine(slide());
        }

        //checks which lane we should be

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            desiredLane++;
            if (desiredLane == 3)
            {
                desiredLane = 2;
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            desiredLane--;
            if (desiredLane == -1)
            {
                desiredLane = 0;
            }
        }

        //calculate where we should be in the future

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        if(desiredLane == 0)
        {
            targetPosition += Vector3.left * laneDistance;
        }else if (desiredLane == 2)
        {
            targetPosition += Vector3.right * laneDistance; 
        }
        transform.position = Vector3.Lerp(transform.position,targetPosition,80*Time.deltaTime);

        if (controller.isGrounded) //checks if the player on the ground
        {
            direction.y = -1;
            //jump if we press up key
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                direction.y = jumpForce;
            }
        }
        else //if we are not on the ground we apply gravity
        {
            direction.y += gravity * Time.deltaTime;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.transform.tag == "obstacle")
        {
            playermanger.gameOver = true;
            FindObjectOfType<aduiomanger>().playSound("game over");
        }
    }
    private IEnumerator slide()
    {
        isSliding = true;
        animator.SetBool("isSliding", true);
        controller.center = new Vector3(0, -0.5f, 0);
        controller.height = 1;
        yield return new WaitForSeconds(1.3f);
        controller.center = new Vector3(0, 0, 0);
        controller.height = 2;
        animator.SetBool("isSliding", false);
        isSliding = false;
    }

}
