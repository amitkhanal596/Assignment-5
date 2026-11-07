using UnityEngine;

public class LightsaberStyle : MonoBehaviour
{
    public Renderer blade;
    public Renderer[] sideBlades;
    public Renderer housing;
    public Light bladeLight;
    public GameObject kyloVolume;

    public Color[] colors =
    {
        new Color(0f, 0.15f, 1f),
        new Color(0.05f, 1f, 0.08f),
        new Color(1f, 0.02f, 0.02f),
        new Color(0.55f, 0.05f, 1f),
        new Color(1f, 0.7f, 0f),
        new Color(0f, 0.9f, 1f),
    };

    private Material material;
    private bool kylo;
    private Color currentColor;
    private Color targetColor;
    private float instability;

    void Start()
    {
        material = blade.material;
        foreach (Renderer r in sideBlades)
        {
            r.sharedMaterial = material;
        }

        currentColor = colors[0];
        SetKylo(false);
    }

    void Update()
    {
        for (int i = 0; i < colors.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                targetColor = colors[i];
            }
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            SetKylo(!kylo);
        }

        currentColor = Color.Lerp(currentColor, targetColor, 10f * Time.deltaTime);
        instability = Mathf.MoveTowards(instability, kylo ? 1f : 0f, 2f * Time.deltaTime);

        material.SetColor("_BladeColor", currentColor);
        material.SetFloat("_Instability", instability);
        bladeLight.color = currentColor;
    }

    void SetKylo(bool on)
    {
        kylo = on;
        targetColor = on ? colors[2] : colors[0];

        housing.enabled = on;
        foreach (Renderer r in sideBlades)
        {
            r.enabled = on;
        }
        kyloVolume.SetActive(on);
    }
}
