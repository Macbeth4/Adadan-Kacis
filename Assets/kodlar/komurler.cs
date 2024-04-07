using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class komurler : MonoBehaviour
{
    public GameObject joyistick;
    public Button kazmabuton;
    public GameObject kazma;
    public Image cooldown;
    public float duration = 5;
    private float currenttime = 0;
    public GameObject komurui;
    public GameObject efekt;
    public GameObject komurcollider;
    public GameObject komursimge;
    public TextMeshProUGUI komursayitext;
    private int komursayilari;
    public AudioSource ses;
    public AudioClip kirmasesi;
    public AudioClip toplama;
    Color ozelColor = new Color(0.2269631f, 0.4716981f, 0.1713243f, 1f);
    Color kirmizirenk = new Color(0.5283019f, 0.1021716f, 0.1021716f, 1f);

    private string bos = "";
    private void Start()
    {
        currenttime = duration;
        komursayilari = GameObject.Find("karakter").GetComponent<karakterkodlari>().komursayisi;
        komursayitext.text = GameObject.Find("karakter").GetComponent<karakterkodlari>().komursayisi.ToString();
        if (komursayilari >= 2) { komursayitext.color = ozelColor; } else { komursayitext.color = kirmizirenk; }

        if (PlayerPrefs.HasKey("komurmik") && PlayerPrefs.GetInt("komurmik", GameObject.Find("karakter").GetComponent<karakterkodlari>().komursayisi) > 0)
        {
            komurui.SetActive(true);
            GameObject.Find("karakter").GetComponent<karakterkodlari>().komursayisi = PlayerPrefs.GetInt("komurmik");
            GameObject.Find("karakter").GetComponent<karakterkodlari>().komurtext.text = GameObject.Find("karakter").GetComponent<karakterkodlari>().komursayisi.ToString();
        }
        else
        {
            komurui.SetActive(false);
            GameObject.Find("karakter").GetComponent<karakterkodlari>().komurtext.text = bos;
        }
    }
    private void Update()
    {
        PlayerPrefs.SetInt("komurmik", GameObject.Find("karakter").GetComponent<karakterkodlari>().komursayisi);
        PlayerPrefs.Save();
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            kazmabuton.gameObject.SetActive(true);
            kazma.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            kazmabuton.gameObject.SetActive(false);
            kazma.SetActive(false);
        }
    }

    public void kazma1()
    {
        kazmabuton.interactable = false;
        joyistick.gameObject.SetActive(false);
        GameObject.Find("karakter").GetComponent<karakterkodlari>().haraket = false;
        ses.PlayOneShot(kirmasesi);
        kazma.GetComponent<Animator>().SetBool("kes", true);
        efekt.SetActive(true);
        StartCoroutine(gerisayimsureci());
        StartCoroutine(kesmesureci2());
    }

    IEnumerator gerisayimsureci()
    {
        cooldown.gameObject.SetActive(true);

        float startTime = Time.time;
        float endTime = startTime + duration;

        while (Time.time < endTime)
        {
            float elapsedTime = Time.time - startTime;
            cooldown.fillAmount = Mathf.InverseLerp(0, duration, elapsedTime);
            yield return null;
        }

        cooldown.gameObject.SetActive(false);
    }

    IEnumerator kesmesureci2()
    {
        yield return new WaitForSeconds(5f);
        joyistick.gameObject.SetActive(true);
        efekt.SetActive(false);
        GameObject.Find("karakter").GetComponent<karakterkodlari>().haraket = true;
        GameObject.Find("karakter").GetComponent<karakterkodlari>().komursayisi++;
        ses.PlayOneShot(toplama);
        komursayilari = GameObject.Find("karakter").GetComponent<karakterkodlari>().komursayisi;
        GameObject.Find("karakter").GetComponent<karakterkodlari>().komurmarkettext.text = GameObject.Find("karakter").GetComponent<karakterkodlari>().komursayisi.ToString();
        if (GameObject.Find("karakter").GetComponent<karakterkodlari>().komursayisi >= 3) { GameObject.Find("karakter").GetComponent<karakterkodlari>().komurmarkettext.color = ozelColor; }
        else { GameObject.Find("karakter").GetComponent<karakterkodlari>().komurmarkettext.color = kirmizirenk; }
        kazma.GetComponent<Animator>().SetBool("kes", false);
        GameObject.Find("karakter").GetComponent<karakterkodlari>().komurtext.text = GameObject.Find("karakter").GetComponent<karakterkodlari>().komursayisi.ToString();
        komursayitext.text = GameObject.Find("karakter").GetComponent<karakterkodlari>().komursayisi.ToString();
        if (komursayilari >= 2) { komursayitext.color = ozelColor; } else { komursayitext.color = kirmizirenk; }
        komurui.SetActive(true);
        kazmabuton.interactable = true;
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        komursimge.SetActive(false);
        komurcollider.GetComponent<EdgeCollider2D>().enabled = false;

        yield return new WaitForSeconds(25f);
        this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        komursimge.SetActive(true);
        komurcollider.GetComponent<EdgeCollider2D>().enabled = true;

    }
}

