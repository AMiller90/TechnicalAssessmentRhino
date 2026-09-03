Rhino House Plugin

A small C# Rhino 8 plugin that creates a simple house model from user-defined dimensions.

This project was developed as part of a technical assessment to demonstrate the ability to learn and work with an unfamiliar SDK/API. The plugin uses RhinoCommon to create and display house geometry inside Rhino.

Assessment Requirements

The plugin provides a Rhino command called:
```text
House
```

The command creates four main parts:

House body
Pitched roof
Door
Chimney

The user can configure:

Width
Depth
Height

The assessment also includes a bonus requirement for an interactive preview while the command is running. This has been implemented.

Features
House Rhino command
Default dimensions of 10 × 8 × 8
Interactive Width, Depth, and Height options
Positive dimension validation
Dimension-driven house geometry
Pitched roof
Front door
Chimney positioned through the roof
Live interactive preview
Preview updates when dimensions change
Preview uses the same geometry builder as the final model
Preview geometry remains temporary until the command is completed
Default Dimensions

The command starts with:
```text
Dimension	Default
Width	10
Depth	8
Height	8
```

The house uses the following coordinate convention:
```text
X = Width
Y = Depth
Z = Height
```

The body occupies:
```text
X: 0 → Width
Y: 0 → Depth
Z: 0 → Height
```

The front of the house is at Y = 0.

Interactive Preview

While the House command is running, Rhino displays a temporary wireframe preview of the house.

The command provides three options:
```text
Width
Depth
Height
```

For example:
```text
Adjust the house or click to finish.
(Width=10 Depth=8 Height=8)
```

Selecting an option allows its value to be changed. The preview is then rebuilt using the new dimensions.

The preview includes:

Body
Roof
Door
Chimney

The preview is drawn dynamically in the Rhino viewport and does not add objects to the Rhino document.

When the user finishes the command, the final geometry is generated using the selected dimensions and added to the Rhino document.

Preview Architecture

The preview uses the same HouseBuilder as the final geometry.

Conceptually:
```text
                 HouseParameters
                       │
                       ▼
                  HouseBuilder
                   /         \
                  /           \
                 ▼             ▼
          Final Geometry     Preview
                │              │
                ▼              ▼
             RhinoDoc     Viewport Drawing
```

This avoids maintaining separate geometry calculations for the preview and final model.

Geometry
House Body

The body is created using a RhinoCommon Box, which is converted to a Brep.
```text
X: 0 → Width
Y: 0 → Depth
Z: 0 → Height
```
Roof

The roof is a simple pitched roof.

Its ridge runs along the depth of the house.

The roof height is derived from the house width:
```text
RoofHeight = Width × 0.4
```

The roof is constructed from a triangular profile and extruded along the house depth.

Door

The door is represented by a thin box positioned on the front face of the house.

Its dimensions are derived from the house dimensions:
```text
DoorWidth  = Width × 0.2
DoorHeight = Height × 0.6
DoorDepth  = Width × 0.05
```

The door extends slightly outside the front face to avoid being coplanar with the body.

Chimney

The chimney is a rectangular box positioned toward the rear of the house and intersects the roof.

Its footprint is based on the smaller horizontal house dimension:
```text
ChimneySize = min(Width, Depth) × 0.15
```

Using the smaller dimension keeps the chimney reasonably proportioned when the house has unusual dimensions.

The chimney's vertical position is calculated from the roof slope so that it passes through the roof and extends above the roof peak.

Project Structure

The main project code is organized around three responsibilities:
```text
TechnicalAssessmentRhino/
│
├── Geometry/
│   └── HouseBuilder.cs
│
├── Models/
│   └── HouseParameters.cs
│
├── Preview/
│   └── HousePreviewInput.cs
│
├── HouseCommand.cs
├── TechnicalAssessmentRhinoPlugin.cs
└── ...
```
HouseCommand

Responsible for the Rhino command lifecycle.

It:

1. Starts the House command.
2. Creates the default dimensions.
3. Starts the interactive input and preview.
4. Retrieves the final dimensions.
5. Builds the final geometry.
6. Adds the geometry to the Rhino document.

HouseParameters

A small model containing the three dimensions required to construct the house:
```text
Width
Depth
Height
```

This keeps the geometry code from having to pass several unrelated values independently.

HouseBuilder

Responsible only for creating geometry.

It provides methods for:
```text
BuildBody()
BuildRoof()
BuildDoor()
BuildChimney()
```

The builder returns RhinoCommon geometry and does not modify the Rhino document.

HousePreviewInput

Handles the interactive Rhino input and dynamic preview.

It uses RhinoCommon's GetPoint input mechanism together with OptionDouble options for Width, Depth, and Height.

The temporary preview is drawn using Rhino's dynamic display functionality.

Using the Release

A Windows Rhino 8 plugin package is available from the GitHub Release:

TechnicalAssessmentRhino v1.0.0

Download:

technicalassessmentrhino-1.0.0-rh8_0-win.yak

Installing the Plugin

The .yak package can be installed directly into Rhino 8.

On Windows:

Download the .yak file from the GitHub Release.
Double-click the .yak file.
Rhino will open and install the package.
Restart Rhino if it was already running so the newly installed plugin is loaded.

Alternatively, the package can be installed using Rhino's bundled Yak command-line tool:

"C:\Program Files\Rhino 8\System\yak.exe" install <path-to-package>\technicalassessmentrhino-1.0.0-rh8_0-win.yak


The plugin targets Rhino 8 for Windows.

Using the House Command

Once the plugin is installed:

Open Rhino 8.
Enter House in the Rhino command line.
A live wireframe preview of the house appears.
Use the Width, Depth, and Height command options to change the dimensions.
The preview updates as the dimensions are changed.
Click in the viewport to finish the command.
The final house geometry is added to the Rhino document.

The default dimensions are:

Dimension	Default
Width	10
Depth	8
Height	8

The command options can be selected directly from the Rhino command line or entered by typing their names.

Building and Running from Visual Studio

For development, the project can also be opened in Visual Studio 2022 and launched using the Rhino debugging target.

Open the solution in Visual Studio.
Build the project.
Start debugging.
Rhino 8 launches as the debugging target.
Enter House in the Rhino command line.
Adjust the dimensions if required.
Click in the viewport to finish the interactive input.

This development workflow is separate from installing the packaged .yak release.

Building the Project
Requirements
Windows
Visual Studio 2022
Rhino 8 for Windows
RhinoCommon / Rhino Visual Studio tooling

The project was created using the RhinoCommon C# plugin template.

Build

Open the solution in Visual Studio and build the project:
```text
Build → Build Solution
```

The project is configured as a Rhino plugin project and can be launched/debugged from Visual Studio.

Running the Plugin
1. Open the project in Visual Studio.
2. Start debugging.
3. Rhino 8 launches as the debugging target.
4. Open the Rhino command line.
5. Enter:
```text
House
```
Adjust Width, Depth, or Height if required.
Click in the viewport to finish the interactive input.
The final house geometry is added to the Rhino document.
Input Validation

Width, Depth, and Height must all be positive.

The current minimum accepted value is:
```text
0.001
```

Values such as 0 or negative values are rejected by the Rhino input options.

No arbitrary maximum dimension is imposed.

This allows the command to handle unusual proportions such as:
```text
Width = 5
Depth = 20
Height = 3
```

and:
```text
Width = 30
Depth = 5
Height = 20
```

Testing

The geometry and preview were tested using the following dimensions:

Default
```text
10 / 8 / 8
```

Verified:

Body proportions
Roof placement
Door placement
Chimney placement
Roof/chimney intersection
Interactive preview
Final geometry

Unusual Proportions
```text
5 / 20 / 3
```

and:
```text
30 / 5 / 20
```

These tests were used to check that the geometry remained sensible when width, depth, and height varied significantly.

Particular attention was given to the chimney because independently scaling its width and depth produced poor proportions for very wide or very deep houses. The final implementation uses the smaller horizontal house dimension for the chimney footprint.

The interactive preview was also tested while rotating the Rhino perspective camera to ensure the temporary geometry remained correctly displayed.

Architecture

The main separation of responsibilities is:
```text
Rhino
  │
  ▼
HouseCommand
  │
  ▼
HousePreviewInput
  │
  ▼
HouseParameters
  │
  ▼
HouseBuilder
  │
  ▼
RhinoCommon Geometry
```

For final geometry:
```text
HouseBuilder
     │
     ▼
   Breps
     │
     ▼
 HouseCommand
     │
     ▼
  RhinoDoc
```

For preview geometry:
```text
HousePreviewInput
       │
       ▼
HouseParameters
       │
       ▼
HouseBuilder
       │
       ▼
Temporary viewport drawing
```

A key design decision was keeping document modification out of HouseBuilder. HouseBuilder creates geometry, while HouseCommand is responsible for adding that geometry to RhinoDoc.

Scope and Limitations

This project intentionally keeps the house model simple and focused on the assessment requirements.

It does not include:

Materials or textures
Layers
Interior rooms
Stairs
Architectural detailing
Persistence or saved house configurations
Custom UI panels
Editing of previously created houses

Running the House command multiple times creates additional house geometry in the current Rhino document.

The roof, door, and chimney are intentionally simple geometric representations rather than detailed architectural components.

RhinoCommon Concepts Used

The project uses several RhinoCommon concepts that were relevant to the implementation:

RhinoDoc — represents the current Rhino document/model.
Box — used to create the main house body and box-based components.
Brep — the geometry representation ultimately added to the Rhino document.
Polyline / curves — used to construct the roof profile.
Extrusion — used to create the pitched roof from its profile.
GetPoint — used to implement the interactive command input.
OptionDouble — used for the Width, Depth, and Height command options.
Dynamic drawing — used to display the temporary interactive preview.

The project was developed incrementally using the RhinoCommon plugin template and official RhinoCommon documentation as references.

Notes

The primary goal of the project was not to build a complete architectural modeling tool, but to demonstrate the ability to learn RhinoCommon and use it to create a small, structured Rhino plugin.

The implementation therefore favors straightforward geometry, clear separation of responsibilities, and dimension-driven behavior over unnecessary complexity.