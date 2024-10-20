using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private AudioSource _switchWeaponSource;

    public WeaponSelector WeaponSelector;

    private void Awake()
    {
        Weapon[] weapons = gameObject.GetComponentsInChildren<Weapon>(true);
        WeaponSelector = new WeaponSelector(weapons);
    }

    private void Update()
    {
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");

        if (scrollWheel >= 0.1f)
        {
            _switchWeaponSource.Play();
            WeaponSelector.Next();
        }

        if (scrollWheel <= -0.1f)
        {
            _switchWeaponSource.Play();
            WeaponSelector.Preview();
        }

        if (Input.GetMouseButton(0))
        {
            WeaponSelector.Fire();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            WeaponSelector.Recharge();
        }
    }
}
