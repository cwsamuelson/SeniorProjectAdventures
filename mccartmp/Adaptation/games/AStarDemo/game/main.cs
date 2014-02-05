//---------------------------------------------------------------------------------------------
// Torque Game Builder
// Copyright (C) GarageGames.com, Inc.
//---------------------------------------------------------------------------------------------

//---------------------------------------------------------------------------------------------
// initializeProject
// Perform game initialization here.
//---------------------------------------------------------------------------------------------
function initializeProject()
{
   // Load up the in game gui.
   exec("~/gui/mainScreen.gui");

   // Exec game scripts.
   exec("./gameScripts/game.cs");
   exec("./gameScripts/paintMap.cs");
   exec("./gameScripts/basicActiveMouse.cs");
   exec("./gameScripts/aStarPath.cs");
   exec("./gameScripts/aStarActor.cs");
   exec("./gameScripts/enemy.cs");
   
   // This is where the game starts. Right now, we are just starting the first level. You will
   // want to expand this to load up a splash screen followed by a main menu depending on the
   // specific needs of your game. Most likely, a menu button will start the actual game, which
   // is where startGame should be called from.
   startGame( expandFilename("game/data/levels/aStarDemot2d.t2d") );
}

//---------------------------------------------------------------------------------------------
// shutdownProject
// Clean up your game objects here.
//---------------------------------------------------------------------------------------------
function shutdownProject()
{
   endGame();
}

//---------------------------------------------------------------------------------------------
// setupKeybinds
// Bind keys to actions here..
//---------------------------------------------------------------------------------------------
function setupKeybinds()
{
   new ActionMap(moveMap);
   //moveMap.bind("keyboard", "a", "doAction", "Action Description");
}
