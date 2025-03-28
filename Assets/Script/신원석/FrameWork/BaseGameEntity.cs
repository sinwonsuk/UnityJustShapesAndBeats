using UnityEngine;

public abstract class BaseGameEntity : MonoBehaviour
{
	public bool IsStop { get; set; }

	/// <summary>
	/// �Ļ� Ŭ�������� base.Setup()���� ȣ��
	/// </summary>
	public abstract void Setup();


	// GameController Ŭ�������� ��� ������Ʈ�� Updated()�� ȣ���� ������Ʈ�� �����Ѵ�.
	public abstract void Updated();

	public abstract void OffActive();

}

