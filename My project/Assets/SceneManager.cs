using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SceneManager : MonoBehaviour
{
    public GameObject Spawner;
    public GameObject SceneLight;
    public GameObject[] AmbientLights;
    public GameObject background;
    public GameObject waves;
    public GameObject Seaweed;
    public GameObject SoundManag;

    void Update()
    {
        
        Color BG;
        Color FG;
        Color GL;
        Color AL;

        switch(Spawner.gameObject.GetComponent<Spawner>().Depth)
        {
             case 0:
            ColorUtility.TryParseHtmlString("#0D3D41", out BG);
            ColorUtility.TryParseHtmlString("#37C0AE", out FG);
            ColorUtility.TryParseHtmlString("#0D3D41", out GL);
            ColorUtility.TryParseHtmlString("#00D0FF", out AL);
            changeScene(.23f,2.65f,BG,FG,GL,AL,true);
            break;

            case 1:
            ColorUtility.TryParseHtmlString("#0D3D41", out BG);
            ColorUtility.TryParseHtmlString("#37C0AE", out FG);
            ColorUtility.TryParseHtmlString("#0D3D41", out GL);
            ColorUtility.TryParseHtmlString("00D0FF", out AL);
            changeScene(.23f,1.65f,BG,FG,GL,AL,false);
            break;


            case 2:
            ColorUtility.TryParseHtmlString("#0D3D41", out BG);
            ColorUtility.TryParseHtmlString("#37C0AE", out FG);
            ColorUtility.TryParseHtmlString("#0D3D41", out GL);
            ColorUtility.TryParseHtmlString("00D0FF", out AL);
            changeScene(.13f,.65f,BG,FG,GL,AL,false);
            break;

        }
    }
        

    void changeScene(float globalIntensity, float ambientIntensity, Color backgroundColor, Color waveColor, Color globalColor, Color ambientColor, bool isSeaweed){
        SceneLight.GetComponent<UnityEngine.Rendering.Universal.Light2D>().intensity = globalIntensity;
        AmbientLights[0].GetComponent<UnityEngine.Rendering.Universal.Light2D>().intensity = ambientIntensity;
        SceneLight.GetComponent<UnityEngine.Rendering.Universal.Light2D>().color = globalColor;
        AmbientLights[0].GetComponent<UnityEngine.Rendering.Universal.Light2D>().color = ambientColor;

        background.GetComponent<SpriteRenderer>().color = backgroundColor;
        waves.GetComponent<SpriteRenderer>().color = waveColor;

        Seaweed.gameObject.GetComponent<SpriteRenderer>().enabled = isSeaweed;
    }

}
