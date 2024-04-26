using UnityEngine;
using System.Collections;
public class OO3float : MonoBehaviour
{
	public GameObject player;
	float radian = 0;
	public float perRadian;
	public float radius;
	Vector3 oldPos;
	public bool horizon,prewarm;
	

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("player");
		oldPos = transform.position;
	}
	void FixedUpdate()
	{
		Vector3 selfPos = transform.position, playerPos = player.transform.position;
		if ((Mathf.Abs(selfPos.x - playerPos.x) > 20 || Mathf.Abs(selfPos.y - playerPos.y) > 20) && !prewarm) return;
        if (!horizon)
        {
			radian += perRadian;
			float dy = Mathf.Cos(radian) * radius;
			transform.position = oldPos + new Vector3(0, dy, 0);
		}
        else
        {
			radian += perRadian;
			float dy = Mathf.Cos(radian) * radius;
			transform.position = oldPos + new Vector3(dy, 0, 0);
		}
	}
}