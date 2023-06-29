using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IJunior.TypedScenes;

public class GameLoad : MonoBehaviour
{
    public void LoadGame()
    {
        SC_Pixel_Art_Top_Down___Basic.Load();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
