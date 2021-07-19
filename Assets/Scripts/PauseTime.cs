using UnityEngine;

public class PauseTime : MonoBehaviour
{
    //going well if there is no animation or movement while pause
    //otherwise handle ball speed and paddle movement on pause
    private void OnEnable()
    {
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
    }
}
