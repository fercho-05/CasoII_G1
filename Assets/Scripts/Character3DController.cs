using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character3DController : MonoBehaviour
{
    [SerializeField]
    float attackRate;

    [SerializeField]
    float attackRange;


    StarterAssetsInputs _input;
    Animator _animator;

    int _swordSlashHash;

    bool _swordSlashState;

    float _attackTime;



    //[SerializeField]
    //Transform attackPoint; //Utilizado para el dibujo

    private void Awake()
    {
        _input = GetComponent<StarterAssetsInputs>();
        _animator = GetComponent<Animator>();

        _swordSlashHash = Animator.StringToHash("SwordSlash");
    }

    private void Update()
    {
        if (_attackTime > 0.0F)
        {
            _attackTime -= Time.deltaTime;
            _attackTime = Mathf.Clamp(_attackTime, 0.0F, attackRange);
        }


        if (_swordSlashState != _input.swordSlash)
        {
            _swordSlashState = _input.swordSlash;

            if (_swordSlashState && _attackTime == 0.0F)
            {
                _animator.SetTrigger(_swordSlashHash);
                _attackTime = (attackRange / attackRate);
            }
        }

        //Acá se usa el ShootWeapon, debe usar el mismo attackTime
    }



    //private void OnDrawGizmos()   Sirve para dibujar en el unity y poder ver mejor algunas cosas
    //{
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawSphere(attackPoint.position, 0.50F);
    //}

}
