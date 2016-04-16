using UnityEngine;
using System.Collections;

public class ButtonPlatform : Platform {
	[SerializeField]
	Button btn;

	protected override void onChange(bool change)
	{
		
		base.onChange(change);
		btn.Active = !change;
	//	btn.gameObject.SetActive(change);
	}

}
