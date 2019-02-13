using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersonController : MonoBehaviour {
    
    public new Transform camera;
    public Transform line;
    public Transform highLine;
    public float velocity;
    public int coins;
    public GameObject lineRender;
    public GameObject activeLineRender;
    [Space]
    public ParticleSystem particle;
    public ParticleSystem particleUltimate;

    [HideInInspector]
    public List<float> velocityCheck;

    private Rigidbody2D rg;

    private bool isGrounded;

    public bool isDead = false;

    public bool shakeOnce = false;

    public bool fullSpeed = false;

    public Animator anim;

    public bool vibrate;

    float lengthLine = 1f, ts = 0.1f;

    void Start () {
        rg = GetComponent<Rigidbody2D>();
    }
	
	void Update () {
        if (!isDead)
            MovePlayer();

        CheckIsDead();
        CameraMove();
    }

    void CameraMove ()
    {
        // Camera Size
        if (transform.position.y > camera.transform.position.y + 5)
        {
            if (camera.GetComponentInChildren<Camera>().orthographicSize >= 50)
                camera.GetComponentInChildren<Camera>().orthographicSize = 50;
            else
                camera.GetComponentInChildren<Camera>().orthographicSize += 0.15f;
        }
        else if ((transform.position.y < camera.transform.position.y + 5))
        {
            if (camera.GetComponentInChildren<Camera>().orthographicSize <= 15)
                camera.GetComponentInChildren<Camera>().orthographicSize = 15;
            else
                camera.GetComponentInChildren<Camera>().orthographicSize -= 0.15f;
        }

        //After we move, adjust the camera to follow the player
        camera.transform.position = new Vector3(transform.position.x + 12f, 10f, camera.transform.position.z);
        line.transform.position = new Vector3(transform.position.x + 12f, 15f, line.transform.position.z);
        highLine.transform.position = new Vector3(transform.position.x + 12f, 35f, highLine.transform.position.z);
    }

    // Move player
    void MovePlayer ()
    {
        velocity = rg.velocity.magnitude;

        if (isGrounded)
        {
            // Touch Screen
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if ((touch.phase == TouchPhase.Stationary) && (velocity < 50))
                {
                    rg.AddForce(Vector2.right, ForceMode2D.Impulse);
                    activeLineRender.SetActive(true);
                }
                else
                    activeLineRender.SetActive(false);
            }
            // Touch Screen

            // Keyboard
            if ((Input.GetKey(KeyCode.Space)) && (velocity < 50))
            {
                rg.AddForce(Vector2.right, ForceMode2D.Impulse);
                activeLineRender.SetActive(true);
            }
            else
                activeLineRender.SetActive(false);
            // Keyboard
        }
        else // If the player is not on the ground
        {
            // Touch Screen
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if ((touch.phase == TouchPhase.Stationary) && (velocity < 100))
                {
                    rg.AddForce(Vector2.right + Vector2.down * 2, ForceMode2D.Impulse);
                    activeLineRender.SetActive(true);
                }
                else
                    activeLineRender.SetActive(false);
            }
            // Touch Screen

            // Keyboard
            if ((Input.GetKey(KeyCode.Space)) && (velocity < 100))
            {
                rg.AddForce(Vector2.right + Vector2.down * 2, ForceMode2D.Impulse);
                activeLineRender.SetActive(true);
            }
            else
                activeLineRender.SetActive(false);
            // Keyboard
        }

        if (velocity > 80)
            velocity = 80;

        if ((velocity >= 70) && (!isGrounded))
            fullSpeed = true;

        if ((velocity < 40) && (!isGrounded))
            fullSpeed = false;

        if (fullSpeed)
        {
            if (lengthLine <= 50f)
                lengthLine++;

            if (!particleUltimate.isPlaying)
                particleUltimate.Play();


            lineRender.GetComponent<TrailRenderer>().endColor = Color.red;
            lineRender.GetComponent<TrailRenderer>().startColor = Color.Lerp(Color.yellow, Color.red, .2f);

            //Debug.Log(lineRender.GetComponent<TrailRenderer>().time);
        }
        else
        {
            particleUltimate.Stop();


            lineRender.GetComponent<TrailRenderer>().endColor = Color.white;
            lineRender.GetComponent<TrailRenderer>().startColor = Color.white;

            if (lengthLine >= 1f)
                lengthLine = .1f;

            if (lengthLine < 1f)
                lengthLine = .1f;
        }
        lineRender.GetComponent<TrailRenderer>().time = lengthLine;


    }

    void CheckIsDead ()
    {
        if (!isGrounded)
        {
            if (velocityCheck.Count >= 15)
                for (int i = 0; i < velocityCheck.Count; i++)
                    velocityCheck.RemoveAt(i);
            else
                velocityCheck.Add(velocity);
        }

        // Check is dead
        if (velocity > 30)
            if (GetAvg(velocityCheck) - 25 > velocity)
            {
                particle.Play();
                isDead = true;
                if (vibrate)
                    Handheld.Vibrate();
            }

        if (velocity > 60)
            if (GetAvg(velocityCheck) - 25 > velocity)
            {
                particle.Play();
                isDead = true;
                if (vibrate)
                    Handheld.Vibrate();
            }

        if (velocity > 60)
            if (GetAvg(velocityCheck) - 20 > velocity)
            {
                particle.Play();
                isDead = true;
                if (vibrate)
                    Handheld.Vibrate();
            }

        if (isDead && !shakeOnce)
        {
            anim.SetBool("Shake", true);

        }

        if (isDead)
        {
            rg.mass = 10;
            rg.drag = 1;
            rg.gravityScale = 5;

            fullSpeed = false;

            particleUltimate.Stop();

            lineRender.GetComponent<TrailRenderer>().time = 0.1f;

            if (IsAnimationPlaying("ShakeCam"))
                anim.SetBool("Shake", false);

            shakeOnce = true;
            
        }
    }

    public void ReloadLevel ()
    {
        if (isDead)
            SceneManager.LoadScene(0);
    }

    // Get avg value from array
    float GetAvg(List<float> array)
    {
        float avg = 0;
        for (int i = 0; i < array.Count; i++)
            avg += array[i];

        avg = avg / array.Count;

        return avg;
    }

    private void OnCollisionEnter2D (Collision2D other)
    {
        if (other.transform.tag == "Ground")
        {
            isGrounded = true;
            // Particle System is play
            if ((other.relativeVelocity.magnitude >= 30) && (velocity < 60))
                particle.Play();
        }
    }

    private void OnCollisionExit2D (Collision2D other)
    {
        if (other.transform.tag == "Ground")
            isGrounded = false;
    }

    private void OnTriggerEnter2D (Collider2D other)
    {
        if (other.transform.tag == "Coin")
            coins++;
    }

    public bool IsAnimationPlaying (string animationName)
    {
        // берем информацию о состоянии
        var animatorStateInfo = anim.GetCurrentAnimatorStateInfo(0);
        // смотрим, есть ли в нем имя какой-то анимации, то возвращаем true
        if (animatorStateInfo.IsName(animationName))
            return true;

        return false;
    }
}
