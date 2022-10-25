Logan's Part Selector

Steps to use:
* Add a PartSelector component to every object you want to select.
* Hook up the member variables exposed to the inspector. This can be accomplished quicker by calling the FetchReferencesFromGameObject method via the context menu. If any of the "Internal Reference" variables aren't set, it will draw a red gizmo warning (you can test this by unhooking one of the references).
* For the camera transform component, you can make an empty gameobject and position it in a good spot to view the part.
* If you want canvas and camera capabilities, you'll need to add a "PartCamera" component to a camera in the scene and add a PartsSelectorManager component to an object in the scene as well. This manager is currently setup to work best on a canvas as it has inspector-exposed references to canvas components. Make sure to set the "mgr" reference to this manager for all the partselectors and the camera.

No external source code was used.