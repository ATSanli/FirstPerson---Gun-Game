using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireScripts : MonoBehaviour
{
    public float mermi,sarjor,toplammermi,menzil,hasar1,hasar2,siradakiates,ateszamani,sayi,zaman,maxZaman;
    public bool ates,reload;
    public Text mermiYazi;

    AudioSource audio;
    public AudioClip atessesi;
    public ParticleSystem MuzzleFlash;


    RaycastHit hit;
    void Start()
    {
         zaman= maxZaman;
         audio = GetComponent<AudioSource>();
        MuzzleFlash = GetComponentInChildren<ParticleSystem>();
    }

    void Update()
    {
        mermiYazi.text = "" + mermi + "/" + toplammermi;


        if (Input.GetMouseButton(0) && mermi > 0 && Time.time>siradakiates && !reload)
        {
            ates = true;
            siradakiates = Time.time + ateszamani;
            audio.PlayOneShot(atessesi);
            MuzzleFlash.Play();
            mermi--; 
        }
        if (Input.GetMouseButtonUp(0))
        {
            MuzzleFlash.Stop();

        }

        if (Input.GetKeyDown (KeyCode.R) && mermi !=30 && !reload)  
        {
            reload = true;
        }
        if (reload)
        {
            sayi = sarjor - mermi;
            zaman -= Time.deltaTime;
            if (zaman <= 0)
            {               
                reload = false;
                zaman = maxZaman;

                if (sayi > toplammermi)
                {
                    mermi += toplammermi;
                    toplammermi = 0;
                }
                if (sayi < toplammermi)
                {
                    mermi += sayi;
                    toplammermi -= sayi;
                }
            }
            
        }
    }
    void FixedUpdate()
    {
        if (ates)
        {
            ates = false;

            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, menzil))
            {
                if (hit.transform.tag == "dusman")
                {
                    Debug.Log("Düþmana Hasar Aldý...");
                    Destroy(hit.transform.gameObject);

                }
            }
        }
    }
}
