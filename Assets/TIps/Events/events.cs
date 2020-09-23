
//La classe health fa la seva vida. Es va modificant al llarg del joc i simplement te un event (1) que cada cop que la vida ha canviat ella avisa al evento de que s'ha activat (2)
//com que el evento esta declarat <float> li hem de passar un float al avisar al evento (3)

public class Health : MonoBehaviour
{
    //(1)
    public event Action<float> OnHealthPctChanged = delegate { };//el delegate es com un = null

    private int currentHealth = 10;
    private int maxHealth = 100;


    public void Update ()
    {

        if (beenDamaged)
        {
            currentHealth -= 2;
            float currentHEalthPct = (float)currentHealth / (float)maxHealth;
            OnHealthPctChanged(currentHEalthPct); // (2) (3)

        }
    }
}

//La classe healthbar fa la seva vida pero necessita fer una cosa en quan la vida del player ha canviat. Per tant en el player.GetComponent<Health>().OnHealthPctChanghed (es a dir, l'evento) li afegim el nom metode que he d'activar quan
//vegi que aquell evento s'ha activat (4)
//(5) simplement definim la funcio que la qual hem passat el nom a l'evento

public class HealthBar : MonoBehaviour
{
    public GameObject player;

    public void Awake()
    {
        player.GetComponent<Health>().OnHealthPctChanghed += HandleHealthChanged; //(4)
    }

    private void HandleHealthChanged(float healthPct) //(5)
    {
        ModifyHealthBar();
    }
}

