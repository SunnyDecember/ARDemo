using UnityEngine;

#if UNITY_EDITOR
[UnityEditor.CanEditMultipleObjects]
[UnityEditor.CustomEditor(typeof(P3D_ClickToPaint))]
public class P3D_ClickToPaint_Editor : P3D_Editor<P3D_ClickToPaint>
{
	protected override void OnInspector()
	{
		DrawDefault("Requires");

		DrawDefault("LayerMask");

		DrawDefault("GroupMask");

		DrawDefault("Paint");

		DrawDefault("Brush");
	}
}
#endif

// This script allows you to paint the texture under the current mouse position
// NOTE: This requires the paint targets have the P3D_Paintable component
public class P3D_ClickToPaint : MonoBehaviour
{
	public enum NearestOrAll
	{
		Nearest,
		All
	}

	[Tooltip("The key that must be held down to mouse look")]
	public KeyCode Requires = KeyCode.Mouse0;

	[Tooltip("The GameObject layers you want to be able to paint")]
	public LayerMask LayerMask = -1;

	[Tooltip("The paintable texture groups you want to be able to paint")]
	public P3D_GroupMask GroupMask = -1;

	[Tooltip("Which surfaces it should hit")]
	public NearestOrAll Paint;

	[Tooltip("The settings for the brush we will paint with")]
	public P3D_Brush Brush;
	
	// Called every frame
	protected virtual void Update()
	{
		var mainCamera = Camera.main;

		if (mainCamera != null)
		{
			// The required key is down?
			if (Input.GetKey(Requires) == true)
			{
				var ray   = mainCamera.ScreenPointToRay(Input.mousePosition);
				var start = ray.GetPoint(mainCamera.nearClipPlane);
				var end   = ray.GetPoint(mainCamera.farClipPlane);

				// Paint between the start and end points
				switch (Paint)
				{
					case NearestOrAll.Nearest:
					{
						P3D_Paintable.ScenePaintBetweenNearest(Brush, start, end, LayerMask, GroupMask);
					}
					break;

					case NearestOrAll.All:
					{
						P3D_Paintable.ScenePaintBetweenAll(Brush, start, end, LayerMask, GroupMask);
					}
					break;
				}
			}
		}
	}
}
