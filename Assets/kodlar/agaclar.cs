using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class agaclar : MonoBehaviour
{
    public GameObject ustagac;
    public GameObject agaccollider;
    public GameObject joyistick;
    public Button kesmebutton;
    public GameObject balta;
    public Image cooldown;
    public float duration = 3;
    private float currenttime = 0;
    public GameObject odunui;
    public GameObject efekt;

    public TextMeshProUGUI agacsayitext;
    public AudioSource ses;
    public AudioClip odunsesi;
    public AudioClip toplama;
    private int agacsayilari;
    Color ozelColor = new Color(0.2269631f, 0.4716981f, 0.1713243f, 1f);
    Color kirmizirenk = new Color(0.5283019f,0.1021716f,0.1021716f,1f);
    private string bos = "";
    private void Start()
    {
        currenttime = duration;
        agacsayilari = GameObject.Find("karakter").GetComponent<karakterkodlari>().agacsayisi;
        agacsayitext.text = GameObject.Find("karakter").GetComponent<karakterkodlari>().agacsayisi.ToString();
        if (agacsayilari >= 8){agacsayitext.color = ozelColor;}else{agacsayitext.color = kirmizirenk;}


        if (PlayerPrefs.HasKey("odunmik") && PlayerPrefs.GetInt("odunmik",GameObject.Find("karakter").GetComponent<karakterkodlari>().agacsayisi) > 0)
        {
            odunui.SetActive(true);
            GameObject.Find("karakter").GetComponent<karakterkodlari>().agacsayisi = PlayerPrefs.GetInt("odunmik");
            GameObject.Find("karakter").GetComponent<karakterkodlari>().agactext.text = GameObject.Find("karakter").GetComponent<karakterkodlari>().agacsayisi.ToString();
        }
        else
        {
            odunui.SetActive(false);
            GameObject.Find("karakter").GetComponent<karakterkodlari>().agactext.text = bos;
        }
    }
    private void Update()
    {
        PlayerPrefs.SetInt("odunmik", GameObject.Find("karakter").GetComponent<karakterkodlari>().agacsayisi);
        PlayerPrefs.Save();

    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            kesmebutton.gameObject.SetActive(true);
            balta.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            kesmebutton.gameObject.SetActive(false);
            balta.SetActive(false);
        }
    }

    public void kesme()
    {
        kesmebutton.interactable = false;
        joyistick.gameObject.SetActive(false);
        efekt.SetActive(true);
        balta.GetComponent<Animator>().SetBool("kes", true);
        ses.PlayOneShot(odunsesi);
        GameObject.Find("karakter").GetComponent<karakterkodlari>().haraket = false;
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

        yield return new WaitForSeconds(1.5f);
        ustagac.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        efekt.SetActive(false);
        joyistick.gameObject.SetActive(true);
        GameObject.Find("karakter").GetComponent<karakterkodlari>().haraket = true;
        GameObject.Find("karakter").GetComponent<karakterkodlari>().agacsayisi++;
        ses.PlayOneShot(toplama);
        agacsayilari = GameObject.Find("karakter").GetComponent<karakterkodlari>().agacsayisi;
        GameObject.Find("karakter").GetComponent<karakterkodlari>().agacmarkettext.text = GameObject.Find("karakter").GetComponent<karakterkodlari>().agacsayisi.ToString();
        if (GameObject.Find("karakter").GetComponent<karakterkodlari>().agacsayisi >= 15) { GameObject.Find("karakter").GetComponent<karakterkodlari>().agacmarkettext.color = ozelColor; }
        else { GameObject.Find("karakter").GetComponent<karakterkodlari>().agacmarkettext.color = kirmizirenk; }
        balta.GetComponent<Animator>().SetBool("kes", false);
        GameObject.Find("karakter").GetComponent<karakterkodlari>().agactext.text = GameObject.Find("karakter").GetComponent<karakterkodlari>().agacsayisi.ToString();
        agacsayitext.text = GameObject.Find("karakter").GetComponent<karakterkodlari>().agacsayisi.ToString();
        if (agacsayilari >= 8){agacsayitext.color = ozelColor;}else{agacsayitext.color = kirmizirenk;}
        
        odunui.SetActive(true);
        kesmebutton.interactable = true;
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        agaccollider.GetComponent<BoxCollider2D>().enabled = false;
        
        yield return new WaitForSeconds(45f);
        ustagac.SetActive(true);
        this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        agaccollider.GetComponent<BoxCollider2D>().enabled = true;
        

    }
}
