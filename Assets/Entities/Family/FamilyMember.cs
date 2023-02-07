using UnityEngine;

public class FamilyMember: MonoBehaviour {
	public int HP;
	private int MaxHP;
	public int BaseCarryCapacity;
	public StatusBox statusBox;
	public bool isHuman;
	public string memberName;

	void Start () {
		statusBox.HPText.text = this.HP.ToString();
		statusBox.StatusText.text = "";
		MaxHP = this.HP;
	}

	public int GetCarryCapacity () {
		// if is sick reduce
		return BaseCarryCapacity;
	}

	public void TakeDamage(int damage) {
		HP -= damage;
		if (HP <= 0) {
			HP = 0;
			Expedition.i.Die(this);
		}
		statusBox.HPText.text = this.HP.ToString();
	}

	public void Heal () {
		HP += UnityEngine.Random.Range (5, 10);
		if (HP > MaxHP) {
			HP = MaxHP;
		}
	}

}