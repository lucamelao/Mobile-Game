using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GliderSelection : MonoBehaviour
{

    public Image shop;
    [SerializeField] private TMP_Text _price;
    Treasury treasury;

    public GameObject[] gliders;
    public int selecetedGlider = 0;

    void Start(){
        treasury = Treasury.GetInstance();
    }

    public void NextGlider(){
        gliders[selecetedGlider].SetActive(false);
        selecetedGlider = (selecetedGlider + 1) % gliders.Length;
        gliders[selecetedGlider].SetActive(true);
    }

    public void PreviousGlider(){
        gliders[selecetedGlider].SetActive(false);
        selecetedGlider--;
        if(selecetedGlider < 0){
            selecetedGlider = gliders.Length - 1;
        }
        gliders[selecetedGlider].SetActive(true);
    }

    public void OpenShop(){
        shop.gameObject.SetActive(true);
        _price.text = gliders[selecetedGlider].GetComponent<GliderType>().price.ToString();
    }

    public void Buy(){
        if(treasury.Spend(gliders[selecetedGlider].GetComponent<GliderType>().price)){
            gliders[selecetedGlider].GetComponent<GliderType>().unlocked = true;
            shop.gameObject.SetActive(false);
        }
    }


    public void StartGame(){
        if(gliders[selecetedGlider].GetComponent<GliderType>().unlocked){
            PlayerPrefs.SetInt("selectedGlider", selecetedGlider);
            SceneManager.LoadScene("SampleScene");
        }
        else{
            OpenShop();
        }
    }
}
