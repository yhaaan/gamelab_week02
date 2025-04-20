using UnityEngine;

public class BackWithTime : MonoBehaviour
{
    //0 , 840 w
    //0, -200
    //0.7 , 0.8
    public GameObject Sun;
    public Gradient skyColorGradient;
    public Gradient SunColorGradient;
    private SpriteRenderer sunColor;
    public SpriteRenderer[] skys;
    public float t;

    private void Update()
    {
        t = EnvironmentManager.instance.wholeTime / 840f;
        sunColor = Sun.GetComponentInChildren<SpriteRenderer>();
        SumPosition();
        UpdateSkyColor();
    }


    private void SumPosition()
    {
        float angle = Mathf.Lerp(0, -200f, t);
        Sun.transform.localEulerAngles = new Vector3(0, 0, angle);
        sunColor.color = SunColorGradient.Evaluate(t);
    }

    private void UpdateSkyColor()
    {
        // Gradient를 평가하여 색상 결정
        Color newColor = skyColorGradient.Evaluate(t);
        foreach (var sprite in skys)
        {
            sprite.color = newColor;
        }
    }
}
