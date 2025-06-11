using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSelected : MonoBehaviour
{

    public GameObject homeFirstSelected, menuFirstSelected, sliderFirstSelected;
    // Start is called before the first frame update
    void Start()
    {
        
    }

  public void HomeScreen()
    {
        EventSystem.current.SetSelectedGameObject(null);

        EventSystem.current.SetSelectedGameObject(homeFirstSelected);
    }

 public void MenuScreen()
    {
        EventSystem.current.SetSelectedGameObject(null);

        EventSystem.current.SetSelectedGameObject (menuFirstSelected);

    }


public void SliderScreen()
    {
        EventSystem.current.SetSelectedGameObject(null) ;

        EventSystem.current.SetSelectedGameObject(sliderFirstSelected);
    }
}
