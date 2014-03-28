//-----------------------------------------------------------------------------
// PlayerModule: EnemyUnit class and functions
//-----------------------------------------------------------------------------

function ToolShooter::CreateInstance(%emyOwner, %type, %posX, %posY, %toolOrientation)  
{  
    %r = new SceneObject()  
	{  
		superclass = "ToolNode";
		class = "ToolShooter";   
		owner = %emyOwner;
		toolType = %type;
		bodyPosX = %posX;
		bodyPosY = %posY;
		orientation = %toolOrientation;
		reloadTime = 2.0*1000;
	};  
  
    return %r;  
}  

//-----------------------------------------------------------------------------

function ToolShooter::initialize(%this)
{	
	exec("./EnemyShooterBullet.cs");
	
	Parent::initialize(%this);
	
	//shot barrel offset (instead of bullet coming out of center of cannon)	
	%this.barrelXoffset = 55*%this.owner.sizeRatio;
	%this.barrelYoffset = 0*%this.owner.sizeRatio;	
}

//-----------------------------------------------------------------------------

function ToolShooter::setupCollisionShape( %this )
{
	%offsetX = %this.myWidth/2;
	%offsetY = %this.myHeight/2;
	
	%pAx = %this.bodyPosX*%this.myWidth - %offsetX;
	%pAy = %this.bodyPosY*%this.myHeight - %offsetY;
	
	%pBx = %this.bodyPosX*%this.myWidth + %offsetX;
	%pBy = %this.bodyPosY*%this.myHeight - %offsetY;
	
	%pCx = %this.bodyPosX*%this.myWidth + %offsetX;
	%pCy = %this.bodyPosY*%this.myHeight + %offsetY;
	
	%pDx = %this.bodyPosX*%this.myWidth - %offsetX;
	%pDy = %this.bodyPosY*%this.myHeight + %offsetY;
	
	
	%shapePoints = 
		%pAx SPC %pAy SPC 
		%pBx SPC %pBy SPC 
		%pCx SPC %pCy SPC 
		%pDx SPC %pDy;	
		
	%this.owner.createPolygonCollisionShape(%shapePoints);
}

//-----------------------------------------------------------------------------

function ToolShooter::setupSprite( %this )
{
	%this.owner.addSprite(%this.bodyPosX*%this.myWidth SPC %this.bodyPosY*%this.myHeight);
	
	%this.owner.setSpriteImage("GameAssets:tool_shooter_a", 0);
	%this.owner.setSpriteSize(64 * %this.owner.sizeRatio, 64 * %this.owner.sizeRatio);
	%this.sortLevel = 6;
	
	%this.owner.setSpriteAngle(%this.orientation);
}

//-----------------------------------------------------------------------------

function ToolShooter::setupBehaviors( %this )
{
	exec("./behaviors/shooterAI.cs");
	%baseAI = ShooterToolBehavior.createInstance();
	%this.addBehavior(%baseAI);
}

//-----------------------------------------------------------------------------

function ToolShooter::shoot( %this )
{
	// add a bullet to the arena
	%newBullet = new CompositeSprite()
	{
		class = "EnemyShooterBullet";
		fireAngle = %this.owner.getAngle();
	};
	
	%this.owner.getMyScene().add( %newBullet );
	
	%localX = %this.bodyPosX*%this.myWidth;// - %this.barrelXoffset*((-1*%this.bodyPosX)/%this.bodyPosX);
	%localY = %this.bodyPosY*%this.myHeight;// + %this.barrelYoffset*((-1*%this.bodyPosY)/%this.bodyPosY);
	
	%newBullet.setPosition(%this.owner.getWorldPoint(%localX, %localY)  );
	
	%this.mySchedule = schedule(%this.reloadTime, 0, "ToolShooter::shoot", %this);
} 