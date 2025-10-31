using UnityEngine;

public class Character : MonoBehaviour
{
    Weapon weapon;

    private void Start()
    {
        weapon = new Weapon();

        weapon.WeaponChange(new Sword());
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Q))
        {
            weapon.WeaponChange(new Sword());
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            weapon.WeaponChange(new Bow());
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            weapon.Attack();
        }
    }
}
public class Weapon
{
    IWeapon weapon;
    public void WeaponChange(IWeapon newWeapon)
    {
        Debug.Log("무기 바뀜");
        weapon = newWeapon;
    }
    public void Attack()
    {
        weapon.Excute();
    }
    
}
public class Sword : IWeapon
{
    public void Excute()
    {
        Debug.Log("검 휘두르기");
    }
}

public class Bow : IWeapon
{
    public void Excute()
    {
        Debug.Log("활 쏘기");
    }
}
public interface IWeapon
{
    void Excute();

}
