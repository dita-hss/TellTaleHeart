using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(SoundEmitter))]
public class Throwable : MonoBehaviour
{

    [SerializeField] private float fadeOutTime = 5.0f; 
    private SoundEmitter _soundEmitter;
    private Rigidbody _rigidbody; 
    // Start is called before the first frame update
    void Awake()
    {
        _soundEmitter = GetComponent<SoundEmitter>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        fadeOutTime -= Time.deltaTime;

        if (fadeOutTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Launch(Vector3 forceDirectionAndMagnitude)
    {
        _rigidbody.AddForce(forceDirectionAndMagnitude, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        string tag = collision.gameObject.tag;
        if (!tag.Equals("Player") && !tag.Equals("Enemy"))
        {
            _soundEmitter.EmitSound();
        }
    }
}
