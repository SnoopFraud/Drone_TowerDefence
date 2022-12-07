using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void pindahscene(string NamaScene)
    {
        SceneManager.LoadScene(NamaScene);
    }

    public void TutupAplikasi()
    {
        Application.Quit();
    }

}
