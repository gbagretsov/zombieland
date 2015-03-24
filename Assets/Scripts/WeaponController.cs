using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour {

    // Параметры стрельбы
    public int maxAmmo = 300; // Максимальное количество патронов
    public bool fullAmmoAtStart;
    public float shootDelay; // Задержка между выстрелами
    private float curDelay;
    private int curAmmo; // Текущее количество патронов
    public float shootDistance = 30f;
    public GUIText ammoGUI;
    public LineRenderer laserSight; // Лазерный прицел
    private bool laserSightOn = false;

    // Поворот пушки
    public Camera camera;
    private MouseInputController mouse;

    public GameObject turret;
    public GameObject fire;

    // Звук стрельбы
    public AudioClip shootSound;
    private AudioSource shootSoundSource;

    private Ray shootDirection;
    RaycastHit shootTarget;

    // Брызги крови
    public GameObject bloodParticle;

    void Start () 
    {        
        SetNewAmmoAmount(fullAmmoAtStart ? maxAmmo : 0);
        curDelay = shootDelay;
        fire.SetActive(false);
        mouse = camera.GetComponent<MouseInputController>();
        laserSight.enabled = false;

        shootSoundSource = gameObject.AddComponent<AudioSource>();
        shootSoundSource.loop = true;
        shootSoundSource.clip = shootSound;
        shootSoundSource.volume = 1;
	}
	
	void Update () 
    {
        RotateTurret();
        shootDirection = new Ray(fire.transform.position, fire.transform.forward);        
        if (laserSightOn)
            DrawLaserRay();
        Shoot();           
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "AmmoPickup" && curAmmo < maxAmmo)
        {
            Destroy(other.gameObject);
            SetNewAmmoAmount(maxAmmo);
        }
        else if (other.gameObject.tag == "LaserSightPickup" && !laserSightOn)
        {
            Destroy(other.gameObject);
            laserSightOn = true;
        }
    }

    private void SetNewAmmoAmount(int ammo)
    {
        curAmmo = ammo;
        ammoGUI.color = curAmmo < maxAmmo / 3 ? Color.red : Color.black;
        ammoGUI.text = "Ammo: " + curAmmo;
    }

    private void Shoot()
    {
        // Учитываем задержку
        if (curDelay > 0)
        {
            curDelay -= Time.deltaTime;
            if (fire.active)
                fire.SetActive(false);
        }

        // Пока нажата ЛКМ и есть патроны
        else
        {
            if (shootSoundSource.isPlaying)
                shootSoundSource.Pause();

            if (Input.GetMouseButton(0) && curAmmo != 0)
            {
                // Делаем выстрел                
                if (Physics.Raycast(shootDirection, out shootTarget, shootDistance))
                {
                    // Обработка попадания ("столкновения" луча)
                    if (shootTarget.collider.tag == "Enemy")
                    {
                        // Уменьшаем здоровье врага
                        shootTarget.collider.transform.root.GetComponent<EnemyController>().enemyHP--;

                        // Активируем брызги крови
                        Object b = Instantiate(bloodParticle, shootTarget.transform.position + new Vector3(0, 1f, 0), Quaternion.identity);
                        Destroy(b, 2);
                    }
                }

                // Проигрываем звук
                if (!shootSoundSource.isPlaying)
                    shootSoundSource.Play();
                
                // Вспышка
                fire.SetActive(true);

                SetNewAmmoAmount(curAmmo - 1);
                curDelay = shootDelay;
            }
        }
    }

    private void RotateTurret()
    {
        turret.transform.Rotate(0, mouse.MouseXInput * mouse.mouseXSensitivity, 0);
    }

    // Отрисовка лазерного луча
    private void DrawLaserRay()
    {
        laserSight.enabled = true;
        laserSight.SetPosition(0, shootDirection.origin);
        laserSight.SetPosition(1, shootDirection.origin + shootDirection.direction * shootDistance);
    }

}
