using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PC1 : MonoBehaviour
{
    public bool jumpB;
    public Rigidbody2D body;
    public bool climbing, SID;
    public Animator anime;
    public float horizontal,vertical,  speed, jump, minHP = 0, maxHP = 100, HP, Z = 15f;
    public Slider slider;

    public Gradient gradient;
    public Image fill;

    public GameObject axe;
    public GameObject handBone;

    private bool Adoration;
    //public float scale = 0.6f;

    private bool addAxe;
    private bool hasAxe = false;

    private AudioSource audioSource;
    public AudioClip audioClip;
    private bool canPlaySound = true;
    public float timer;

    public GameObject[] tp;
    bool canTp = true;
    private void Start()
    {
        HP = maxHP;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {

        TimbiAdoration();
        BulochkaRun();
        SvetochekJump();
        PositivSidDown();
        TimbiDance();
        PicaClimbing();
        //if (Input.GetKeyDown(KeyCode.T)&& addAxe && axe.transform.parent == null && !hasAxe)
        //{
        //    Debug.Log("Подбираю");
        //    axe.transform.SetParent(handBone.transform);
        //    axe.transform.position = handBone.transform.position;
        //    hasAxe = true;
        //}
        //if (Input.GetKeyDown(KeyCode.T) && axe.transform.parent != null && hasAxe)
        //{
        //    Debug.Log("Выкидываю");
        //    axe.transform.SetParent(null);
        //    hasAxe = false;
        //}

        if (Input.GetKeyDown(KeyCode.T))
        {
            if (axe.transform.position != handBone.transform.position && addAxe)
            {
                Debug.Log("Беру");
                axe.transform.SetParent(handBone.transform);
                axe.transform.position = handBone.transform.position;
                addAxe = false;
            }
            else
            {
                Debug.Log("Выкидываю");
                axe.transform.SetParent(null);
                addAxe = false;
            }
        }

    }
    void TimbiAdoration()
    {
        anime.SetBool("adoration", Input.GetKey(KeyCode.E));
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    anime.SetBool("adoration", true);
        //    //ChangeHP(maxHP);
        //}
        //else if (Input.GetKeyUp(KeyCode.E))
        //{
        //    anime.SetBool("adoration", false);
        //}
    }
    void TimbiDance()
    {
        anime.SetBool("dance", Input.GetKey(KeyCode.Q));
        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    anime.SetBool("dance", true);

        //}
        //else if (Input.GetKeyUp(KeyCode.Q))
        //{
        //    anime.SetBool("dance", false);
        //}
    }
    void PositivSidDown()
    {
        anime.SetBool("sidDown", Input.GetKey(KeyCode.Tab));
        //if (Input.GetKeyDown(KeyCode.Tab) && SID)
        //{
        //    anime.SetBool("sidDown", true);

        //}
        //else if (Input.GetKeyUp(KeyCode.Tab))
        //{
        //    anime.SetBool("sidDown", false);
        //}
    }
    void PicaClimbing()
    {
        if (Input.GetKeyDown(KeyCode.E) && climbing)
        {
            vertical = Input.GetAxis("Vertical");
            transform.Translate(Vector2.up * vertical * 50 * Time.deltaTime);
        }
       
    }
    void SvetochekJump()
    {
        jump = Input.GetAxis("Vertical");
        bool space = Input.GetButtonDown("Jump");
        if ((space || jump > 0)  &&  !anime.GetBool("isJump"))
        {

            jumpB = true;
            anime.SetBool("isJump", true);
        }
        if (!jumpB)
        {
           
           
            return;
        }
        body.velocity = Vector2.zero;
        Vector2 poverJump = new Vector2(0, Z);
        body.AddForce( poverJump, ForceMode2D.Impulse);
        jumpB = false;
    }
    void BulochkaRun()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        anime.SetBool("isRun", false);

        if (horizontal > 0 || horizontal < 0)
        {
            timer -= timer > 0 ? Time.deltaTime : 0;
            if (canPlaySound)
            {
                audioSource.PlayOneShot(audioClip);
                canPlaySound = false;
            }

            if (timer < 0)
            {
                canPlaySound = true;
                timer = 0.4f;
            }
            anime.SetBool("isRun", true);
            transform.localScale = new Vector2(horizontal, 1f);
        }
        else
        {
            audioSource.Stop();
            timer = 0.4f;
        }
       
        transform.Translate(Vector2.right * horizontal * speed * Time.deltaTime);
        speed = Input.GetKey(KeyCode.LeftShift) ? speed = 10 : speed = 5;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        SID = collision.gameObject.CompareTag("sid down") ? SID = true : SID;
        climbing = collision.gameObject.CompareTag("climbing") ? climbing = true : climbing ;
        Adoration = collision.gameObject.CompareTag("adoration") ? Adoration = true : Adoration = false;
        addAxe = collision.gameObject.CompareTag("Axe");
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        SID = collision.gameObject.CompareTag("sid down") ? SID = false : SID;
        climbing = collision.gameObject.CompareTag("climbing") ? climbing = false : climbing;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            anime.SetBool("isJump", false);
        }
        if (canTp)
        {
            string[] namesCastles = { "TP2", "TP1" };
            for (int i = 0; i < tp.Length; i++)
            {
                if (collision.gameObject.name == namesCastles[i])
                {
                    Debug.Log("Столкнулся с телепортом "+i);
                    StartCoroutine(Teleportik(tp[i].transform.position));
                    break;
                }
            }
            //if (collision.gameObject.name == "TP1")
            //{
            //    Debug.Log("Столкнулся с телепортом 1");
            //    StartCoroutine(Teleportik(tp[1].transform.position));
            //}
            //if (collision.gameObject.name == "TP2")
            //{
            //    Debug.Log("Столкнулся с телепортом 2");
            //    StartCoroutine(Teleportik(tp[0].transform.position));
            //}
        }

    }
    IEnumerator Teleportik(Vector2 otherTp)
    {
        transform.position = otherTp + new Vector2(2,0); //позиция перса = позиция другой тпшки + немножко сдвигаем его вправо,
                                                      //чтоб он справа от входа в замок появлялся, а не застревал в текстурах
        canTp = false; //чтоб она не тплась 100500 раз на одном месте
        yield return new WaitForSeconds(0.1f); //спустя ничтожное кол-во времени
        canTp = true; //снова разрешаем ей тпаться
    }
    public void ChangeHP(float changeHP)
    {
        HP += changeHP;
        HP = HP > 100 ? 100 : HP;
        //Debug.Log(HP);
        slider.value = HP;

        if (HP < 0)
        {
            SceneManager.LoadScene("SampleScene");
        }
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
