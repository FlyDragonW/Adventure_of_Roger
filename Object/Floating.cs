using UnityEngine;
using System.Collections;
public class Floating : MonoBehaviour
{
	GameObject player;
	public int index;
	float radian = 0;
	float perRadian = 0.03f; 
	float radius = 0.3f; 
	Vector3 oldPos;
	bool isAbleToGet = true;
					
	void Start()
	{
		player = GameObject.FindGameObjectWithTag("player");
		oldPos = transform.position;
		if (index == 1) isAbleToGet = false;
	}
	void Update()
	{
		Vector3 selfPos = transform.position, playerPos = player.transform.position;
		if ((Mathf.Abs(selfPos.x - playerPos.x) > 20 || Mathf.Abs(selfPos.y - playerPos.y) > 20)) return;
		radian += perRadian; 
		float dy = Mathf.Cos(radian) * radius; 
		transform.position = oldPos + new Vector3(0, dy, 0);
		if (GameObject.Find("HeartBlocker") == null) isAbleToGet = true;
	}

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "player" && isAbleToGet)
        {
			GameObject.Find("items").GetComponent<item>().GetItem(index);
			Destroy(this.gameObject);
        }
    }
}