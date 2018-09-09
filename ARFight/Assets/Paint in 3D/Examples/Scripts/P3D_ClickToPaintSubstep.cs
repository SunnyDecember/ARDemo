using UnityEngine;

#if UNITY_EDITOR
[UnityEditor.CanEditMultipleObjects]
[UnityEditor.CustomEditor(typeof(P3D_ClickToPaintSubstep))]
public class P3D_ClickToPaintSubstep_Editor : P3D_Editor<P3D_ClickToPaintSubstep>
{
	protected override void OnInspector()
	{
		DrawDefault("Requires");

		DrawDefault("RaycastMask");

		DrawDefault("GroupMask");

		DrawDefault("StepSize");

		DrawDefault("Paint");

		DrawDefault("Brush");
	}
}
#endif

// This script allows you to paint the scene using raycasts
// NOTE: This requires the paint targets have the P3D_Paintable component
public class P3D_ClickToPaintSubstep : MonoBehaviour
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

	[Tooltip("The maximum amount of pixels between ")]
	public float StepSize = 1.0f;

	[Tooltip("Which surfaces it should hit")]
	public NearestOrAll Paint;

	[Tooltip("The settings for the brush we will paint with")]
	public P3D_Brush Brush;

	private Camera mainCamera;

	private Vector2 oldMousePosition;

	// Called every frame
	protected virtual void Update()
	{
		if (mainCamera == null) mainCamera = Camera.main;

		if (mainCamera != null && StepSize > 0.0f)
		{
			// The required key is down?
			if (Input.GetKeyDown(Requires) == true)
			{
				oldMousePosition = Input.mousePosition;
            }

			// The required key is set?
			if (Input.GetKey(Requires) == true)
			{
				// Find the ray for this screen position
				var newMousePosition = (Vector2)Input.mousePosition;
				var stepCount        = Vector2.Distance(oldMousePosition, newMousePosition) / StepSize + 1;

				for (var i = 0; i < stepCount; i++)
				{
					var subMousePosition = Vector2.Lerp(oldMousePosition, newMousePosition, i / stepCount);
					var ray              = mainCamera.ScreenPointToRay(subMousePosition);
					var start            = ray.GetPoint(mainCamera.nearClipPlane);
					var end              = ray.GetPoint(mainCamera.farClipPlane);

					// This will both use Physics.Raycast and search P3D_Paintables
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

				oldMousePosition = newMousePosition;
			}
		}
	}
}
