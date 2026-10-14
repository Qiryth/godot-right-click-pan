#if TOOLS
using Godot;

// ReSharper disable once CheckNamespace
namespace Qiryth.GodotAddons;

[Tool]
public partial class RightClickPan : EditorPlugin
{
	// Static
	private static void SimulateMiddleClick(bool isPressed, Vector2 position, Vector2 globalPosition) => 
		Input.ParseInputEvent(new InputEventMouseButton { ButtonIndex = MouseButton.Middle, Pressed = isPressed,
			Position = position, GlobalPosition = globalPosition });
	
	
	// Variables
	private bool _rightMouseButtonDown;


	// Lifecycle
	public override bool _Handles(GodotObject @object)
	{
		return true;
	}

	public override bool _ForwardCanvasGuiInput(InputEvent @event)
	{
		if (@event is InputEventMouseButton { ButtonIndex: MouseButton.Right } eventMouseButton)
		{
			_rightMouseButtonDown = true;
			SimulateMiddleClick(eventMouseButton.Pressed, eventMouseButton.Position, eventMouseButton.GlobalPosition);
			return eventMouseButton.Pressed;
		}

		if (_rightMouseButtonDown && @event is InputEventMouseMotion motion &&
		    !Input.IsMouseButtonPressed(MouseButton.Right))
		{
			SimulateMiddleClick(false, motion.Position, motion.GlobalPosition);
		}
		
		return false;
	}
}
#endif
