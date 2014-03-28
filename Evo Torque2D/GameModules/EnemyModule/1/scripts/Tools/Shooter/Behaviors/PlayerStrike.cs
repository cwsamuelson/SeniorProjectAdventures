//-----------------------------------------------------------------------------
// PlayerModule: playerBullet class and functions
//-----------------------------------------------------------------------------

function PlayerStrike::onAdd( %this )
{
	%this.initialize();
}

//-----------------------------------------------------------------------------

function PlayerStrike::initialize(%this)
{		
	%this.setSceneGroup(4);
	%this.setSceneLayer(4);
	%this.fixedAngle = true;
	
	%this.fresh = true;
	
	%this.strikeDamage = 20;
	%this.driftSpeed = 8;	
	%this.lifeSpan = 0.25 * 1000;	//ms
	
	%this.sizeRatio = $pixelToWorldRatio;
	%this.myWidth = 46 * %this.sizeRatio;
	%this.myHeight = 174 * %this.sizeRatio;
	
	%this.setupSprite();
		
	%this.setLinearVelocityPolar(%this.strikeAngle+20, %this.driftSpeed);		//0 degrees is down
	
    %this.createPolygonBoxCollisionShape(%this.myWidth, %this.myHeight);
    %this.setCollisionShapeIsSensor(0, true);
    %this.setCollisionGroups( "10 15" );
	%this.setCollisionCallback(true);
	
    %this.setUpdateCallback(true);
	
	%this.mySchedule = schedule(%this.lifeSpan, 0, "PlayerStrike::deleteThis", %this);
}

//-----------------------------------------------------------------------------

function PlayerStrike::setupSprite( %this )
{
	%this.addSprite("0 0");
	%this.setSpriteImage("GameAssets:playerstrike", 0);
	%this.setSpriteSize(%this.myWidth, %this.myHeight);
	%this.setAngle(%this.strikeAngle);
}

//-----------------------------------------------------------------------------

function PlayerStrike::onCollision(%this, %object, %collisionDetails)
{
	if(%this.fresh)
	{
		if(%object.class $= "EnemyUnit")
		{
			//%object.recycle(%object.side);
			
			%object.takeDamage(%this.strikeDamage);
			%this.fresh = false;
		}
	}
}

//-----------------------------------------------------------------------------

function PlayerStrike::onUpdate(%this)
{
	%this.setSpriteBlendAlpha(getEventTimeLeft(%this.mySchedule) / %this.lifeSpan);
}

//-----------------------------------------------------------------------------

function PlayerStrike::deleteThis( %this )
{
	%this.safeDelete();
}

//-----------------------------------------------------------------------------

function PlayerStrike::destroy( %this )
{
}