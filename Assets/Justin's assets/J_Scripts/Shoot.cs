using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [System.NonSerialized] public static Shoot current;
    [System.NonSerialized] public string gunAmmoUIStr;

    public GameObject bullet;
    public GameObject bullet1;
    private float offset = 0.5f;
    private float speed = 15f;

    private enum weaponType {pistol,shotgun,machineGun,mine};

    // Starting ammo for shotgun, machineGun, and mine respectively
    private int[] ammo = new int[]{15,50,10};
    weaponType currentWeapon;

    

    private bool fired = false;

    void Awake()
    {
        if (current == null)
            current = this;
        else
            Destroy(this.gameObject);
    }

    void Start()
    {
        currentWeapon = weaponType.pistol;
    }

    // Update is called once per frame
    void Update()
    {
        gunAmmoUIStr = returnUpdates();
        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.LeftShift))
            Pew();
        if (Input.GetKey(KeyCode.Mouse0) && currentWeapon == weaponType.machineGun && fired == false && ammo[1] != 0)
            StartCoroutine(PewMachineGun());
        if (Input.GetKeyDown(KeyCode.E))
            currentWeapon = (currentWeapon != weaponType.mine) ? currentWeapon + 1 : weaponType.pistol;
        if (Input.GetKeyDown(KeyCode.Q))
            currentWeapon = (currentWeapon != weaponType.pistol) ? currentWeapon - 1 : weaponType.mine;
    }

    void Pew()
    {
        switch (currentWeapon)
        {
            case weaponType.pistol:
                PewPistol();
                break;
            case weaponType.shotgun:
                if(ammo[0] != 0)
                    PewShotgun();
                break;
            case weaponType.machineGun:
                if (ammo[1] != 0)
                    StartCoroutine(PewMachineGun());
                break;
            case weaponType.mine:
                if (ammo[2] != 0)
                    DropMine();
                break;
            default:
                Debug.Log("ERROR: Invalid Weapon Type");
                return;
        }
        FindObjectOfType<D_AudioManager>().Play("Shoot");
    }

    // functions for firing each type of weapon
    void PewPistol()
    {
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = worldMousePos - this.transform.position;
        direction.Normalize();

        GameObject clone = Instantiate(bullet, this.transform.position + (Vector3)(direction * 0.5f), Quaternion.identity);
        float rotation_z = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        clone.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotation_z + offset);
        clone.GetComponent<Rigidbody2D>().velocity = direction * speed;
    }

    void PewShotgun()
    {
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = worldMousePos - this.transform.position;
        Vector2 direction2 = new Vector2(direction.x - 0.5f, direction.y - 1f);
        Vector2 direction3 = new Vector2(direction.x + 0.5f, direction.y + 1f);
        direction.Normalize();
        direction2.Normalize();
        direction3.Normalize();

        GameObject clone1 = Instantiate(bullet, this.transform.position + (Vector3)(direction * 0.5f), Quaternion.identity);
        GameObject clone2 = Instantiate(bullet, this.transform.position + (Vector3)(direction * 0.5f), Quaternion.identity);
        GameObject clone3 = Instantiate(bullet, this.transform.position + (Vector3)(direction * 0.5f), Quaternion.identity);

        float rotation_z = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float rotation_z2 = Mathf.Atan2(direction2.y, direction2.x) * Mathf.Rad2Deg;
        float rotation_z3 = Mathf.Atan2(direction3.y, direction3.x) * Mathf.Rad2Deg;

        clone1.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotation_z + offset);
        clone1.GetComponent<Rigidbody2D>().velocity = direction * speed;

        clone2.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotation_z2 + offset);
        clone2.GetComponent<Rigidbody2D>().velocity = direction2 * speed;

        clone3.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotation_z3 + offset);
        clone3.GetComponent<Rigidbody2D>().velocity = direction3 * speed;

        ammo[0]--;
    }

    IEnumerator PewMachineGun()
    {
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = worldMousePos - this.transform.position;
        direction.Normalize();

        GameObject clone = Instantiate(bullet, this.transform.position + (Vector3)(direction * 0.5f), Quaternion.identity);
        float rotation_z = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        clone.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotation_z + offset);
        clone.GetComponent<Rigidbody2D>().velocity = direction * speed;
        ammo[1]--;

        fired = true;
        yield return new WaitForSecondsRealtime(0.1f);
        fired = false;
    }

    void DropMine()
    {
        Vector2 minePosition = this.transform.position;
        minePosition.x -= .2f;
        minePosition.y -= .2f;
        GameObject clone = Instantiate(bullet1, minePosition, Quaternion.identity);
        ammo[2]--;
    }

    string returnUpdates()
    {
        string retVal = "Gun: ";
        switch(currentWeapon)
        {
            case weaponType.pistol:
                retVal += "Pistol\nAmmo: Inf";
                break;
            case weaponType.shotgun:
                retVal += "Shotgun\nAmmo: ";
                retVal += ammo[0].ToString();
                break;
            case weaponType.machineGun:
                retVal += "Machine Gun\nAmmo: ";
                retVal += ammo[1].ToString();
                break;
            case weaponType.mine:
                retVal += "Mine\nAmmo: ";
                retVal += ammo[2].ToString();
                break;
            default:
                Debug.Log("ERROR: Invalid Weapon Type");
                break;
        }
        return retVal;
    }

}
