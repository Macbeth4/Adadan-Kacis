using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ironlar : MonoBehaviour
{
    public GameObject joyistick;
    public Button kazmabuton;
    public GameObject kazma;
    public Image cooldown;
    public float duration = 5;
    private float currenttime = 0;
    public GameObject ironui;
    public GameObject efekt;
    public GameObject ironcollider;
    public GameObject ironsimge;
    public TextMeshProUGUI ironsayitext;
    private int ironsayilari;
    public AudioSource ses;
    public AudioClip kirmasesi;
    public AudioClip toplama;
    Color ozelColor = new Color(0.2269631f, 0.4716981f, 0.1713243f, 1f);
    Color kirmizirenk = new Color(0.5283019f, 0.1021716f, 0.1021716f, 1f);

    private string bos = "";
    private void Start()
    {
        currenttime = duration;
        ironsayilari = GameObject.Find("karakter").GetComponent<karakterkodlari>().ironsayisi;
        ironsayitext.text = GameObject.Find("karakter").GetComponent<karakterkodlari>().ironsayisi.ToString();
        if (ironsayilari >= 2) { ironsayitext.color = ozelColor; } else { ironsayitext.color = kirmizirenk; }

        if (PlayerPrefs.HasKey("ironmik") && PlayerPrefs.GetInt("ironmik", GameObject.Find("karakter").GetComponent<karakterkodlari>().ironsayisi) > 0)
        {
            ironui.SetActive(true);
            GameObject.Find("karakter").GetComponent<karakterkodlari>().ironsayisi = PlayerPrefs.GetInt("ironmik");
            GameObject.Find("karakter").GetComponent<karakterkodlari>().irontext.text = GameObject.Find("karakter").GetComponent<karakterkodlari>().ironsayisi.ToString();
        }
        else
        {
            ironui.SetActive(false);
            GameObject.Find("karakter").GetComponent<karakterkodlari>().irontext.text = bos;
        }
    }
    private void Update()
    {
        PlayerPrefs.SetInt("ironmik", GameObject.Find("karakter").GetComponent<karakterkodlari>().ironsayisi);
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
        GameObject.Find("karakter").GetComponent<karakterkodlari>().ironsayisi++;
        ses.PlayOneShot(toplama);
        ironsayilari = GameObject.Find("karakter").GetComponent<karakterkodlari>().ironsayisi;
        GameObject.Find("karakter").GetComponent<karakterkodlari>().ironmarkettext.text = GameObject.Find("karakter").GetComponent<karakterkodlari>().ironsayisi.ToString();
        if (GameObject.Find("karakter").GetComponent<karakterkodlari>().ironsayisi >= 5) { GameObject.Find("karakter").GetComponent<karakterkodlari>().ironmarkettext.color = ozelColor; }
        else { GameObject.Find("karakter").GetComponent<karakterkodlari>().ironmarkettext.color = kirmizirenk; }
        kazma.GetComponent<Animator>().SetBool("kes", false);
        GameObject.Find("karakter").GetComponent<karakterkodlari>().irontext.text = GameObject.Find("karakter").GetComponent<karakterkodlari>().ironsayisi.ToString();
        ironsayitext.text = GameObject.Find("karakter").GetComponent<karakterkodlari>().ironsayisi.ToString();
        if (ironsayilari >= 2) { ironsayitext.color = ozelColor; } else { ironsayitext.color = kirmizirenk; }
        ironui.SetActive(true);
        kazmabuton.interactable = true;
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        ironsimge.SetActive(false);
        ironcollider.GetComponent<EdgeCollider2D>().enabled = false;

        yield return new WaitForSeconds(20f);
        this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        ironsimge.SetActive(true);
        ironcollider.GetComponent<EdgeCollider2D>().enabled = true;

    }
}
