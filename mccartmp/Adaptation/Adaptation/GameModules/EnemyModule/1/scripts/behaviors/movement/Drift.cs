if (!isObject(DriftBehavior))
{
  %template = new BehaviorTemplate(DriftBehavior);

  %template.friendlyName = "Drift Down";
  %template.behaviorType = "Movement";
  %template.description  = "Drift Down.  Recycle Object At Bottom";

  %template.addBehaviorField(minSpeed, "Minimum speed to fall", float, 15.0);
  %template.addBehaviorField(maxSpeed, "Maximum speed to fall", float, 35.0);
}

function DriftBehavior::onBehaviorAdd(%this)
{
	%this.recycle();
}

function DriftBehavior::onCollision(%this, %object, %collisionDetails)
{
	if(%object.getSceneGroup() == 5)	//Player sceneGroup 
	{
		echo("hittt!");
		%this.recycle(%object.side);
	}
}

function DriftBehavior::recycle(%this)
{
  %this.owner.setPosition(getRandom(-320, 320), 240);
  %this.owner.setLinearVelocityX( 0 );
  %this.owner.setLinearVelocityY( -getRandom(%this.minSpeed, %this.maxSpeed) );
}