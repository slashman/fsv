using UnityEngine;

public class FamilyMember: MonoBehaviour {
	public int HP;
	public int BaseCarryCapacity;
	public StatusBox statusBox;
	public bool isHuman;

	void Start () {
		statusBox.HPText.text = this.HP.ToString();
		statusBox.StatusText.text = "";
	}

	public int GetCarryCapacity () {
		// if is sick reduce
		return BaseCarryCapacity;
	}

	public void TakeDamage() {
		HP--;
		if (HP <= 0) {
			HP = 0;
			Expedition.i.Die(this);
		}
		statusBox.HPText.text = this.HP.ToString();
	}	
}