

//A static class is really the same as any other class, the only difference the static keyword does is tell the compiler that only static variables and methods can go in it, and throws you an error if you try to do it. 
//Also, you are not allowed to instantiate a static class!
public static class SpellProcessor
{
	private static List <string> _spellNames = new List<string>();
	
	private static int spellsCasted = 0;
	 
}


public class SpellProcessor
{
	private static List <string> _spellNames = new List<string>();
	
	private int spellsCasted = 0;
	 
}


//Singleton
public class GameMaster : MonoBehaviour
{
    private static GameMaster _gmReference;
    public static GameMaster GmRegerence { get { return _gmReference; } }

    void Awake()
    {
        if (GmRegerence != null)
            GameObject.Destroy(GmRegerence);
        else
            GmRegerence = this;

        DontDestroyOnLoad(this);
    }
}