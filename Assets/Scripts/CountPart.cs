using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
//Robert Chaney
public class CountPart : MonoBehaviour
{
    public string value;
    public Canvas camvas;
    public TextMeshProUGUI percentText;
    public int percent;
    public ParticleSystem ps;
    public int num=0;
    public GameObject percentTextOj;
    void Start()
    {   percentText=percentTextOj.GetComponent<TextMeshProUGUI>();
        ps = GetComponent<ParticleSystem>();
        percent = 0;
        num = 0;
    }
    void Update()
    {
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[ps.particleCount];
        num = ps.GetParticles(particles);
    

        percent =(300 -( (60 * num) / 300))/3;
      value = percent.ToString()+ "%";
        percentText.text = value;   
    }

 
}