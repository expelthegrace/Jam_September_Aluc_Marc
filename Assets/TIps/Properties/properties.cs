public class Ola
{
	//propietat que fa la funcio de variable i tambe et dona la funcio de get (en aquest cas es com onlyread)
	public int contador {get;}
	
	public int i { get; private set; }
		
		
		
	private string  _nameFile;
	//propietat que fa la funcio de set i get de una altra variable
	public string NameFile
	{
		get
		{
			return _nameFile;
		}
		set
		{
			_nameFile = value + "lll";
		}
	}
	
	
	//I wanted to call certain functions whenever the value of a variable changes, I can just put that in the set block of the property
	private int m_someInt;
	public int SomeInt
	{
		set
		{
			m_someInt = value;
			DoSomethingWhichReliesOnUpdatedValue();
		}
	  }
	 
	}
}