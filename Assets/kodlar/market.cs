using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class market : MonoBehaviour
{
    public Animator anim;
    public GameObject marketpaneli;
    public GameObject kesbutonlar;
    public GameObject kazbutonlar;
    public GameObject kesbutonlar1;
    public GameObject kazbutonlar1;

    [Header("CANVAS")]
    public GameObject ilksahne;
    public GameObject ikincisahne;
    public GameObject envanter;
    public GameObject marketekran;
    public GameObject joyistick;
    public GameObject anamenupanel;
    public GameObject butonmenu;
    public GameObject ayarlar;
    public GameObject bitispanel;
    public void anamenu()
    {
        anamenupanel.SetActive(true);
        joyistick.SetActive(false);
        butonmenu.SetActive(false);
        ilksahne.SetActive(false);
        ikincisahne.SetActive(false);
        envanter.SetActive(false);
        marketekran.SetActive(false);


    }

    public async void sifirla()
    {
        GameObject.Find("karakter").GetComponent<karakterkodlari>().agacsayisi = 0;
        GameObject.Find("karakter").GetComponent<karakterkodlari>().tassayisi = 0;
        GameObject.Find("karakter").GetComponent<karakterkodlari>().metalsayisi = 0;
        GameObject.Find("karakter").GetComponent<karakterkodlari>().komursayisi = 0;
        GameObject.Find("karakter").GetComponent<karakterkodlari>().ironsayisi = 0;
        PlayerPrefs.DeleteKey("komurmik"); PlayerPrefs.DeleteKey("ironmik");
        PlayerPrefs.DeleteKey("metalmik"); PlayerPrefs.DeleteKey("odunmik"); PlayerPrefs.DeleteKey("tasmik");
        PlayerPrefs.DeleteAll();
        await Task.Delay(400);
        SceneManager.LoadScene(0);
    }

    public void bitis()
    {
        bitispanel.SetActive(true);
        joyistick.SetActive(false);
        butonmenu.SetActive(false);
        ilksahne.SetActive(false);
        ikincisahne.SetActive(false);
        envanter.SetActive(false);
        marketekran.SetActive(false);


    }

    public void oyna()
    {
        anamenupanel.SetActive(false);
        joyistick.SetActive(true);
        ilksahne.SetActive(true);
        butonmenu.SetActive(true);
        ikincisahne.SetActive(true);
        envanter.SetActive(true);


    }
    public void ayarlarac()
    {
        ayarlar.SetActive(true);
        anamenupanel.SetActive(false);

    }
    public void ayarlarkapa()
    {
        ayarlar.SetActive(false);
        anamenupanel.SetActive(true);

    }

    public void OpenURL()
    {
        Application.OpenURL("https://www.instagram.com/besucbesdort/");
    }
    public void marketacil()
    {
        marketpaneli.SetActive(true);
        kesbutonlar.SetActive(false);
        kazbutonlar.SetActive(false);
        kesbutonlar1.SetActive(false);
        kazbutonlar1.SetActive(false);
        joyistick.SetActive(false);
    }

    public async void marketkapan()
    {
        anim.SetBool("acil", true);

        kesbutonlar.SetActive(true);
        kazbutonlar.SetActive(true);
        kesbutonlar1.SetActive(true);
        kazbutonlar1.SetActive(true);
        joyistick.SetActive(true);
        await Task.Delay(400);
        marketpaneli.SetActive(false);
    }

}
