using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EScene : MonoBehaviour
{
    
    public void LIslandScen(){
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("IslandScene");
    }
}
