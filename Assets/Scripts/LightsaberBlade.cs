using UnityEngine;

public class LightsaberBlade : MonoBehaviour
{
    public GameObject blade;

    public AudioSource audioSource;
    public AudioClip onSound;
    public AudioClip offSound;

    public float extendSpeed = 0.1f;
    private bool weaponActive;
    private const float SCALE_MIN = 0f;
    private float scaleMax;
    private float extendDelta;
    private float scaleCurrent;
    private float localScaleX;
    private float localScaleZ;

    void Start()
    {
        localScaleX = transform.localScale.x;
        localScaleZ = transform.localScale.z;
        scaleMax = transform.localScale.y;
        scaleCurrent = scaleMax;
        extendDelta = scaleMax / extendSpeed;
        weaponActive = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            extendDelta = weaponActive ? -Mathf.Abs(extendDelta) : Mathf.Abs(extendDelta);

            if (audioSource != null)
            {
                audioSource.clip = weaponActive ? offSound : onSound;
                audioSource.Play();
            }
        }

        scaleCurrent += extendDelta * Time.deltaTime;
        scaleCurrent = Mathf.Clamp(scaleCurrent, SCALE_MIN, scaleMax);
        transform.localScale = new Vector3(localScaleX, scaleCurrent, localScaleZ);
        weaponActive = scaleCurrent > 0;

        if (weaponActive && !blade.activeSelf)
        {
            blade.SetActive(true);
        }
        else if (!weaponActive && blade.activeSelf)
        {
            blade.SetActive(false);
        }
    }
}
