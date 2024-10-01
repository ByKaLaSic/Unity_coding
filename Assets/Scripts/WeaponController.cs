using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private AudioSource _switchWeaponSource;

    private WeaponSelector _weaponSelector;

    private void Start()
    {
        Weapon[] weapons = gameObject.GetComponentsInChildren<Weapon>(true);
        _weaponSelector = new WeaponSelector(weapons);
    }

    private void Update()
    {
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");

        if (scrollWheel >= 0.1f)
        {
            _switchWeaponSource.Play();
            _weaponSelector.Next();
        }

        if (scrollWheel <= -0.1f)
        {
            _switchWeaponSource.Play();
            _weaponSelector.Preview();
        }

        if (Input.GetMouseButton(0))
        {
            _weaponSelector.Fire();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            _weaponSelector.Recharge();
        }
    }
}
