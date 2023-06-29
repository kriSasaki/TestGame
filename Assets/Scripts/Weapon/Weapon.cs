using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public abstract class Weapon : MonoBehaviour
{
    protected const string Shot = "Shot";

    [SerializeField] private string _label;
    [SerializeField] private int _price;
    [SerializeField] private bool _isBuyed = false;

    [SerializeField] protected Transform _shootPoint;
    [SerializeField] protected float FireRate;
    [SerializeField] protected float Scatter;
    [SerializeField] protected SimpleBullet Bullet;

    private Sprite _icon;

    private SpriteRenderer _sprite;

    private GetValueZ _getValueZ;

    private Vector2 _ordinateAxis;

    private Transform _thisTransform;


    private Side _side;

    private float _maxDelay = 100;
    protected float Delay = 100;
    protected float Spread;

    protected AudioSource _audio;

    protected Vector3 ScatterAngle;

    protected Animator Animator;


    protected static System.Random _random;

    public string Label => _label;
    public int Price => _price;
    public Sprite Icon => _icon;
    public bool IsBuyed => _isBuyed;

    private void Awake()
    {
        Animator = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
        _icon = _sprite.sprite;      
    }

    private void Start()
    {
        _audio= GetComponent<AudioSource>();
        _getValueZ = GetComponent<GetValueZ>();
        _isBuyed = false;
        _ordinateAxis = Vector2.up;
        _thisTransform = transform;
        _random = new System.Random();
    }

    public virtual void Shoot()
    {
        if (Delay >= FireRate)
        {
            _audio.Play();
            Animator.SetTrigger(Shot);
            ScatterAngle = _shootPoint.eulerAngles;
            ScatterAngle.z += Random.Range(-Scatter, Scatter);

            Instantiate(Bullet, _shootPoint.position, Quaternion.Euler(ScatterAngle));
            Delay = 0;
        }
    }

    public void Buy()
    {
        _isBuyed = true;
    }

    public void SetPositionZ(float z)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, z);
    }

    private void Update()
    {
        if (Delay < _maxDelay)
        {
            Delay += Time.deltaTime;
        }

        float z = _getValueZ.GetValue(out Vector2 abscissaAxis, out Vector2 viewVector) * (int)GetSide(abscissaAxis, viewVector);
        _thisTransform.rotation = Quaternion.Euler(0, 0, z);
    }

    private Side GetSide(Vector2 abscissaAxis, Vector2 viewVector)
    {
        _side = Side.Right;
        _sprite.flipY = false;

        if (viewVector.y <= abscissaAxis.y)
        {
            _side = Side.Left;
        }

        if (viewVector.x <= _ordinateAxis.x)
        {
            _sprite.flipY = true;
        }

        return _side;
    }
  
    private enum Side
    {
        Left = -1,
        Right = 1
    }
}
