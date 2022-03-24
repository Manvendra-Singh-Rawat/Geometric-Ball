using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_new : MonoBehaviour
{
    [SerializeField]
    private float extention;
    [SerializeField]
    private GameObject _time;
    [SerializeField]
    private GameObject[] environment;
    [SerializeField]
    private GameObject[] obstacle;
    private float distance = -0.5f;
    [SerializeField]
    private float spawn_base = 50.07f;
    [SerializeField]
    private float speed = 4.0f;

    private float asdf = 1.0f;

    private Vector3 jump;
    private float jump_force = 2.05f;
    private float horizontal_input, imaginary_input;
    private bool isGrounded;
    Rigidbody rb;

    private UI_manager UImanager;


    void Start()
    {
        UImanager = GameObject.Find("Canvas").GetComponent<UI_manager>();

        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0, 3.5f, 0f);

    }

    void OnCollisionStay()
    {
        isGrounded = true;
    }

    void OnCollisionExit()
    {
        isGrounded = false;
    }

    void Update()
    {
        movement();

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isGrounded = false;
            rb.AddForce(jump * jump_force, ForceMode.Impulse);
        }

        spawner();
        spawn_obstacles();

        time_remain();
    }

    private void time_remain()
    {
        if(asdf <= Time.time)
        {
            asdf++;
            UImanager.time_change();
        }
    }

    private void spawner()
    {
        if (transform.position.z >= spawn_base)
        {
            Vector3 position = new Vector3(0, 0, transform.position.z + 98.23f);
            int random = Random.Range(1,2);
            Instantiate(environment[random], position, Quaternion.identity);
            Instantiate(obstacle[0], position, Quaternion.identity);
            spawn_base = spawn_base + 39.88f;
        }
    }

    private void movement()
    {
        if (isGrounded == true)
        {
            horizontal_input = Input.GetAxis("Horizontal");
            imaginary_input = Input.GetAxis("Vertical");
        }
        rb.AddForce(new Vector3(horizontal_input * speed * 8.0f * Time.deltaTime, 0.0f, imaginary_input * speed * Time.deltaTime * 6));
    }

    private void spawn_obstacles()
    {
        if (transform.position.z >= distance)
        {
            int random = Random.Range(1, 3);
            if (random == 1)
            {
                int random_number = Random.Range(1, 4);
                for(int i=1; i<=random_number; i++)
                {
                    int random_x = Random.Range(-5, 6);
                    Vector3 position = new Vector3(random_x, 2.0f, transform.position.z + 80.0f);
                    Instantiate(obstacle[random], position, Quaternion.identity);
                    
                }
                Vector3 position_x = new Vector3(0, 15.0f, transform.position.z + 80.0f);
                Instantiate(_time, position_x, Quaternion.identity);
            }
            else if (random == 2)
            {
                Vector3 position = new Vector3(0, 1.48f, transform.position.z + 80.0f);
                Instantiate(obstacle[random], position, obstacle[random].transform.rotation);
                Vector3 position_x = new Vector3(0, 2.72f, transform.position.z + 80.0f);
                Instantiate(_time, position_x, Quaternion.identity);
            }
            distance = transform.position.z + 60.0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "time_barricade")
        {
            UImanager.time_blessing();
            UImanager._score();
            Destroy(other.gameObject);
        }else if(other.tag == "player_ded")
        {
            UImanager.final_state();
            Destroy(this.gameObject);
        }
    }
}
