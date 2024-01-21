using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoodBar : MonoBehaviour
{

    [SerializeField] private Slider slider;
    private float maxValue;
    private float minValue;
    private float currentValue;
    [SerializeField] private float durationInMinute = 0.1f;

    private Guest guest;
    [SerializeField] private Camera camera;
    [SerializeField] private Transform Target;
    [SerializeField] private float offSetOnY;
    private bool activeMood;


    public void UpdateMoodBar()
    {
        float oldValue = currentValue;
        float reduceValue = 1 / durationInMinute / 60 * Time.deltaTime;
        currentValue -= reduceValue;
        currentValue = Mathf.Max(currentValue, minValue);
        slider.value = Mathf.Lerp(oldValue, currentValue, reduceValue);
        
    }


    // Start is called before the first frame update
    void Start()
    {
        maxValue = 1;
        minValue = 0;
        currentValue = maxValue;
        activeMood = false;
    }

    public void SetGuest(Guest _guest)
    {
        this.guest = _guest;
    }
    // Update is called once per frame
    void Update()
    {
        if (guest == null) return;
        transform.rotation = camera.transform.rotation;
        
        transform.position = Target.position + new Vector3(0,offSetOnY,0);


        activeMood = CheckCondition();
        if (activeMood)
        {
            //Debug.Log("update mood bar");
            UpdateMoodBar();
        }

        
    }

    private bool CheckCondition()
    {
        //check if guest arrive the target Point.
        if (guest.transform.position.x == guest.targetPoint.position.x
         && this.transform.position.z == guest.targetPoint.position.z)
        {
            if (!guest.taskComplete)
            {
                if (currentValue == 0)
                {
                    //Debug.Log("guest is angry to leave");
                    guest.Leave();
                    return false;
                }
                return true;
            }
        }

        

        return false;
    }
}
