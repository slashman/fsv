using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatusBox : MonoBehaviour {
	public TMP_Text NameText;
	public TMP_Text HPText;
	public TMP_Text HungerText;
	public TMP_Text StatusText;

	private FamilyMember familyMember;

	public void AssignFamilyMember (FamilyMember member) {
		this.familyMember = member;
	}

	public void Feed () {
		this.familyMember.ConsumeFood();
	}

	public void OnPointerEnter () {
		transform.localScale = new Vector3(1.1f,1.1f,1);
	}

	public void OnPointerExit () {
		transform.localScale = new Vector3(1,1,1);
	}
}