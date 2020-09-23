
public abstract class Spell
{
	public Spell() {}
	
	private int power = 10;
	
	public abstract int range {get;}
	
	public abstract Power {get;}
}

public class FireballSpell : Spell
{
	public FireballSpell (){}
	
	public override range
	{
		get
		{
			return 30;
		}
	}
	
	public override Power
	{
		get
		{
			return power * 10:
		}
	}
}

