using UnityEngine;

public abstract class BaseGameEntity : MonoBehaviour
{
	public bool IsStop { get; set; }

	/// <summary>
	/// 파생 클래스에서 base.Setup()으로 호출
	/// </summary>
	public abstract void Setup();


	// GameController 클래스에서 모든 에이전트의 Updated()를 호출해 에이전트를 구동한다.
	public abstract void Updated();

	public abstract void OffActive();

}

