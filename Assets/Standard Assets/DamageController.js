var sound : SoundController;
sound = transform.root.GetComponent(SoundController);

private var car : Car;
car = transform.GetComponent(Car);

var healthGUI : GUIText;
var maxHealthPoints : int; 
private var hp : int;

function Start()
{
    hp = maxHealthPoints;
    healthGUI.text = "Health: " + hp.ToString();
}

function OnCollisionEnter(collInfo : Collision)
{
	if(enabled && collInfo.contacts.Length > 0)
	{
	    var volumeFactor : float = Mathf.Clamp01(collInfo.relativeVelocity.magnitude * 0.08);
	    volumeFactor *= Mathf.Clamp01(0.3 + Mathf.Abs(Vector3.Dot(collInfo.relativeVelocity.normalized, collInfo.contacts[0].normal)));
		volumeFactor = volumeFactor * 0.5 + 0.5;
		sound.Crash(volumeFactor);
		
		var damage = collInfo.relativeVelocity.magnitude;
	    // Если столкнулись с землёй и машина перевёрнута
	    if (collInfo.gameObject.tag == "Ground" && 
            (transform.eulerAngles.x > 60 && transform.eulerAngles.x < 300 || 
             transform.eulerAngles.z > 60 && transform.eulerAngles.z < 300))	    
	        GotDamage(damage);
	    
	    // Если столкнулись с врагом            
	    else if (collInfo.gameObject.tag == "Enemy" && damage > 4)
	        collInfo.gameObject.transform.root.GetComponent("EnemyController").enemyHP = 0;

	    // Если столкнулись с препятствием
	    else if (collInfo.gameObject.tag != "Ground")
	        GotDamage(damage / 2);	    
	}
}

function OnTriggerEnter(other : Collider)
{
    // Проверяем, столкнулись ли с кубиком
    if (other.gameObject.tag == "HealthPickup" && hp != maxHealthPoints)
    {            
        Destroy(other.gameObject);
        hp = maxHealthPoints;
        healthGUI.color = Color.black;
        healthGUI.text = "Health: " + hp.ToString();        
    }
}

public function GotDamage(damage)
{
    hp -= damage;
    if (hp <= 0)
    {
        healthGUI.text = "Your car is broken!";
        // Забираем управление
        Destroy(car);
        
    }
    else
    {
        healthGUI.color = hp < maxHealthPoints / 2 ? Color.red : Color.black;
        healthGUI.text = "Health: " + hp.ToString();
    }
}