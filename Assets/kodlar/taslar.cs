using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class taslar : MonoBehaviour
{
    public GameObject joyistick;
    public Button kazmabuton;
    public GameObject kazma;
    public Image cooldown;
    public float duration;
    private float currenttime = 0;
    public GameObject tasui;
    public GameObject efekt;
    public GameObject tascollider;
    public TextMeshProUGUI tassayitext;
    private int tassayilari;
    public AudioSource ses;
    public AudioClip tassesi;
    public AudioClip toplama;
    Color ozelColor = new Color(0.2269631f, 0.4716981f, 0.1713243f, 1f);
    Color kirmizirenk = new Color(0.5283019f,0.1021716f,0.1021716f,1f);


    private string bos = "";
    private void Start()
    {
        currenttime = duration;
        tassayilari = GameObject.Find("karakter").GetComponent<karakterkodlari>().tassayisi;
        tassayitext.text = GameObject.Find("karakter").GetComponent<karakterkodlari>().tassayisi.ToString();
        if (tassayilari >= 2){tassayitext.color = ozelColor;}else{tassayitext.color = kirmizirenk;}

        if (PlayerPrefs.HasKey("tasmik") && PlayerPrefs.GetInt("tasmik", GameObject.Find("karakter").GetComponent<karakterkodlari>().tassayisi) > 0 )
        {
            tasui.SetActive(true);
            GameObject.Find("karakter").GetComponent<karakterkodlari>().tassayisi = PlayerPrefs.GetInt("tasmik");
            GameObject.Find("karakter").GetComponent<karakterkodlari>().tastext.text = GameObject.Find("karakter").GetComponent<karakterkodlari>().tassayisi.ToString();
        }
        else
        {
            tasui.SetActive(false);
            GameObject.Find("karakter").GetComponent<karakterkodlari>().tastext.text = bos;
        }
    }
    private void Update()
    {
        PlayerPrefs.SetInt("tasmik", GameObject.Find("karakter").GetComponent<karakterkodlari>().tassayisi);
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
        ses.PlayOneShot(tassesi);
        GameObject.Find("karakter").GetComponent<karakterkodlari>().haraket = false;
        kazma.GetComponent<Animator>().SetBool("kes",true);
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
        yield return new WaitForSeconds(4f);
        joyistick.gameObject.SetActive(true);
        efekt.SetActive(false);
        GameObject.Find("karakter").GetComponent<karakterkodlari>().haraket = true;
        GameObject.Find("karakter").GetComponent<karakterkodlari>().tassayisi++;
        ses.PlayOneShot(toplama);
        tassayilari = GameObject.Find("karakter").GetComponent<karakterkodlari>().tassayisi;
        GameObject.Find("karakter").GetComponent<karakterkodlari>().tasmarkettext.text = GameObject.Find("karakter").GetComponent<karakterkodlari>().tassayisi.ToString();
        if (GameObject.Find("karakter").GetComponent<karakterkodlari>().tassayisi >= 10) { GameObject.Find("karakter").GetComponent<karakterkodlari>().tasmarkettext.color = ozelColor; }
        else { GameObject.Find("karakter").GetComponent<karakterkodlari>().tasmarkettext.color = kirmizirenk; }
        kazma.GetComponent<Animator>().SetBool("kes",false);
        GameObject.Find("karakter").GetComponent<karakterkodlari>().tastext.text = GameObject.Find("karakter").GetComponent<karakterkodlari>().tassayisi.ToString();
        tassayitext.text = GameObject.Find("karakter").GetComponent<karakterkodlari>().tassayisi.ToString();
        if (tassayilari >= 2){tassayitext.color = ozelColor;}else{tassayitext.color = kirmizirenk;}
        tasui.SetActive(true);
        kazmabuton.interactable = true;
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        tascollider.GetComponent<EdgeCollider2D>().enabled = false;
        
        yield return new WaitForSeconds(30f);
        this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        tascollider.GetComponent<EdgeCollider2D>().enabled = true;
        
    }
}
