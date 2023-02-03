using System.Collections.Generic;
using UnityEngine;

public class Expedition: MonoBehaviour {
	public static Expedition i;
	public int Food;
	public List<FamilyMember> members;

	void Start () {
		Expedition.i = this;
	}

	public void ConsumeFood () {
		for (int i = 0; i < members.Count; i++) {
			FamilyMember member = members[i];
			Food--;
			if (Food <= 0) {
				Food = 0;
				member.TakeDamage();
			}
		}
		GameUI.i.UpdateStatus();
		Expedition.i.CheckDeath();
	}

	public void Die (FamilyMember member) {
		Destroy(member.statusBox.gameObject);
		Destroy(member.gameObject);
		members.Remove(member);
	}

	public void CheckDeath () {
		bool foundHuman = false;
		for (int i = 0; i < members.Count; i++) {
			FamilyMember member = members[i];
			if (member.isHuman) {
				foundHuman = true;
				break;
			}
		}
		if (!foundHuman) {
			World.i.StopTime();
			GameUI.i.ShowGameOver();
		}
	}
}