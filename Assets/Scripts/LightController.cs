using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    [SerializeField]
    private Light l;

    [SerializeField]
    private bool flicker_enabled;

    [SerializeField]
    private bool is_arrhythmic = true;

    [SerializeField]
    private float flicker_min = 0f;

    [SerializeField]
    private float flicker_max = 10f;

    [SerializeField]
    private bool is_on = true;

    [SerializeField]
    private List<float> flicker_pattern;
    private int idx;

    private MeshRenderer light_mesh;
    private List<Material> light_mats = new();

    public void on_complete(float v)
    {
        l.intensity = v;
    }

    private void OnValidate()
    {
        l = GetComponentInChildren<Light>();
        light_mesh = GetComponent<MeshRenderer>();
    }

    private void Awake()
    {
        light_mesh.GetMaterials(light_mats);
    }

    private void Start()
    {
        if (flicker_enabled && is_on)
        {
            StartCoroutine(is_arrhythmic ? arrhythmic_flicker() : sinusoidal_flicker());
        }
        else if (!is_on)
        {
            on_complete(flicker_min);
        }
        else if (is_on)
        {
            on_complete(flicker_max);
        }
    }

    private void Update()
    {
        light_mesh.SetMaterials(is_on ? light_mats : new());
    }

    public IEnumerator arrhythmic_flicker()
    {
        if (flicker_pattern == null || flicker_pattern.Count < 1)
        {
            on_complete(flicker_max);
            yield break;
        }

        if (idx < 0 || idx >= flicker_pattern.Count)
        {
            idx = 0;
        }

        float intensity = is_on ? flicker_max : flicker_min;
        on_complete(intensity);

        float duration = Mathf.Max(0.0001f, flicker_pattern[idx]);
        idx = (idx + 1) % flicker_pattern.Count;

        yield return new WaitForSeconds(duration);
        is_on = !is_on;

        yield return StartCoroutine(arrhythmic_flicker());
    }

    [SerializeField]
    private float flicker_speed = 2f;

    [SerializeField, Range(0f, 1f)]
    private float flicker_randomness = 0.05f;

    private float t;

    public IEnumerator sinusoidal_flicker()
    {
        if (flicker_speed <= 0f)
        {
            on_complete(flicker_max);
            yield break;
        }

        float half_period = 0.5f / flicker_speed;
        float jitter = UnityEngine.Random.Range(-flicker_randomness, flicker_randomness);
        float duration = Mathf.Max(0.0001f, half_period * (1f + jitter));
        float intensity = is_on ? flicker_max : flicker_min;

        on_complete(intensity);
        yield return new WaitForSeconds(duration);
        is_on = !is_on;
        yield return StartCoroutine(sinusoidal_flicker());
    }
}
