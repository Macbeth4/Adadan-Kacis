using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class karakterkodlari : MonoBehaviour
{
    [Header("HARAKET")]
    public FixedJoystick joystick;
    public float moveSpeed;
    public bool haraket;
    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 moveVelocity;
    [Header("AGACLAR")]
    public int agacsayisi;
    public TextMeshProUGUI agactext;
    public GameObject odunui;
    [Header("TASLAR")]
    public int tassayisi;
    public TextMeshProUGUI tastext;
    public GameObject tasui;

    [Header("KOMURLER")]
    public int komursayisi;
    public TextMeshProUGUI komurtext;
    public TextMeshProUGUI marketkomurtext;
    public GameObject komurui;

    [Header("IRONLAR")]
    public int ironsayisi;
    public TextMeshProUGUI irontext;
    public TextMeshProUGUI marketirontext;
    public GameObject ironui;

    Color ozelColor = new Color(0.2269631f, 0.4716981f, 0.1713243f, 1f);
    Color kirmizirenk = new Color(0.5283019f, 0.1021716f, 0.1021716f, 1f);

    public CinemachineVirtualCamera virtualCamera;
    public GameObject player;
    public GameObject siyahekran;
    public GameObject tp;

    [Header("KAYIK")]
    public GameObject yetersiz;
    public GameObject kayikk;
    public GameObject kayikbilgi;
    public GameObject uretildi;
    public GameObject yapilacak;
    public GameObject kayik;
    bool kayikolusturuldu = false;

    [Header("METAL")]
    public int metalsayisi;
    public GameObject metalui;
    public TextMeshProUGUI metaltext;
    public GameObject yetersizmetal;
    public GameObject metalbilgi;

    [Header("GEMİ")]
    public GameObject gemi;
    public TextMeshProUGUI metalmarkettext;
    public TextMeshProUGUI agacmarkettext;
    public TextMeshProUGUI tasmarkettext;
    public TextMeshProUGUI komurmarkettext;
    public TextMeshProUGUI ironmarkettext;
    public GameObject gemibilgi;
    public GameObject uretildii;
    public GameObject yetersizgemi;
    public GameObject yapilacakgemi;
    bool gemiolusturuldu;
    public GameObject gemipanel;
    public GameObject player2;
    public AudioSource ses;
    public AudioClip yetersizsesi;
    public AudioClip toplama;
    private void Start()
    {
        metalbilgi.SetActive(false);
        kayikk.SetActive(true);



        if (PlayerPrefs.HasKey("metalmik") && PlayerPrefs.GetInt("metalmik", metalsayisi) > 0)
        {
            metalui.SetActive(true);
            metalsayisi = PlayerPrefs.GetInt("metalmik");
            metaltext.text = metalsayisi.ToString();
        }
        else
        {
            metalui.SetActive(false);
            metaltext.text = "";
        }

        if (PlayerPrefs.HasKey("kayikuretildi"))
        {
            kayikbilgi.SetActive(false);
            uretildi.SetActive(true);
            yapilacak.SetActive(true);
            kayik.SetActive(true);
            kayikolusturuldu = true;
        }
        else
        {
            kayikbilgi.SetActive(true);
            uretildi.SetActive(false);
            yapilacak.SetActive(false);
            kayik.SetActive(false);
            kayikolusturuldu = false;
        }

        if (PlayerPrefs.HasKey("gemiuretildi"))
        {
            gemibilgi.SetActive(false);
            uretildii.SetActive(true);
            yapilacakgemi.SetActive(true);
            gemi.SetActive(true);
            gemiolusturuldu = true;
        }
        else
        {
            gemibilgi.SetActive(true);
            uretildii.SetActive(false);
            yapilacakgemi.SetActive(false);
            gemi.SetActive(false);
            gemiolusturuldu = false;
        }

        tasmarkettext.text = tassayisi.ToString(); agacmarkettext.text = agacsayisi.ToString();
        ironmarkettext.text = ironsayisi.ToString(); komurmarkettext.text = komursayisi.ToString(); metalmarkettext.text = metalsayisi.ToString();
        if (metalsayisi >= 3) { metalmarkettext.color = ozelColor; }
        else { metalmarkettext.color = kirmizirenk; }
        if (ironsayisi >= 5) { ironmarkettext.color = ozelColor; }
        else { ironmarkettext.color = kirmizirenk; }
        if (komursayisi >= 3) { komurmarkettext.color = ozelColor; }
        else { komurmarkettext.color = kirmizirenk; }
        if (tassayisi >= 10) { tasmarkettext.color = ozelColor; }
        else { tasmarkettext.color = kirmizirenk; }
        if (agacsayisi >= 15) { agacmarkettext.color = ozelColor; }
        else { agacmarkettext.color = kirmizirenk; }
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }


    private void Update()
    {


        if (haraket)
        {
            // Yatay ve dikey hareket
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            Vector2 moveDirection = new Vector2(horizontalInput, verticalInput);
            moveVelocity = moveDirection * moveSpeed;

        }
        else
        {

        }

        if (moveVelocity.magnitude > 0)
        {
            anim.SetBool("kosma", true);
        }
        else
        {
            anim.SetBool("kosma", false);
        }

    }

    private void FixedUpdate()
    {
        if (haraket)
        {
            rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);

        }
        else
        {
            moveVelocity.x = joystick.Horizontal;
            moveVelocity.y = joystick.Vertical;
            rb.MovePosition(rb.position + moveVelocity * moveSpeed * Time.fixedDeltaTime);


        }
    }

    public void uretmetal()
    {
        if (ironsayisi >= 2 && komursayisi >= 2)
        {
            ironsayisi -= 2; komursayisi -= 2;
            irontext.text = ironsayisi.ToString(); komurtext.text = komursayisi.ToString();
            marketkomurtext.text = komursayisi.ToString(); marketirontext.text = ironsayisi.ToString();
            komurmarkettext.text = komursayisi.ToString(); ironmarkettext.text = ironsayisi.ToString();
            if (komursayisi == 0) { komurui.SetActive(false); komurtext.text = ""; }
            if (ironsayisi == 0) { ironui.SetActive(false); irontext.text = ""; }
            if (ironsayisi >= 2) { marketirontext.color = ozelColor; }
            else { marketirontext.color = kirmizirenk; }
            if (komursayisi >= 2) { marketkomurtext.color = ozelColor; }
            else { marketkomurtext.color = kirmizirenk; }
            if (ironsayisi >= 5) { ironmarkettext.color = ozelColor; }
            else { ironmarkettext.color = kirmizirenk; }
            if (komursayisi >= 3) { komurmarkettext.color = ozelColor; }
            else { komurmarkettext.color = kirmizirenk; }
            metalsayisi++;
            metalui.SetActive(true);
            ses.PlayOneShot(toplama);
            metalmarkettext.text = metalsayisi.ToString();
            if (metalsayisi >= 3) { metalmarkettext.color = ozelColor; }
            else { metalmarkettext.color = kirmizirenk; }
            metaltext.text = metalsayisi.ToString();
            PlayerPrefs.SetInt("metalmik", metalsayisi);
            PlayerPrefs.Save();

        }
        else
        {
            if (yetersizmetal.activeSelf) { } else {ses.PlayOneShot(yetersizsesi); yetersizmetal.SetActive(true); Invoke("YetersizKapat1", 1.5f); }
            
        }
    }
    public void YetersizKapat1() { yetersizmetal.SetActive(false); }

    public void uretgemi()
    {
        if (tassayisi >= 10 && agacsayisi >= 15 && komursayisi >= 3 && ironsayisi >= 5 & metalsayisi >= 3)
        {
            tassayisi -= 10; agacsayisi -= 15; komursayisi -= 3; ironsayisi -= 5; metalsayisi -= 3;
            tastext.text = tassayisi.ToString(); agactext.text = agacsayisi.ToString();
            irontext.text = ironsayisi.ToString(); komurtext.text = komursayisi.ToString(); metaltext.text = metalsayisi.ToString();
            tasmarkettext.text = tassayisi.ToString(); agacmarkettext.text = agacsayisi.ToString();
            ironmarkettext.text = ironsayisi.ToString(); komurmarkettext.text = komursayisi.ToString(); metalmarkettext.text = metalsayisi.ToString();
            if (agacsayisi == 0) { odunui.SetActive(false); agactext.text = ""; }
            if (tassayisi == 0) { tasui.SetActive(false); tastext.text = ""; }
            if (komursayisi == 0) { komurui.SetActive(false); komurtext.text = ""; }
            if (ironsayisi == 0) { ironui.SetActive(false); irontext.text = ""; }
            if (metalsayisi == 0) { metalui.SetActive(false); metaltext.text = ""; }
            gemiolusturuldu = true;
            if (metalsayisi >= 3) { metalmarkettext.color = ozelColor; }
            else { metalmarkettext.color = kirmizirenk; }
            if (ironsayisi >= 5) { ironmarkettext.color = ozelColor; }
            else { ironmarkettext.color = kirmizirenk; }
            if (komursayisi >= 3) { komurmarkettext.color = ozelColor; }
            else { komurmarkettext.color = kirmizirenk; }
            if (tassayisi >= 10) { tasmarkettext.color = ozelColor; }
            else { tasmarkettext.color = kirmizirenk; }
            if (agacsayisi >= 15) { agacmarkettext.color = ozelColor; }
            else { agacmarkettext.color = kirmizirenk; }
            gemibilgi.SetActive(false);
            uretildii.SetActive(true);
            yapilacakgemi.SetActive(true);
            gemi.SetActive(true);
            PlayerPrefs.SetInt("gemiuretildi", 1);
            PlayerPrefs.Save();
            /*
            marketagactext.text = agacsayisi.ToString();
            markettastext.text = tassayisi.ToString();
            if (tassayisi >= 2) { markettastext.color = ozelColor; }
            else { markettastext.color = kirmizirenk; }
            if (agacsayisi >= 8) { marketagactext.color = ozelColor; }
            else { marketagactext.color = kirmizirenk; }
            */
            // ÜRET
        }
        else
        {
            
            if (yetersizgemi.activeSelf) { } else {ses.PlayOneShot(yetersizsesi); yetersizgemi.SetActive(true); Invoke("YetersizKapatgemi", 1.5f); }
        }
    }

    public void YetersizKapatgemi() { yetersizgemi.SetActive(false); }


    public void uret()
    {
        if (tassayisi >= 2 && agacsayisi >= 8)
        {
            tassayisi -= 2; agacsayisi -= 8;
            tastext.text = tassayisi.ToString(); agactext.text = agacsayisi.ToString();
            if (agacsayisi == 0) { odunui.SetActive(false); agactext.text = ""; }
            if (tassayisi == 0) { tasui.SetActive(false); tastext.text = ""; }
            tasmarkettext.text = tassayisi.ToString(); agacmarkettext.text = agacsayisi.ToString();
            if (tassayisi >= 10) { tasmarkettext.color = ozelColor; }
            else { tasmarkettext.color = kirmizirenk; }
            if (agacsayisi >= 15) { agacmarkettext.color = ozelColor; }
            else { agacmarkettext.color = kirmizirenk; }

            kayikolusturuldu = true;
            kayikbilgi.SetActive(false);
            uretildi.SetActive(true);
            yapilacak.SetActive(true);
            kayik.SetActive(true);
            PlayerPrefs.SetInt("kayikuretildi", 1);
            PlayerPrefs.Save();
            /*
            marketagactext.text = agacsayisi.ToString();
            markettastext.text = tassayisi.ToString();
            if (tassayisi >= 2) { markettastext.color = ozelColor; }
            else { markettastext.color = kirmizirenk; }
            if (agacsayisi >= 8) { marketagactext.color = ozelColor; }
            else { marketagactext.color = kirmizirenk; }
            */
            // ÜRET
        }
        else
        {
            
            if (yetersiz.activeSelf) { } else { ses.PlayOneShot(yetersizsesi); yetersiz.SetActive(true); Invoke("YetersizKapat", 1.5f); }
        }
    }

    public void YetersizKapat() { yetersiz.SetActive(false); }


    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "kiyi")
        {
            if (kayikolusturuldu)
            {
                StartCoroutine(kiyiyagit());
            }
        }

        if (other.gameObject.tag == "gemikiyi")
        {
            if (gemiolusturuldu)
            {
                joystick.gameObject.SetActive(false);
                StartCoroutine(gemikiyiyagit());

            }
        }
    }


    IEnumerator kiyiyagit()
    {
        virtualCamera.Follow = null;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        player.SetActive(true);
        kayik.GetComponent<Animator>().SetBool("kayik1", true);
        gameObject.transform.position = tp.transform.position;
        yield return new WaitForSeconds(3f);
        haraket = false;
        siyahekran.SetActive(true);
        player.SetActive(false);
        yield return new WaitForSeconds(2f);
        gemipanel.SetActive(true);
        kayikk.SetActive(false);
        metalbilgi.SetActive(true);
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        virtualCamera.Follow = gameObject.transform;

        Invoke("siyahekrankapan", 0.75f);
    }
    public void siyahekrankapan()
    {
        haraket = true;
        siyahekran.SetActive(false);
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }

    IEnumerator gemikiyiyagit()
    {
        virtualCamera.Follow = null;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        player2.SetActive(true);
        gemi.GetComponent<Animator>().SetBool("gemi1", true);
        yield return new WaitForSeconds(3f);
        GameObject.Find("market").GetComponent<market>().bitis();


        

        
    }

}
