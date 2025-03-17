using UnityEngine;

public class TeleportPointController : MonoBehaviour 
{
	public enum TeleportPointType {
		MoveToPosition,
		ChangeScene
	};


	public TeleportPointType teleportType = TeleportPointType.MoveToPosition;
	
	public Color anchorMainColor; //titleVisibleColor
	public Color anchorHighlightedColor; //titleHighlightedColor
	public Color anchorLockedColor; //titleLockedColor


}
